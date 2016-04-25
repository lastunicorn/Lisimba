using System;
using DustInTheWind.Lisimba.Wpf.Commands;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    class StartViewModel : ViewModelBase
    {
        public NewAddressBookCommand NewAddressBookCommand { get; private set; }
        public OpenAddressBookCommand OpenAddressBookCommand { get; private set; }

        public StartViewModel(NewAddressBookCommand newAddressBookCommand, OpenAddressBookCommand openAddressBookCommand)
        {
            if (newAddressBookCommand == null) throw new ArgumentNullException("newAddressBookCommand");
            if (openAddressBookCommand == null) throw new ArgumentNullException("openAddressBookCommand");

            NewAddressBookCommand = newAddressBookCommand;
            OpenAddressBookCommand = openAddressBookCommand;
        }
    }
}
