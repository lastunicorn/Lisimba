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
using DustInTheWind.Lisimba.Egg.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class SaveAddressBookCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;
        private readonly UiService uiService;
        private readonly StatusService statusService;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return "Save current opened address book."; }
        }

        public SaveAddressBookCommand(CurrentData currentData, UiService uiService, StatusService statusService, RecentFiles recentFiles)
        {
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.currentData = currentData;
            this.uiService = uiService;
            this.statusService = statusService;
            this.recentFiles = recentFiles;

            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = currentData.AddressBook != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = currentData.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                string fileName;
                bool isNew = false;

                if (currentData.AddressBook.FileName == null)
                {
                    fileName = uiService.AskToSaveLsbFile();

                    if (fileName == null)
                        return;

                    isNew = true;
                }
                else
                {
                    fileName = currentData.AddressBook.FileName;
                }

                ZipXmlGate gate = new ZipXmlGate();
                gate.Save(currentData.AddressBook, fileName);

                currentData.AddressBook.SetAsSaved();

                statusService.StatusText = string.Format("Address book saved. ({0} contacts)", currentData.AddressBook.Contacts.Count);

                if (isNew)
                    recentFiles.AddRecentFile(Path.GetFullPath(fileName));
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}
