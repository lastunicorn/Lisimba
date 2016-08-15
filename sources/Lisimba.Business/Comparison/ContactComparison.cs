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
        private PersonNameComparison personNameComparison;
        private NotesComparison notesComparison;
        private BirthdayComparison birthdayComparison;
        private CategoryComparison categoryComparison;
        private PictureComparison pictureComparison;

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

                // Equal
                // - all items should be Equal

                bool areEqual = Results.All(x => x.Equality == ItemEquality.BothEmpty || x.Equality == ItemEquality.Equal);

                if (areEqual)
                    Equality = ItemEquality.Equal;
                else
                {
                    // Similar
                    // - Names should not be Different

                    bool areSimilar = personNameComparison.Equality != ItemEquality.Different &&
                        personNameComparison.Equality != ItemEquality.BothEmpty;

                    Equality = areSimilar
                        ? ItemEquality.Similar
                        : ItemEquality.Different;
                }
            }
        }

        private void CompareAllItems()
        {
            personNameComparison = new PersonNameComparison(ContactLeft.Name, ContactRight.Name);
            Results.Add(personNameComparison);

            notesComparison = new NotesComparison(ContactLeft, ContactRight);
            Results.Add(notesComparison);

            birthdayComparison = new BirthdayComparison(ContactLeft.Birthday, ContactRight.Birthday);
            Results.Add(birthdayComparison);

            categoryComparison = new CategoryComparison(ContactLeft, ContactRight);
            Results.Add(categoryComparison);

            pictureComparison = new PictureComparison(ContactLeft, ContactRight);
            Results.Add(pictureComparison);

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