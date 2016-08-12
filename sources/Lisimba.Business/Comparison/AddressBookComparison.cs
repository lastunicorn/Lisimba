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

        public List<ContactComparison> Results { get; private set; }
        

        public bool AreEqual
        {
            get { return Results.All(x => x.AreEqual); }
        }

        public IEnumerable<ContactComparison> IdenticalContacts
        {
            get { return Results.Where(x => x.AreEqual); }
        }

        public IEnumerable<ContactComparison> UniqueLeftContacts
        {
            get { return Results.Where(x => x.Equality == ItemEquality.LeftExists); }
        }

        public IEnumerable<ContactComparison> UniqueRightContacts
        {
            get { return Results.Where(x => x.Equality == ItemEquality.RightExists); }
        }

        public AddressBookComparison(AddressBook addressBookLeft, AddressBook addressBookRight)
        {
            if (addressBookLeft == null) throw new ArgumentNullException("addressBookLeft");
            if (addressBookRight == null) throw new ArgumentNullException("addressBookRight");

            this.addressBookLeft = addressBookLeft;
            this.addressBookRight = addressBookRight;

            Results = new List<ContactComparison>();
        }

        public void Compare()
        {
            Results.Clear();

            List<Contact> addressBookRightContacts = addressBookRight.Contacts.ToList();

            foreach (Contact contactLeft in addressBookLeft.Contacts)
            {
                ContactComparison comparison = addressBookRightContacts
                    .Select(x => new ContactComparison(contactLeft, x))
                    .FirstOrDefault(x => x.AreEqual);

                if (comparison != null)
                {
                    Results.Add(comparison);
                    addressBookRightContacts.Remove(comparison.ContactRight);
                }
                else
                {
                    Results.Add(new ContactComparison(contactLeft, null));
                }
            }

            foreach (Contact contactRight in addressBookRightContacts)
                Results.Add(new ContactComparison(null, contactRight));
        }
    }
}