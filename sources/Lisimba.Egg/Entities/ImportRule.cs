// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class ImportRule
    {
        public ImportType ImportType { get; set; }

        public Contact NewContact { get; set; }

        public Contact OriginalContact { get; set; }

        public ImportRule(ImportRule record)
        {
            ImportType = ImportType.AddAsNew;
            CopyFrom(record);
        }

        public ImportRule(Contact newContact)
        {
            ImportType = ImportType.AddAsNew;
            NewContact = newContact;
        }

        public void CopyFrom(ImportRule record)
        {
            NewContact = record.NewContact;
            ImportType = record.ImportType;
            OriginalContact = record.OriginalContact;
        }
    }
}
