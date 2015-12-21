using System;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Comparers;
using Lisimba.Cmd.CommandSystem;
using Lisimba.Cmd.Data;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Commands
{
    class NextBirthdaysCommand : ICommand
    {
        private readonly AddressBooks addressBooks;
        private readonly ConsoleView consoleView;

        public NextBirthdaysCommand(AddressBooks addressBooks, ConsoleView consoleView)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.addressBooks = addressBooks;
            this.consoleView = consoleView;
        }

        public void Execute(CommandInfo commandInfo)
        {
            if (commandInfo == null) throw new ArgumentNullException("commandInfo");

            DisplayNextBirthdays();
        }

        private void DisplayNextBirthdays()
        {
            var contacts = addressBooks.AddressBook.Contacts
                .Where(x => x.Birthday != null)
                .Where(x => Date.CompareWithoutYear(x.Birthday, DateTime.Today) >= 0)
                .OrderBy(x => x, new ContactByBirthdayComparer())
                .Take(10);

            foreach (Contact contact in contacts)
            {
                consoleView.DisplayContactWithBirthday(contact);
            }
        }
    }
}