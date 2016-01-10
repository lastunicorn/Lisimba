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

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class AddressBookGuarder
    {
        private readonly AddressBookGuarderConsole console;

        public AddressBooks AddressBooks { get; set; }

        public AddressBookGuarder(AddressBookGuarderConsole console)
        {
            if (console == null) throw new ArgumentNullException("console");

            this.console = console;
        }

        /// <returns><c>true</c> if it is allowed to continue; false otherwise.</returns>
        public bool EnsureSave()
        {
            if (AddressBooks == null || AddressBooks.Current == null || AddressBooks.Current.IsAddressBookSaved)
                return true;

            bool? needToSave = console.AskToSaveAddressBook();

            if (needToSave == null)
                return false;

            if (!needToSave.Value)
                return true;

            if (AddressBooks.Current.Location == null)
            {
                string newLocation = console.AskForNewLocation();

                if (newLocation == null)
                    return false;

                AddressBooks.SaveAddressBookAs(newLocation);
            }
            else
            {
                AddressBooks.SaveAddressBook();
            }

            return true;
        }
    }
}
