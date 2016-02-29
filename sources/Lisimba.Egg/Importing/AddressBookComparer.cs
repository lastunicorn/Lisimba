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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Egg.Importing
{
    public class AddressBookComparer
    {
        public ComparisonResult Compare(AddressBook addressBook1, AddressBook addressBook2)
        {
            if (addressBook1 == null) throw new ArgumentNullException("addressBook1");
            if (addressBook2 == null) throw new ArgumentNullException("addressBook2");

            ComparisonResult result = new ComparisonResult();

            List<Contact> contacts2 = addressBook2.Contacts.ToList();

            foreach (Contact contact in addressBook1.Contacts)
            {
                int index2 = contacts2.IndexOf(contact);

                if (index2 >= 0)
                {
                    ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                    {
                        Contact1 = contact,
                        Contact2 = contacts2[index2],
                        AreEqual = true
                    };

                    result.Add(contactComparisonResult);
                    contacts2.Remove(contact);
                }
                else
                {
                    ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                    {
                        Contact1 = contact,
                        AreEqual = false
                    };

                    result.Add(contactComparisonResult);
                }
            }

            foreach (Contact contact in contacts2)
            {
                ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                {
                    Contact2 = contact,
                    AreEqual = false
                };

                result.Add(contactComparisonResult);
            }

            return result;
        }
    }
}