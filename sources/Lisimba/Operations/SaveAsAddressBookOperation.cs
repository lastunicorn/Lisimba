﻿// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class SaveAsAddressBookOperation : ExecutableViewModelBase<object>
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return LocalizedResources.SaveAsAddressBookOperationDescription; }
        }

        public SaveAsAddressBookOperation(OpenedAddressBooks openedAddressBooks, UserInterface userInterface,
            ApplicationStatus applicationStatus, RecentFiles recentFiles)
            : base(applicationStatus)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
            this.recentFiles = recentFiles;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = openedAddressBooks.Current != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = openedAddressBooks.Current != null;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                if (openedAddressBooks.Current == null)
                    throw new LisimbaException(LocalizedResources.NoAddessBookOpenedError);

                string newLocation = userInterface.AskToSaveLsbFile();
                openedAddressBooks.Current.SaveAddressBook(newLocation);

                AddFileToRecentFileList(newLocation);

                openedAddressBooks.Current.SaveAddressBook(newLocation);

                //OnAddressBookSaved(EventArgs.Empty);
                
                DisplaySuccessStatusText();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }

        private void AddFileToRecentFileList(string fileName)
        {
            string fileFullPath = Path.GetFullPath(fileName);
            recentFiles.AddRecentFile(fileFullPath, null);
        }

        private void DisplaySuccessStatusText()
        {
            int contactCount = openedAddressBooks.Current.AddressBook.Contacts.Count;
            applicationStatus.StatusText = string.Format(Resources.AddressBookSaved_StatusText, contactCount);
        }
    }
}