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

using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public class CategoryComparison : ItemComparisonBase<Contact>
    {
        public CategoryComparison(Contact contactLeft, Contact contactRight)
            : base(contactLeft, contactRight)
        {
        }

        protected override bool LeftHasValue()
        {
            return !string.IsNullOrEmpty(ItemLeft.Category);
        }

        protected override bool RightHasValue()
        {
            return !string.IsNullOrEmpty(ItemRight.Category);
        }

        protected override bool ValuesAreEqual()
        {
            return ItemLeft.Category == ItemRight.Category;
        }

        protected override bool ValuesAreSimilar()
        {
            return ItemLeft.Category == ItemRight.Category;
        }
    }
}