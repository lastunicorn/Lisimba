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

        public List<IItemComparison> Results { get; private set; }
        public ItemEquality Equality { get; private set; }

        public ContactComparison(Contact contactLeft, Contact contactRight)
        {
            ContactLeft = contactLeft;
            ContactRight = contactRight;

            Results = new List<IItemComparison>();

            Compare();
        }

        private void Compare()
        {
            Results.Clear();

            if (ContactLeft == null && ContactRight == null)
                Equality = ItemEquality.BothEmpty;
            else if (ContactLeft == null)
                Equality = ItemEquality.RightExists;
            else if (ContactRight == null)
                Equality = ItemEquality.LeftExists;
            else
            {
                CompareAllItems();

                bool areEqual = Results.All(x => x.Equality == ItemEquality.BothEmpty || x.Equality == ItemEquality.Equal);

                if (areEqual)
                    Equality = ItemEquality.Equal;
                else
                    Equality = (Results.All(x => x.Equality != ItemEquality.Different))
                        ? ItemEquality.Similar
                        : ItemEquality.Different;

                // Equal
                // - all items should be Equal

                // Similar - not Equal and:
                // - 

                // If birthday has same date but different description. (*)
                // If parts of the name are added in one side and absent in the other
                // If category exists only in one side
            }
        }

        private void CompareAllItems()
        {
            Results.Add(new NotesComparison(ContactLeft, ContactRight));
            Results.Add(new BirthdayComparison(ContactLeft, ContactRight));
            Results.Add(new CategoryComparison(ContactLeft, ContactRight));
            Results.Add(new FirstNameComparison(ContactLeft, ContactRight));
            Results.Add(new MiddleNameComparison(ContactLeft, ContactRight));
            Results.Add(new LastNameComparison(ContactLeft, ContactRight));
            Results.Add(new NicknameComparison(ContactLeft, ContactRight));
            Results.Add(new PictureComparison(ContactLeft, ContactRight));

            List<ContactItem> contactRightItems = ContactRight.Items.ToList();

            foreach (ContactItem itemLeft in ContactLeft.Items)
            {
                ItemComparison comparison = contactRightItems
                    .Select(x => new ItemComparison(itemLeft, x))
                    .FirstOrDefault(x => x.Equality == ItemEquality.Equal);

                if (comparison != null)
                {
                    Results.Add(comparison);
                    contactRightItems.Remove(comparison.ItemRight);
                }
                else
                {
                    Results.Add(new ItemComparison(itemLeft, null));
                }
            }

            foreach (ContactItem itemRight in contactRightItems)
                Results.Add(new ItemComparison(null, itemRight));
        }
    }
}