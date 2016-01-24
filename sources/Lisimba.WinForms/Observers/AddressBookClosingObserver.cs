// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Observers
{
    class AddressBookClosingObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;

        public AddressBookClosingObserver(OpenedAddressBooks openedAddressBooks, UserInterface userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosing += HandleAddressBookClosing;
            openedAddressBooks.NewLocationNeeded += HandleAddressBooksNewLocationNeeded;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookClosing -= HandleAddressBookClosing;
            openedAddressBooks.NewLocationNeeded -= HandleAddressBooksNewLocationNeeded;
        }

        private void HandleAddressBookClosing(object sender, AddressBookClosingEventArgs e)
        {
            if (e.AddressBook.Status == AddressBookStatus.Modified)
            {
                bool? needToSave = AskToSaveAddressBook();

                if (needToSave == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.SaveAddressBook = needToSave.Value;
                }
            }
            else
            {
                e.SaveAddressBook = false;
            }
        }

        private bool? AskToSaveAddressBook()
        {
            string text = LocalizedResources.EnsureAddressBookIsSaved_Question;
            string title = LocalizedResources.EnsureAddressBookIsSaved_Title;

            return userInterface.DisplayYesNoCancelQuestion(text, title);
        }

        private void HandleAddressBooksNewLocationNeeded(object sender, NewLocationNeededEventArgs e)
        {
            string newLocation = userInterface.AskToSaveLsbFile();

            if (string.IsNullOrEmpty(newLocation))
                e.Cancel = true;
            else
                e.NewLocation = newLocation;
        }
    }
}