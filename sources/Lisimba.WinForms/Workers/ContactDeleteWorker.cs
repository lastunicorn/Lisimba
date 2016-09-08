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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.WorkerModel;
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms.Workers
{
    internal class ContactDeleteWorker : IWorker
    {
        private readonly AddressBooks addressBooks;
        private readonly WindowSystem windowSystem;

        public ContactDeleteWorker(AddressBooks addressBooks, WindowSystem windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.addressBooks = addressBooks;
            this.windowSystem = windowSystem;
        }

        public void Start()
        {
            addressBooks.ContactDeleting += HandleContactDeleting;
        }

        public void Stop()
        {
            addressBooks.ContactDeleting -= HandleContactDeleting;
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