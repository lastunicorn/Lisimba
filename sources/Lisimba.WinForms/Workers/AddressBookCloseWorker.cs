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
using DustInTheWind.Lisimba.Business.WorkerModel;
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.Workers
{
    internal class AddressBookCloseWorker : IWorker
    {
        private readonly AddressBooks addressBooks;
        private readonly WindowSystem windowSystem;
        private readonly ApplicationStatus applicationStatus;

        public AddressBookCloseWorker(AddressBooks addressBooks, WindowSystem windowSystem,
            ApplicationStatus applicationStatus)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.addressBooks = addressBooks;
            this.windowSystem = windowSystem;
            this.applicationStatus = applicationStatus;
        }

        public void Start()
        {
            addressBooks.AddressBookClosing += HandleAddressBookClosing;
            addressBooks.AddressBookClosed += HandleAddressBookClosed;
        }

        public void Stop()
        {
            addressBooks.AddressBookClosing -= HandleAddressBookClosing;
            addressBooks.AddressBookClosed -= HandleAddressBookClosed;
        }

        private void HandleAddressBookClosing(object sender, AddressBookClosingEventArgs e)
        {
            if (e.AddressBook.Status == AddressBookStatus.Modified)
            {
                bool? needToSave = AskToSaveAddressBook();

                if (needToSave == null)
                    e.Cancel = true;
                else
                    e.SaveAddressBook = needToSave.Value;
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

            return windowSystem.DisplayYesNoCancelQuestion(text, title);
        }

        private void HandleAddressBookClosed(object sender, AddressBookClosedEventArgs e)
        {
            applicationStatus.StatusText = LocalizedResources.AddressBookClosedSuccessStatus;
        }
    }
}