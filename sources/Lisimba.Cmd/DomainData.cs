using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Gating;

namespace Lisimba.Cmd
{
    class DomainData
    {
        public AddressBook AddressBook { get; private set; }
        public IGate DefaultGate { get; private set; }

        public bool ExitRequested { get; set; }

        public DomainData()
        {
            DefaultGate = new ZipXmlGate();
        }

        public void LoadAddressBook(string fileName)
        {
            AddressBook = DefaultGate.Load(fileName ?? "agenda.lsb");
        }

        public string GetAddressBookName()
        {
            return AddressBook == null ? null : AddressBook.Name;
        }
    }
}