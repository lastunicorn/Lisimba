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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.WinFormsCommon.ObservingModel;

namespace DustInTheWind.Lisimba.Observers
{
    internal class ContactDeletingObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly WindowSystem windowSystem;

        public ContactDeletingObserver(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.openedAddressBooks = openedAddressBooks;
            this.windowSystem = windowSystem;
        }

        public void Start()
        {
            openedAddressBooks.ContactDeleting += HandleContactDeleting;
        }

        public void Stop()
        {
            openedAddressBooks.ContactDeleting -= HandleContactDeleting;
        }

        private void HandleContactDeleting(object sender, ContactDeletingEventArgs e)
        {
            if (!e.Cancel)
            {
                bool allowToDelete = ConfirmDeleteContact(e.ContactToDelete);
                e.Cancel = !allowToDelete;
            }
        }

        private bool ConfirmDeleteContact(Contact contactToDelete)
        {
            string text = string.Format(LocalizedResources.ContactDelete_ConfirametionQuestion, contactToDelete.Name);
            string title = LocalizedResources.ContactDelete_ConfirmationTitle;

            return windowSystem.DisplayYesNoExclamation(text, title);
        }
    }
}