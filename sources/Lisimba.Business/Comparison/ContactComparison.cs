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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class ContactComparison
    {
        public Contact ContactLeft { get; private set; }
        public Contact ContactRight { get; private set; }

        public List<IItemComparison> Differences { get; private set; }

        public ContactComparison(Contact contactLeft, Contact contactRight)
        {
            ContactLeft = contactLeft;
            ContactRight = contactRight;

            Differences = new List<IItemComparison>();
        }

        public void Compare()
        {
            Differences.Clear();

            Differences.Add(new NotesComparison(ContactLeft, ContactRight));
            Differences.Add(new BirthdayComparison(ContactLeft, ContactRight));
            Differences.Add(new CategoryComparison(ContactLeft, ContactRight));
            Differences.Add(new FirstNameComparison(ContactLeft, ContactRight));
            Differences.Add(new MiddleNameComparison(ContactLeft, ContactRight));
            Differences.Add(new LastNameComparison(ContactLeft, ContactRight));
            Differences.Add(new NicknameComparison(ContactLeft, ContactRight));
            Differences.Add(new PictureComparison(ContactLeft, ContactRight));

            List<ContactItem> contactRightItems = ContactRight.Items.ToList();

            foreach (ContactItem itemLeft in ContactLeft.Items)
            {
                List<ItemComparison> comparisons = contactRightItems
                    .Select(x => new ItemComparison(itemLeft, x))
                    .ToList();

                ItemComparison comparison = comparisons.FirstOrDefault(x => x.Equality == ItemEquality.Equal);

                if (comparison != null)
                {
                    Differences.Add(comparison);
                    contactRightItems.Remove(comparison.ItemRight);
                }
                else
                {
                    Differences.Add(new ItemComparison(itemLeft, null));
                }
            }

            foreach (ContactItem itemRight in contactRightItems)
                Differences.Add(new ItemComparison(null, itemRight));
        }
    }
}