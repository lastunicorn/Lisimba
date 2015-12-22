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

namespace Lisimba.Cmd.Business
{
    class AddressBookGuarderConsole
    {
        public bool? AskToSaveAddressBook()
        {
            Console.WriteLine("Do you want to save current address book? [y-yes; n-no; c-cancel] ");
            ConsoleKeyInfo key = Console.ReadKey(false);

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

        public string AskForLocation()
        {
            Console.WriteLine("Address book file name [empty string to cancel]: ");
            return Console.ReadLine();
        }
    }
}
