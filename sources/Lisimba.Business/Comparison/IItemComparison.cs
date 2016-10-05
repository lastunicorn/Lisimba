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

namespace DustInTheWind.Lisimba.Business.Comparison
{
    /// <summary>
    /// Represents a comparison between two items.
    /// </summary>
    public interface IItemComparison
    {
        /// <summary>
        /// Gets the left side item that is part of the comparison.
        /// </summary>
        object ParentLeft { get; }

        /// <summary>
        /// Gets the right side item that is part of the comparison.
        /// </summary>
        object ParentRight { get; }

        /// <summary>
        /// Gets the value from the left side item that is actually compared.
        /// It may be the item itself or just a property of the item.
        /// </summary>
        object ValueLeft { get; }

        /// <summary>
        /// Gets the value from the right side item that is actually compared.
        /// It may be the item itself or just a property of the item.
        /// </summary>
        object ValueRight { get; }

        /// <summary>
        /// Gets the result of the comparison.
        /// </summary>
        ItemEquality Equality { get; }

        List<IItemComparison> Comparisons { get; }
    }
}