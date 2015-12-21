using System;
using System.Globalization;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class ShowCommand : ICommand
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public ShowCommand(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            if (commandInfo.HasParameters)
                DisplayContactDetails(commandInfo[1]);
            else
                DisplayAllContacts();
        }

        private void DisplayContactDetails(string contactName)
        {
            var contacts = addressBooks.AddressBook.Contacts
                .Where(x =>
                    (x.Name.FirstName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.FirstName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.MiddleName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.MiddleName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.LastName != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.LastName, contactName, CompareOptions.IgnoreCase) >= 0) ||
                    (x.Name.Nickname != null && CultureInfo.InvariantCulture.CompareInfo.IndexOf(x.Name.Nickname, contactName, CompareOptions.IgnoreCase) >= 0));

            foreach (Contact contact in contacts)
            {
                consoleView.DisplayContactDetails(contact);
            }
        }

        private void DisplayAllContacts()
        {
            foreach (Contact contact in addressBooks.AddressBook.Contacts)
            {
                consoleView.DisplayContactShort(contact);
            }
        }
    }
}