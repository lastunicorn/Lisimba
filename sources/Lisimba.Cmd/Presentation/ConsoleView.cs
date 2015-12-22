using System;
using System.Reflection;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using Lisimba.Cmd.Common;

namespace Lisimba.Cmd.Presentation
{
    class ConsoleView
    {
        #region Common

        public void DisplayNoAddressBookMessage()
        {
            ConsoleHelper.WriteLineError("No address book is oppened.");
        }

        public void WriteError(string text)
        {
            ConsoleHelper.WriteLineError(text);
        }

        #endregion

        #region Application Main Loop

        public void WriteWelcomeMessage()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            ConsoleHelper.WriteLineEmphasize("Lisimba ver. " + version);
        }

        public void WriteGoodByeMessage()
        {
            Console.WriteLine("See you soon!");
        }

        public void WriteGateInfo(string gateName)
        {
            Console.WriteLine("DefaultGate: {0}", gateName);
        }

        #endregion

        #region Unknown Flow

        public void DisplayUnknownCommandError()
        {
            ConsoleHelper.WriteLineError("Unknown command");
        }

        #endregion

        #region Open Flow

        public void DisplayAddressBookOpenSuccess(string addressBookFileName, int contactsCount)
        {
            string message = string.Format("Successfully loaded {0} contacts from file '{1}'.", contactsCount, addressBookFileName);
            ConsoleHelper.WriteLineSuccess(message);
        }

        #endregion

        #region Close Flow

        public void DisplayAddressBookCloseSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book was closed.");
        }

        #endregion

        #region New Flow

        public void DisplayAddressBookCreateSuccess()
        {
            ConsoleHelper.WriteLineSuccess("New address book successfully created.");
        }

        #endregion

        #region NextBirthdays Flow

        public void DisplayContactWithBirthday(Contact contact)
        {
            string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
            Console.WriteLine(text);
        }

        #endregion

        #region Show Flow

        public void DisplayContactDetails(Contact contact)
        {
            Console.WriteLine(contact.Name.ToString());
        }

        public void DisplayContactShort(Contact contact)
        {
            Console.WriteLine(contact.ToString());
        }

        #endregion

        #region Update Flow

        public void DisplayInvalidUpdateActionError()
        {
            ConsoleHelper.WriteLineError("Invalid update action.");
        }

        public void DisplayAddressBookNameChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("Address book name successfully changed.");
        }

        #endregion

        #region Info Flow

        public void DisplayAddressBookInfo(AddressBook addressBook, string addressBookLocation)
        {
            Console.WriteLine();

            ConsoleHelper.WriteEmphasize("Address book: ");
            Console.WriteLine(addressBook.Name);

            ConsoleHelper.WriteEmphasize("Location: ");
            Console.WriteLine(addressBookLocation);

            ConsoleHelper.WriteEmphasize("Contacts: ");
            Console.WriteLine(addressBook.Contacts.Count);
        }

        #endregion

        #region Gate Flow

        public void DisplayGate(IGate gate)
        {
            Console.WriteLine();

            ConsoleHelper.WriteEmphasize("DefaultGate: ");
            Console.WriteLine("{0} ({1})", gate.Name, gate.Id);

            ConsoleHelper.WriteEmphasize("Description: ");
            Console.WriteLine(gate.Description);
        }

        public void DisplayGateChangeSuccess()
        {
            ConsoleHelper.WriteLineSuccess("The gate was successfully changed.");
        }

        #endregion

        #region Save Flow

        public void DisplayAddressBookSaveSuccess()
        {
            ConsoleHelper.WriteSuccess("Address book was successfully saved.");
        }

        #endregion
    }
}