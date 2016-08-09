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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public class AddressBookComparer
    {
        private readonly AddressBook addressBook1;
        private readonly AddressBook addressBook2;

        private AddressBookComparisonResult result;
        private List<Contact> contacts2;

        public AddressBookComparer(AddressBook addressBook1, AddressBook addressBook2)
        {
            if (addressBook1 == null) throw new ArgumentNullException("addressBook1");
            if (addressBook2 == null) throw new ArgumentNullException("addressBook2");

            this.addressBook1 = addressBook1;
            this.addressBook2 = addressBook2;
        }

        public AddressBookComparisonResult Compare()
        {
            result = new AddressBookComparisonResult();
            contacts2 = addressBook2.Contacts.ToList();

            foreach (Contact contact1 in addressBook1.Contacts)
            {
                bool foundInContacts2 = CheckAgainstSecondAddressBook(contact1);

                if (!foundInContacts2)
                    AddResult(contact1, null, false);
            }

            foreach (Contact contact2 in contacts2)
                AddResult(null, contact2, false);

            return result;
        }

        private bool CheckAgainstSecondAddressBook(Contact contact1)
        {
            for (int i2 = 0; i2 < contacts2.Count; i2++)
            {
                Contact contact2 = contacts2[i2];

                bool areEquals = contact1.Equals(contact2);
                if (areEquals)
                {
                    AddResult(contact1, contact2, true);
                    contacts2.Remove(contact2);

                    return true;
                }
            }

            return false;
        }

        private void AddResult(Contact contact1, Contact contact2, bool areEqual)
        {
            ContactComparisonResult contactComparisonResult = new ContactComparisonResult
            {
                Contact1 = contact1,
                Contact2 = contact2,
                AreEqual = areEqual
            };

            result.Add(contactComparisonResult);
        }
    }
}