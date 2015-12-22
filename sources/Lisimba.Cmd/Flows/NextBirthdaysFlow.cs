﻿using System;
using System.Linq;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Comparers;
using Lisimba.Cmd.Business;
using Lisimba.Cmd.Common;
using Lisimba.Cmd.Presentation;

namespace Lisimba.Cmd.Flows
{
    class NextBirthdaysFlow : IFlow
    {
        private readonly AddressBooks addressBooks;
        private readonly NextBirthdaysFlowConsole console;

        public NextBirthdaysFlow(AddressBooks addressBooks, NextBirthdaysFlowConsole console)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (console == null) throw new ArgumentNullException("console");

            this.addressBooks = addressBooks;
            this.console = console;
        }

        public void Execute(Command command)
        {
            if (command == null) throw new ArgumentNullException("command");

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
                console.DisplayContactWithBirthday(contact);
            }
        }
    }
}