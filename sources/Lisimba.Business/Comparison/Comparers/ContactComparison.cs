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

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class ContactComparison : ItemComparisonBase<Contact>
    {
        private PersonNameComparison personNameComparison;
        private NotesComparison notesComparison;
        private DateComparison birthdayComparison;
        private CategoryComparison categoryComparison;
        private PictureComparison pictureComparison;

        public ContactComparison(Contact contactLeft, Contact contactRight)
            : base(contactLeft, contactRight)
        {
        }

        protected override void PrepareToCompareValues()
        {
            if (Comparisons == null)
                Comparisons = new List<IItemComparison>();
            else
                Comparisons.Clear();
        }

        protected override void PrepareToCompareNotEmptyValues()
        {
            CompareAllItems();
        }

        private void CompareAllItems()
        {
            personNameComparison = new PersonNameComparison(ValueLeft.Name, ValueRight.Name);
            Comparisons.Add(personNameComparison);

            notesComparison = new NotesComparison(ValueLeft.Notes, ValueRight.Notes);
            Comparisons.Add(notesComparison);

            birthdayComparison = new DateComparison(ValueLeft.Birthday, ValueRight.Birthday);
            Comparisons.Add(birthdayComparison);

            categoryComparison = new CategoryComparison(ValueLeft.Category, ValueRight.Category);
            Comparisons.Add(categoryComparison);

            pictureComparison = new PictureComparison(ValueLeft.Picture, ValueRight.Picture);
            Comparisons.Add(pictureComparison);

            List<ContactItem> contactRightItems = ValueRight.Items.ToList();

            foreach (ContactItem itemLeft in ValueLeft.Items)
            {
                IItemComparison comparison = contactRightItems
                    .Select(x => ItemComparisonFactory.Create(itemLeft, x))
                    .FirstOrDefault(x => x.Equality == ItemEquality.Equal || x.Equality == ItemEquality.Similar);

                if (comparison != null)
                {
                    Comparisons.Add(comparison);
                    contactRightItems.Remove(comparison.ValueRight as ContactItem);
                }
                else
                {
                    Comparisons.Add(ItemComparisonFactory.Create(itemLeft, null));
                }
            }

            foreach (ContactItem itemRight in contactRightItems)
                Comparisons.Add(ItemComparisonFactory.Create(null, itemRight));
        }

        protected override bool ValuesAreEqual()
        {
            // Equal
            // - all items should be Equal or Empty

            return Comparisons.All(x => x.Equality == ItemEquality.BothEmpty || x.Equality == ItemEquality.Equal);
        }

        protected override bool ValuesAreSimilar()
        {
            // Similar
            // - Names should not be both empty and should be anything but Different.

            return personNameComparison.Equality != ItemEquality.Different &&
                   personNameComparison.Equality != ItemEquality.BothEmpty;
        }
    }
}