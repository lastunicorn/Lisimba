// Lisimba
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
using System.ComponentModel;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    class AddressBookEnsureSaveObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;

        public AddressBookEnsureSaveObserver(OpenedAddressBooks openedAddressBooks, UserInterface userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosing += HandleAddressBookClosing;
        }

        private void HandleAddressBookClosing(object sender, CancelEventArgs e)
        {
            bool allowToContinue = EnsureAddressBookIsSaved();

            if (!allowToContinue)
                e.Cancel = true;
        }

        public bool EnsureAddressBookIsSaved()
        {
            if (openedAddressBooks.Current == null || openedAddressBooks.Current.Status != AddressBookStatus.Modified)
                return true;

            string text = LocalizedResources.EnsureAddressBookIsSaved_Question;
            string title = LocalizedResources.EnsureAddressBookIsSaved_Title;

            bool? needToSave = userInterface.DisplayYesNoCancelQuestion(text, title);

            if (needToSave == null)
                return false;

            if (!needToSave.Value)
                return true;

            if (openedAddressBooks.Current.Location == null)
            {
                string newLocation = userInterface.AskToSaveLsbFile();

                if (newLocation == null)
                    return false;

                openedAddressBooks.Current.SaveAddressBook(newLocation);
            }
            else
            {
                openedAddressBooks.Current.SaveAddressBook();
            }

            return true;
        }
    }
}