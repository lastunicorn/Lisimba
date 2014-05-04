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
