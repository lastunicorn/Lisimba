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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.CommandLine.Properties;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class ShowFlow : IFlow
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly EnhancedConsole console;

        public ShowFlow(OpenedAddressBooks openedAddressBooks, EnhancedConsole console)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.openedAddressBooks = openedAddressBooks;
            this.console = console;
        }

        public void Execute(ReadOnlyCollection<string> parameters)
        {
            if (openedAddressBooks.Current != null)
            {
                if (parameters != null && parameters.Count > 0)
                    DisplayContactDetails(parameters[0]);
                else
                    DisplayAllContacts();
            }
            else
            {
                console.WriteLineError(Resources.NoAddessBookOpenedError);
            }
        }

        private void DisplayContactDetails(string contactName)
        {
            IEnumerable<Contact> contacts = GetContacts(contactName);

            foreach (Contact contact in contacts)
            {
                console.WriteLineNormal("Name: " + contact.Name);
                console.WriteLineNormal("Birthday: " + contact.Birthday.ToShortString() + " - " + contact.ZodiacSign + " " + ToChar(contact.ZodiacSign));
            }
        }

        private char ToChar(ZodiacSign zodiacSign)
        {
            switch (zodiacSign)
            {
                case ZodiacSign.NotSpecified:
                    return ' ';
                case ZodiacSign.Aquarius:
                    return '♒'; // &#9810;
                case ZodiacSign.Pisces:
                    return '♓'; // &#9811
                case ZodiacSign.Aries:
                    return '♈'; // &#9800
                case ZodiacSign.Taurus:
                    return '♉'; // &#9801
                case ZodiacSign.Gemini:
                    return '♊'; // &#9802
                case ZodiacSign.Cancer:
                    return '♋'; // &#9803
                case ZodiacSign.Leo:
                    return '♌'; // &#9804
                case ZodiacSign.Virgo:
                    return '♍'; // &#9805
                case ZodiacSign.Libra:
                    return '♎'; // &#9806
                case ZodiacSign.Scorpio:
                    return '♏'; // &#9807
                case ZodiacSign.Sagittarius:
                    return '♐'; // &#9808
                case ZodiacSign.Capricorn:
                    return '♑'; // &#9809
                default:
                    throw new ArgumentOutOfRangeException("zodiacSign", zodiacSign, null);
            }
        }

        private IEnumerable<Contact> GetContacts(string contactName)
        {
            return openedAddressBooks.Current.AddressBook.Contacts.Where(x => MatchName(x.Name.FirstName, contactName) || MatchName(x.Name.MiddleName, contactName) || MatchName(x.Name.LastName, contactName) || MatchName(x.Name.Nickname, contactName));
        }

        private static bool MatchName(string name, string contactName)
        {
            return name != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(name, contactName, CompareOptions.IgnoreCase) >= 0;
        }

        private void DisplayAllContacts()
        {
            foreach (Contact contact in openedAddressBooks.Current.AddressBook.Contacts)
                console.WriteLineNormal(contact.ToString());
        }
    }
}