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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.GateManagement;

namespace DustInTheWind.Lisimba.Cmd.Observers
{
    class AddressBookClosingObserver : IObserver
    {
        private readonly EnhancedConsole console;
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;

        public AddressBookClosingObserver(EnhancedConsole console, OpenedAddressBooks openedAddressBooks, AvailableGates availableGates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.console = console;
            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
        }

        public void Start()
        {
            openedAddressBooks.AddressBookClosing += HandleAddressBookClosing;
        }

        public void Stop()
        {
            openedAddressBooks.AddressBookClosing -= HandleAddressBookClosing;
        }

        private void HandleAddressBookClosing(object sender, CancelEventArgs e)
        {
            bool allowToContinue = EnsureAddressBookIsSaved();

            if (!allowToContinue)
                e.Cancel = true;
        }

        /// <returns><c>true</c> if it is allowed to continue; false otherwise.</returns>
        public bool EnsureAddressBookIsSaved()
        {
            if (openedAddressBooks.Current == null || openedAddressBooks.Current.Status != AddressBookStatus.Modified)
                return true;

            bool? needToSave = AskToSaveAddressBook();

            if (needToSave == null)
                return false;

            if (!needToSave.Value)
                return true;

            if (openedAddressBooks.Current.Location == null)
            {
                string newLocation = AskForNewLocation();

                if (newLocation == null)
                    return false;

                if (openedAddressBooks.Current.Gate == null)
                    openedAddressBooks.Current.SaveAddressBook(newLocation, availableGates.DefaultGate);
                else
                    openedAddressBooks.Current.SaveAddressBook(newLocation);
            }
            else
            {
                openedAddressBooks.Current.SaveAddressBook();
            }

            return true;
        }

        public bool? AskToSaveAddressBook()
        {
            console.WriteNormal(Resources.AskToSaveAddressBook);

            ConsoleKeyInfo key = console.ReadKey();
            console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.Y:
                    return true;

                case ConsoleKey.N:
                    return false;

                default:
                    return null;
            }
        }

        public string AskForNewLocation()
        {
            console.WriteNormal(Resources.AskForNewLocation);
            return console.ReadLine();
        }
    }
}