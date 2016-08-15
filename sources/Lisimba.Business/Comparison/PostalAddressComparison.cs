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
    public class PostalAddressComparison : ItemComparisonBase<PostalAddress>
    {
        public PostalAddressComparison(PostalAddress emailLeft, PostalAddress emailRight)
            : base(emailLeft, emailRight)
        {
        }

        protected override bool LeftHasValue()
        {
            return ItemLeft != null;
        }

        protected override bool RightHasValue()
        {
            return ItemRight != null;
        }

        protected override bool ValuesAreEqual()
        {
            return PostalAddress.Equals(ItemLeft, ItemRight);
        }

        protected override bool ValuesAreSimilar()
        {
            return (string.IsNullOrEmpty(ItemLeft.Street) || string.IsNullOrEmpty(ItemRight.Street) || ItemLeft.Street == ItemRight.Street) &&
                   (string.IsNullOrEmpty(ItemLeft.City) || string.IsNullOrEmpty(ItemRight.City) || ItemLeft.Street == ItemRight.City) &&
                   (string.IsNullOrEmpty(ItemLeft.State) || string.IsNullOrEmpty(ItemRight.State) || ItemLeft.Street == ItemRight.State) &&
                   (string.IsNullOrEmpty(ItemLeft.PostalCode) || string.IsNullOrEmpty(ItemRight.PostalCode) || ItemLeft.Street == ItemRight.PostalCode) &&
                   (string.IsNullOrEmpty(ItemLeft.Country) || string.IsNullOrEmpty(ItemRight.Country) || ItemLeft.Street == ItemRight.Country);
        }
    }
}