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

namespace DustInTheWind.Lisimba.Business.Comparison
{
    /// <summary>
    /// A comparison between two items of type <see cref="TItem"/>.
    /// The comparison is performed on some properties of the items.
    /// The type of the properties is <see cref="TValue"/>.
    /// </summary>
    public abstract class ItemComparisonBase<TItem, TValue> : IItemComparison<TItem, TValue>
    {
        public TItem ItemLeft { get; private set; }
        public TItem ItemRight { get; private set; }

        object IItemComparison.ItemLeft
        {
            get { return ItemLeft; }
        }

        object IItemComparison.ItemRight
        {
            get { return ItemRight; }
        }

        public abstract TValue ValueLeft { get; }

        public abstract TValue ValueRight { get; }

        object IItemComparison.ValueLeft
        {
            get { return ValueLeft; }
        }

        object IItemComparison.ValueRight
        {
            get { return ValueRight; }
        }

        public ItemEquality Equality { get; private set; }

        protected ItemComparisonBase(TItem itemLeft, TItem itemRight)
        {
            ItemLeft = itemLeft;
            ItemRight = itemRight;

            Compare();
        }

        private void Compare()
        {
            PrepareToCompareValues();

            bool leftHasValue = LeftHasValue();
            bool rightHasValue = RightHasValue();

            if (!leftHasValue && !rightHasValue)
                Equality = ItemEquality.BothEmpty;
            else if (!leftHasValue)
                Equality = ItemEquality.RightExists;
            else if (!rightHasValue)
                Equality = ItemEquality.LeftExists;
            else
                CompareNotEmptyValues();
        }

        private void CompareNotEmptyValues()
        {
            PrepareToCompareNotEmptyValues();

            if (ValuesAreEqual())
                Equality = ItemEquality.Equal;
            else if (ValuesAreSimilar())
                Equality = ItemEquality.Similar;
            else
                Equality = ItemEquality.Different;
        }

        protected abstract bool LeftHasValue();
        protected abstract bool RightHasValue();
        protected abstract bool ValuesAreEqual();
        protected abstract bool ValuesAreSimilar();

        protected virtual void PrepareToCompareValues()
        {
        }

        protected virtual void PrepareToCompareNotEmptyValues()
        {
        }
    }
}