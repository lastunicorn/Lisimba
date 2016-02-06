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

namespace DustInTheWind.Lisimba.Egg.AddressBookModel
{
    class ContactMatch
    {
        public int Percentage { get; private set; }
        public Contact Contact1 { get; private set; }
        public Contact Contact2 { get; private set; }

        public ContactMatch(Contact contact1, Contact contact2)
        {
            Contact1 = contact1;
            Contact2 = contact2;

            CalculatePercentage();
        }

        private void CalculatePercentage()
        {
            if (Contact1 == null || Contact2 == null)
            {
                Percentage = 0;
            }
            else
            {
                bool name = PersonName.Equals(Contact1.Name, Contact2.Name);
                bool birthday = Date.Equals(Contact1.Birthday, Contact2.Birthday);
                List<ContactItem> identicalItems = Contact1.Items
                    .Where(x => Contact2.Items.Contains(x))
                    .ToList();
                int itemCount = Math.Max(Contact1.Items.Count, Contact2.Items.Count);
                bool notes = Contact1.Notes == Contact2.Notes;

                Percentage = (name ? 30 : 0) +
                             (birthday ? 30 : 0) +
                             (itemCount == 0 ? 30 : identicalItems.Count * 30 / itemCount) +
                             (notes ? 10 : 0);
            }
        }
    }
}