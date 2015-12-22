// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.Lisimba.Egg.Comparers
{
    internal class MultipleComparer : IComparer
    {
        private readonly List<IComparer> comparers;

        public MultipleComparer(IEnumerable<IComparer> comparers)
        {
            if (comparers == null) throw new ArgumentNullException("comparers");

            this.comparers = new List<IComparer>(comparers);
        }

        public int Compare(object x, object y)
        {
            if (comparers.Count == 0)
                return 0;

            return comparers
                .Select(comparer => comparer.Compare(x, y))
                .FirstOrDefault(value => value != 0);
        }
    }
}