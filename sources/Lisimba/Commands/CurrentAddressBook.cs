using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Egg.Exceptions;
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

            // Open the file
            AddressBook = addressBookLoader.LoadFromFile(fileName);

            IsNew = false;
            IsModified = false;

            // Display a status text
            statusService.StatusText = AddressBook.Count + " contacts oppened.";

            // Update the RecentFiles list.
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

            // Display a status text
            statusService.StatusText = "Address book saved. (" + AddressBook.Count + " contacts)";

            OnAddressBookSaved(EventArgs.Empty);
        }

        public void SaveAs()
        {
            string fileName = AskToSaveLsbFile();

            if (fileName != null)
            {
                addressBookLoader.SaveToFile(AddressBook, fileName);
                IsNew = false;
                IsModified = false;

                // Display a status text
                statusService.StatusText = "Address book saved. (" + AddressBook.Count + " contacts)";

                // Update the RecentFiles list.
                recentFilesService.AddRecentFile(Path.GetFullPath(fileName));

                OnAddressBookSaved(EventArgs.Empty);
            }
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

                statusService.StatusText = countImport + " contacts imported from " + newContacts.Count + " contacts in .csv file.";

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
