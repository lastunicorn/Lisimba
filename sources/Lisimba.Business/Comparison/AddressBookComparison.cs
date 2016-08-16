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

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class AddressBookComparison
    {
        private readonly AddressBook addressBookLeft;
        private readonly AddressBook addressBookRight;

        public List<ContactComparison> Comparisons { get; private set; }
        
        public bool AreEqual
        {
            get { return Comparisons.All(x => x.Equality == ItemEquality.Equal); }
        }

        public IEnumerable<ContactComparison> IdenticalContacts
        {
            get { return Comparisons.Where(x => x.Equality == ItemEquality.Equal); }
        }

        public IEnumerable<ContactComparison> UniqueLeftContacts
        {
            get { return Comparisons.Where(x => x.Equality == ItemEquality.LeftExists); }
        }

        public IEnumerable<ContactComparison> UniqueRightContacts
        {
            get { return Comparisons.Where(x => x.Equality == ItemEquality.RightExists); }
        }

        public AddressBookComparison(AddressBook addressBookLeft, AddressBook addressBookRight)
        {
            if (addressBookLeft == null) throw new ArgumentNullException("addressBookLeft");
            if (addressBookRight == null) throw new ArgumentNullException("addressBookRight");

            this.addressBookLeft = addressBookLeft;
            this.addressBookRight = addressBookRight;

            Comparisons = new List<ContactComparison>();
        }

        public void Compare()
        {
            Comparisons.Clear();

            List<Contact> addressBookRightContacts = addressBookRight.Contacts.ToList();

            // Search each contact from left address book into the right address book.

            foreach (Contact contactLeft in addressBookLeft.Contacts)
            {
                ContactComparison comparison = addressBookRightContacts
                    .Select(x => new ContactComparison(contactLeft, x))
                    .FirstOrDefault(x => x.Equality == ItemEquality.Equal || x.Equality == ItemEquality.Similar);

                if (comparison != null)
                {
                    Comparisons.Add(comparison);

                    // If identical contact found in right address book, remove it.
                    addressBookRightContacts.Remove(comparison.ContactRight);
                }
                else
                {
                    Comparisons.Add(new ContactComparison(contactLeft, null));
                }
            }

            // Create items for the remaining contacts in the right address book.

            foreach (Contact contactRight in addressBookRightContacts)
                Comparisons.Add(new ContactComparison(null, contactRight));
        }
    }
}