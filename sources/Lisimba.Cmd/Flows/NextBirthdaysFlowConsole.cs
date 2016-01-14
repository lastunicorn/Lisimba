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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Cmd.Properties;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.Cmd.Flows
{
    class NextBirthdaysFlowConsole
    {
        private readonly UserInterface userInterface;

        public NextBirthdaysFlowConsole(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;
        }

        public void DisplayContactWithBirthday(Contact contact)
        {
            string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
            Console.WriteLine(text);
        }

        public void DisplayNoAddressBookMessage()
        {
            userInterface.WriteLineError(Resources.NoAddessBookOpenedError);
        }
    }
}