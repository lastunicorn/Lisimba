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
using DustInTheWind.Lisimba.Cmd.Common;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class PrompterConsole
    {
        public void DisplayPrompter(string addressBookName, bool isSaved)
        {
            Console.WriteLine();
            ConsoleHelper.WriteEmphasize("lisimba");

            if (addressBookName != null)
            {
                Console.Write(" ");

                string formattedAddressBookName = BuildAddressBookName(addressBookName, isSaved);
                Console.Write(formattedAddressBookName);
            }

            ConsoleHelper.WriteEmphasize(" > ");
        }

        private static string BuildAddressBookName(string addressBookName, bool isSaved)
        {
            string notSavedMarker = isSaved ? string.Empty : "*";
            return string.Format("[{0}]{1}", addressBookName, notSavedMarker);
        }

        public string ReadCommand()
        {
            return Console.ReadLine();
        }

        public void WriteError(string text)
        {
            ConsoleHelper.WriteLineError(text);
        }
    }
}