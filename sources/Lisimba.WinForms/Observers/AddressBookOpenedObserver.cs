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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ObservingModel;
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.Observers
{
    internal class AddressBookOpenedObserver : IObserver
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly ApplicationStatus applicationStatus;
        private readonly WindowSystem windowSystem;
        private readonly BirthdaysInfo birthdaysInfo;

        public AddressBookOpenedObserver(OpenedAddressBooks openedAddressBooks, ApplicationStatus applicationStatus,
            WindowSystem windowSystem, BirthdaysInfo birthdaysInfo)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (birthdaysInfo == null) throw new ArgumentNullException("birthdaysInfo");

            this.openedAddressBooks = openedAddressBooks;
            this.applicationStatus = applicationStatus;
            this.windowSystem = windowSystem;
            this.birthdaysInfo = birthdaysInfo;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookOpened += HandleAddressBookOpened;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookOpened -= HandleAddressBookOpened;
        }

        private void HandleAddressBookOpened(object sender, AddressBookOpenedEventArgs e)
        {
            DisplayOpenSuccessMessage();
            DisplayWarnings(e.Result.Warnings);
            birthdaysInfo.Show();
        }

        private void DisplayOpenSuccessMessage()
        {
            if (openedAddressBooks.Current == null)
                return;

            if (openedAddressBooks.Current.Status == AddressBookStatus.New)
            {
                applicationStatus.StatusText = LocalizedResources.NewAddressBook_SuccessStatusText;
            }
            else
            {
                int contactsCount = openedAddressBooks.Current.AddressBook.Contacts.Count;
                applicationStatus.StatusText = string.Format(Resources.OpenAddressBook_SuccessStatusText, contactsCount);
            }
        }

        private void DisplayWarnings(IEnumerable<Exception> warnings)
        {
            if (warnings == null || !warnings.Any())
                return;

            windowSystem.DisplayWarning(warnings);
        }
    }
}