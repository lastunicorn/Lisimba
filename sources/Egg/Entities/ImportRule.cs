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

using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    public class ImportRule
    {
        private ImportType importType = ImportType.AddAsNew;
        public ImportType ImportType
        {
            get { return importType; }
            set { importType = value; }
        }

        private Contact newContact = null;
        public Contact NewContact
        {
            get { return newContact; }
            set { newContact = value; }
        }

        private Contact originalContact = null;
        public Contact OriginalContact
        {
            get { return originalContact; }
            set { originalContact = value; }
        }

        public ImportRule(ImportRule record)
        {
            CopyFrom(record);
        }

        public ImportRule(Contact newContact)
        {
            this.newContact = newContact;
        }

        public ImportRule(Contact newContact, ImportType importType)
        {
            this.newContact = newContact;
            this.importType = importType;
        }

        public ImportRule(Contact newContact, ImportType importType, Contact originalContact)
        {
            this.newContact = newContact;
            this.importType = importType;
            this.originalContact = originalContact;
        }

        public void CopyFrom(ImportRule record)
        {
            newContact = record.newContact;
            importType = record.importType;
            originalContact = record.originalContact;
        }
    }
}
