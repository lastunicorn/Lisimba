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

            foreach (Contact contact1 in addressBook1.Contacts)
            {
                bool foundInContacts2 = false;

                for (int i2 = 0; i2 < contacts2.Count; i2++)
                {
                    Contact contact2 = contacts2[i2];

                    if (!contact1.Equals(contact2))
                        continue;

                    ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                    {
                        Contact1 = contact1,
                        Contact2 = contact2,
                        AreEqual = true
                    };

                    result.Add(contactComparisonResult);
                    contacts2.Remove(contact1);

                    foundInContacts2 = true;
                    break;
                }

                if (!foundInContacts2)
                {
                    ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                    {
                        Contact1 = contact1,
                        AreEqual = false
                    };

                    result.Add(contactComparisonResult);
                }
            }

            foreach (Contact contact2 in contacts2)
            {
                ContactComparisonResult contactComparisonResult = new ContactComparisonResult
                {
                    Contact2 = contact2,
                    AreEqual = false
                };

                result.Add(contactComparisonResult);
            }

            return result;
        }
    }
}