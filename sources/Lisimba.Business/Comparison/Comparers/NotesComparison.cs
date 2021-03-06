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

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class NotesComparison : ItemComparisonBase<string>
    {
        public NotesComparison(string valueLeft, string valueRight)
            : base(valueLeft, valueRight)
        {
        }

        protected override bool LeftHasValue()
        {
            return !string.IsNullOrEmpty(ValueLeft);
        }

        protected override bool RightHasValue()
        {
            return !string.IsNullOrEmpty(ValueRight);
        }

        protected override bool ValuesAreEqual()
        {
            return ValueLeft == ValueRight;
        }

        protected override bool ValuesAreSimilar()
        {
            return ValueLeft.Contains(ValueRight) ||
                ValueRight.Contains(ValueLeft);
        }
    }
}