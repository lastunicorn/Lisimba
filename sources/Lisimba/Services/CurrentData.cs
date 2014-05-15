﻿// Lisimba
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
using System.Linq;
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

        public AddressBook AddressBook
        {
            get { return addressBook; }
            set
            {
                if (addressBook == value)
                    return;

                OnAddressBookChanging(EventArgs.Empty);

                AddressBook oldAddressBook = addressBook;
                addressBook = value;

                Contact = null;

                OnAddressBookChanged(new AddressBookChangedEventArgs(oldAddressBook, addressBook));
            }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                if (contact == value)
                    return;

                contact = value;
                OnContactChanged();
            }
        }

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        #region Event AddressBookChanging

        public event EventHandler AddressBookChanging;

        protected virtual void OnAddressBookChanging(EventArgs e)
        {
            EventHandler handler = AddressBookChanging;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region Event AddressBookChanged

        public event EventHandler<AddressBookChangedEventArgs> AddressBookChanged;

        protected virtual void OnAddressBookChanged(AddressBookChangedEventArgs e)
        {
            EventHandler<AddressBookChangedEventArgs> handler = AddressBookChanged;

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

            warnings = new List<Exception>();
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
            AddressBook openedAddressBook = gate.Load(fileName);
            openedAddressBook.SetAsSaved();

            warnings.AddRange(gate.Warnings);

            AddressBook = openedAddressBook;

            statusService.StatusText = string.Format("{0} contacts oppened.", openedAddressBook.Contacts.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));
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

            AddressBook.SetAsSaved();

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Contacts.Count);

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

            AddressBook.SetAsSaved();

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Contacts.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void ImportFromYahooCsv()
        {
            warnings.Clear();

            string fileName = AskToOpenYahooCsvFile();

            if (fileName == null)
                return;

            try
            {
                YahooCsvGate yahooCsvGate = new YahooCsvGate();
                AddressBook yahooAddressBook = yahooCsvGate.Load(fileName);

                ContactCollection yahooContacts = yahooAddressBook.Contacts;
                ImportRuleCollection mergeRules = CreateMergeRules(yahooContacts);

                AddressBook = new AddressBook();
                int countImport = AddressBook.Contacts.AddRange(yahooContacts, mergeRules);

                statusService.StatusText = string.Format("{0} contacts imported from {1} contacts in .csv file.", countImport, yahooContacts.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ImportRuleCollection CreateMergeRules(ContactCollection contacts)
        {
            IEnumerable<ImportRule> rules = contacts.Select(x => new ImportRule(x));
            return new ImportRuleCollection(rules.ToList());
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
