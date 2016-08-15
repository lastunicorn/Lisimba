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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Comparison
{
    public abstract class ItemComparisonBase : IItemComparison
    {
        protected Contact ContactLeft { get; set; }
        protected Contact ContactRight { get; set; }

        public abstract ContactItem ItemLeft { get; }
        public abstract ContactItem ItemRight { get; }

        public ItemEquality Equality { get; private set; }

        protected ItemComparisonBase(Contact contactLeft, Contact contactRight)
        {
            if (contactLeft == null) throw new ArgumentNullException("contactLeft");
            if (contactRight == null) throw new ArgumentNullException("contactRight");

            ContactLeft = contactLeft;
            ContactRight = contactRight;

            Compare();
        }

        private void Compare()
        {
            if (ContactLeftHasValue())
                if (ContactRightHasValue())
                    if (HaveSameValue())
                        Equality = ItemEquality.Equal;
                    else if (HaveSimilarValue())
                        Equality = ItemEquality.Similar;
                    else
                        Equality = ItemEquality.Different;
                else
                    Equality = ItemEquality.LeftExists;
            else
                Equality = ContactRightHasValue() ? ItemEquality.RightExists : ItemEquality.BothEmpty;
        }

        protected abstract bool ContactLeftHasValue();
        protected abstract bool ContactRightHasValue();
        protected abstract bool HaveSameValue();
        protected abstract bool HaveSimilarValue();
    }

    public abstract class ItemComparisonBase<T> : IItemComparison
        where T : ContactItem
    {
        public T ItemLeft { get; set; }
        public T ItemRight { get; set; }

        ContactItem IItemComparison.ItemLeft
        {
            get { return ItemLeft; }
        }

        ContactItem IItemComparison.ItemRight
        {
            get { return ItemRight; }
        }

        public ItemEquality Equality { get; private set; }

        protected ItemComparisonBase(T itemLeft, T itemRight)
        {
            ItemLeft = itemLeft;
            ItemRight = itemRight;

            Compare();
        }

        private void Compare()
        {
            if (LeftHasValue())
                if (RightHasValue())
                    if (ValuesAreEqual())
                        Equality = ItemEquality.Equal;
                    else if (ValuesAreSimilar())
                        Equality = ItemEquality.Similar;
                    else
                        Equality = ItemEquality.Different;
                else
                    Equality = ItemEquality.LeftExists;
            else
                Equality = RightHasValue() ? ItemEquality.RightExists : ItemEquality.BothEmpty;
        }

        protected abstract bool LeftHasValue();
        protected abstract bool RightHasValue();
        protected abstract bool ValuesAreEqual();
        protected abstract bool ValuesAreSimilar();
    }
}