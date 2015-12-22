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
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Business
{
    class AddressBookGuarderConsole
    {
        public bool? AskToSaveAddressBook()
        {
            Console.Write(Resources.AskToSaveAddressBook);
            
            ConsoleKeyInfo key = Console.ReadKey(false);
            Console.WriteLine();

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
            Console.WriteLine(Resources.AskForNewLocation);
            return Console.ReadLine();
        }
    }
}
