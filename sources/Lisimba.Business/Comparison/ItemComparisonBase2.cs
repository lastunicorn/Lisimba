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

namespace DustInTheWind.Lisimba.Business.Comparison
{
    /// <summary>
    /// A comparison between two items of type <see cref="TParent"/>.
    /// The comparison is performed on some properties of the items.
    /// The type of the properties is <see cref="TValue"/>.
    /// </summary>
    public abstract class ItemComparisonBase<TParent, TValue> : IItemComparison<TParent, TValue>
    {
        public TParent ParentLeft { get; private set; }
        public TParent ParentRight { get; private set; }

        object IItemComparison.ParentLeft
        {
            get { return ParentLeft; }
        }

        object IItemComparison.ParentRight
        {
            get { return ParentRight; }
        }

        public TValue ValueLeft { get; private set; }

        public TValue ValueRight { get; private set; }

        object IItemComparison.ValueLeft
        {
            get { return ValueLeft; }
        }

        object IItemComparison.ValueRight
        {
            get { return ValueRight; }
        }

        public ItemEquality Equality { get; private set; }

        protected ItemComparisonBase(TParent parentLeft, TValue valueLeft, TParent parentRight, TValue valueRight)
        {
            ParentLeft = parentLeft;
            ParentRight = parentRight;

            ValueLeft = valueLeft;
            ValueRight = valueRight;

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

        protected virtual bool LeftHasValue()
        {
            return ValueLeft != null;
        }

        protected virtual bool RightHasValue()
        {
            return ValueRight != null;
        }

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