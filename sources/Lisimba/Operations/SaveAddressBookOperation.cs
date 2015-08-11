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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class SaveAddressBookOperation : ExecutableViewModelBase<object>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UserInterface userInterface;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return LocalizedResources.SaveAddressBookOperationDescription; }
        }

        public SaveAddressBookOperation(AddressBookShell addressBookShell, UserInterface userInterface, ApplicationStatus applicationStatus, RecentFiles recentFiles)
            : base(applicationStatus)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.addressBookShell = addressBookShell;
            this.userInterface = userInterface;
            this.recentFiles = recentFiles;

            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = addressBookShell.AddressBook != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = addressBookShell.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                string fileName;
                bool isNew = false;

                if (addressBookShell.FileName == null)
                {
                    fileName = userInterface.AskToSaveLsbFile();

                    if (fileName == null)
                        return;

                    isNew = true;
                }
                else
                {
                    fileName = addressBookShell.FileName;
                }

                ZipXmlGate gate = new ZipXmlGate();
                addressBookShell.SaveTo(gate, fileName);

                applicationStatus.StatusText = string.Format("Address book saved. ({0} contacts)", addressBookShell.AddressBook.Contacts.Count);

                if (isNew)
                    recentFiles.AddRecentFile(Path.GetFullPath(fileName));
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }
    }
}