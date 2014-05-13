// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Gating;

namespace DustInTheWind.Lisimba.Services
{
    class CurrentData
    {
        private readonly StatusService statusService;
        private readonly RecentFilesService recentFilesService;

        private readonly List<Exception> warnings;
        private Contact contact;
        private AddressBook addressBook;

        public Func<string> AskToOpenLsbFile { get; set; }

        public Func<string> AskToSaveLsbFile { get; set; }

        public Func<string> AskToOpenYahooCsvFile { get; set; }

        public Func<string> AskToSaveYahooCsvFile { get; set; }

        public bool IsNew { get; private set; }

        public bool IsModified { get; set; }

        public AddressBook AddressBook
        {
            get { return addressBook; }
            private set
            {
                if (addressBook != null)
                    addressBook.Changed -= HandleAddressBookContentChanged;

                addressBook = value;

                if (addressBook != null)
                    addressBook.Changed += HandleAddressBookContentChanged;
            }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnContactChanged();
            }
        }

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        #region Event AddressBookChanged

        public event EventHandler AddressBookChanged;

        protected virtual void OnAddressBookChanged(EventArgs e)
        {
            EventHandler handler = AddressBookChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Event AddressBookContentChanged

        public event EventHandler AddressBookContentChanged;

        protected virtual void OnAddressBookContentChanged(EventArgs e)
        {
            EventHandler handler = AddressBookContentChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Event AddressBookSaved

        public event EventHandler AddressBookSaved;

        protected virtual void OnAddressBookSaved(EventArgs e)
        {
            EventHandler handler = AddressBookSaved;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Event ContactChanged

        public event EventHandler ContactChanged;

        protected virtual void OnContactChanged()
        {
            EventHandler handler = ContactChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion


        public CurrentData(StatusService statusService, RecentFilesService recentFilesService)
        {
            if (statusService == null)
                throw new ArgumentNullException("statusService");

            if (recentFilesService == null)
                throw new ArgumentNullException("recentFilesService");

            this.statusService = statusService;
            this.recentFilesService = recentFilesService;

            IsNew = true;
            IsModified = false;

            warnings = new List<Exception>();
        }

        private void HandleAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            IsModified = true;
            OnAddressBookContentChanged(EventArgs.Empty);
        }

        public void New()
        {
            warnings.Clear();

            AddressBook = new AddressBook();

            IsNew = true;
            IsModified = false;

            statusService.StatusText = "A new address book was created.";

            OnAddressBookChanged(EventArgs.Empty);
        }

        public void Open(string fileName)
        {
            warnings.Clear();

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = AskToOpenLsbFile();

                if (fileName == null)
                    return;
            }

            ZipXmlGate gate = new ZipXmlGate();
            AddressBook = gate.Load(fileName);

            warnings.AddRange(gate.Warnings);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("{0} contacts oppened.", AddressBook.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

            OnAddressBookChanged(EventArgs.Empty);
        }

        public void Save()
        {
            warnings.Clear();

            if (AddressBook.FileName.Length == 0)
            {
                SaveAs();
                return;
            }


            ZipXmlGate gate = new ZipXmlGate();
            gate.Save(AddressBook, AddressBook.FileName);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Count);

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void SaveAs()
        {
            warnings.Clear();

            string fileName = AskToSaveLsbFile();

            if (fileName == null)
                return;

            ZipXmlGate gate = new ZipXmlGate();
            gate.Save(AddressBook, fileName);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void ImportFromYahooCsv()
        {
            warnings.Clear();

            AddressBook = new AddressBook();

            IsNew = true;
            IsModified = false;

            string fileName = AskToOpenYahooCsvFile();

            if (fileName == null)
                return;

            try
            {
                YahooCsvGate yahooCsvGate = new YahooCsvGate();
                AddressBook newAddressBook = yahooCsvGate.Load(fileName);

                ContactCollection newContacts = newAddressBook.Contacts;
                ImportRuleCollection importRules = CreateImportRules(newContacts);
                int countImport = AddressBook.AddRange(newContacts, importRules);

                IsModified = true;

                statusService.StatusText = string.Format("{0} contacts imported from {1} contacts in .csv file.", countImport, newContacts.Count);

                OnAddressBookChanged(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ImportRuleCollection CreateImportRules(ContactCollection newContacts)
        {
            ImportRuleCollection rules = new ImportRuleCollection();

            for (int i = 0; i < newContacts.Count; i++)
            {
                rules.Add(new ImportRule(newContacts[i]));
            }

            return rules;
        }

        public void ExportToYahooCsv()
        {
            warnings.Clear();

            string fileName = AskToSaveYahooCsvFile();

            if (fileName == null)
                return;

            YahooCsvGate gate = new YahooCsvGate();
            gate.Save(AddressBook, fileName);
        }
    }
}
