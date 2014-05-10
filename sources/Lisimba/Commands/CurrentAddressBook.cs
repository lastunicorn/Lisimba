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
using System.IO;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class CurrentAddressBook
    {
        private readonly AddressBookManager addressBookLoader = new AddressBookManager();
        private readonly StatusService statusService;
        private readonly RecentFilesService recentFilesService;

        public Func<string> AskToOpenLsbFile { get; set; }

        public Func<string> AskToSaveLsbFile { get; set; }

        public Func<string> AskToOpenYahooCsvFile { get; set; }

        public Func<string> AskToSaveYahooCsvFile { get; set; }

        public EventHandler<IncorrectXmlVersionEventArgs> HandleIncorrectXmlVersion;

        public bool IsNew { get; private set; }

        public bool IsModified { get; set; }

        public AddressBook AddressBook { get; private set; }

        #region Event AddressBookChanged

        public event EventHandler AddressBookChanged;

        protected virtual void OnAddressBookChanged(EventArgs e)
        {
            EventHandler handler = AddressBookChanged;

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

        public CurrentAddressBook(StatusService statusService, RecentFilesService recentFilesService)
        {
            if (statusService == null)
                throw new ArgumentNullException("statusService");

            if (recentFilesService == null)
                throw new ArgumentNullException("recentFilesService");

            this.statusService = statusService;
            this.recentFilesService = recentFilesService;

            IsNew = true;
            IsModified = false;

            addressBookLoader.IncorrectXmlVersion += HandleIncorrectXmlVersion;
        }

        public void New()
        {
            AddressBook = new AddressBook();

            IsNew = true;
            IsModified = false;

            statusService.StatusText = "A new address book was created.";

            OnAddressBookChanged(EventArgs.Empty);
        }

        public void Open(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = AskToOpenLsbFile();

                if (fileName == null)
                    return;
            }

            AddressBook = addressBookLoader.LoadFromFile(fileName);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("{0} contacts oppened.", AddressBook.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

            OnAddressBookChanged(EventArgs.Empty);
        }

        public void Save()
        {
            if (AddressBook.FileName.Length == 0)
            {
                SaveAs();
                return;
            }

            addressBookLoader.SaveToFile(AddressBook);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Count);

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void SaveAs()
        {
            string fileName = AskToSaveLsbFile();

            if (fileName == null)
                return;

            addressBookLoader.SaveToFile(AddressBook, fileName);

            IsNew = false;
            IsModified = false;

            statusService.StatusText = string.Format("Address book saved. ({0} contacts)", AddressBook.Count);
            recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void ImportFromYahooCsv()
        {
            string fileName = AskToOpenYahooCsvFile();

            if (fileName == null)
                return;

            try
            {
                ContactCollection newContacts = addressBookLoader.ImportFromFile(fileName, FileFormat.CsvYahoo);
                ImportRuleCollection importRules = addressBookLoader.CreateImportRules(newContacts);
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

        public void ExportToYahooCsv()
        {
            string fileName = AskToSaveYahooCsvFile();

            if (fileName == null)
                return;

            addressBookLoader.ExportToFile(AddressBook, fileName, FileFormat.CsvYahoo);
        }
    }
}
