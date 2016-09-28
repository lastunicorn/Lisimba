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
using System.Reflection;

namespace DustInTheWind.Lisimba.Business.Comparison.Comparers
{
    public class ObjectComparison : ItemComparisonBase<object, object>
    {
        public ObjectComparison(object parentLeft, object valueLeft, object parentRight, object valueRight)
            : base(parentLeft, valueLeft, parentRight, valueRight)
        {
        }

        protected override bool ValuesAreEqual()
        {
            return AreEqual();
        }

        protected override bool ValuesAreSimilar()
        {
            return AreEqual();
        }

        private bool AreEqual()
        {
            if (ValueLeft.GetType() != ValueRight.GetType())
                return false;

            Type t = ValueLeft.GetType();
            MethodInfo methodInfo = t.GetMethod("Equals");

            if (ReferenceEquals(ValueLeft, ValueRight))
                return true;

            if (methodInfo.GetParameters().Length == 2 && methodInfo.ReturnParameter != null && methodInfo.ReturnParameter.ParameterType == typeof(bool))
                return (bool)methodInfo.Invoke(null, new[] { ValueLeft, ValueRight });

            return false;
        }
    }
}