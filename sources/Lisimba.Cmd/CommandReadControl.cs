using System;
using Lisimba.Cmd.Commands;

namespace Lisimba.Cmd
{
    /// <summary>
    /// Reads a command from the console and parses it.
    /// </summary>
    class CommandReadControl
    {
        private readonly DomainData domainData;
        private readonly ConsoleView consoleView;

        public CommandReadControl(DomainData domainData, ConsoleView consoleView)
        {
            if (domainData == null) throw new ArgumentNullException("domainData");
            if (consoleView == null) throw new ArgumentNullException("consoleView");

            this.domainData = domainData;
            this.consoleView = consoleView;
        }

        public CommandInfo Read()
        {
            string addressBookName = domainData.AddressBookName;
            bool isSaved = domainData.IsAddressBookSaved;

            Console.WriteLine();
            consoleView.DisplayPrompter(addressBookName, isSaved);
            
            string commandText = Console.ReadLine();

            return new CommandInfo(commandText);
        }
    }
}