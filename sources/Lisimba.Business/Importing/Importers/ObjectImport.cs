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

using System.Text;
using DustInTheWind.Lisimba.Business.Comparison;
using DustInTheWind.Lisimba.Business.Comparison.Comparers;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class ObjectImport : ItemImportBase<object, object>
    {
        protected override string Name
        {
            get { return "Object"; }
        }

        public ObjectImport(IItemComparison<object, object> itemComparison)
            : base(itemComparison)
        {
        }

        protected override void AddAsNew()
        {
        }

        protected override void Merge()
        {
        }

        protected override void Replace()
        {
        }

        public static ObjectImport Empty()
        {
            return new ObjectImport(new ObjectComparison(null, null, null, null));
        }
    }
}