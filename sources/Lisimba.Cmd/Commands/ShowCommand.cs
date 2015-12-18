using System;
using System.Globalization;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd.Commands
{
    class ShowCommand : ICommand
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public ShowCommand(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
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
            var contacts = domainData.AddressBook.Contacts
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
            foreach (Contact contact in domainData.AddressBook.Contacts)
            {
                consoleView.DisplayContactShort(contact);
            }
        }
    }
}