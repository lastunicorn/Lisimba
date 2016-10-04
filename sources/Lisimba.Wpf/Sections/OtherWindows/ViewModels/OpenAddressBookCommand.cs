// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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

using System;
using System.Text;
using System.Windows.Input;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class OpenAddressBookCommand : ICommand
    {
        private readonly WindowSystem windowSystem;
        private readonly Gates gates;
        private readonly AddressBooks addressBooks;
        public AddressBookImporter AddressBookImporter { get; private set; }

        public string Result { get; private set; }

        public event EventHandler ImportFinished;

        public OpenAddressBookCommand(WindowSystem windowSystem, Gates gates, AddressBooks addressBooks)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (gates == null) throw new ArgumentNullException("gates");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.windowSystem = windowSystem;
            this.gates = gates;
            this.addressBooks = addressBooks;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string fileName = windowSystem.AskToOpen("lsb", "");
            
            if(fileName == null)
                return;

            AddressBook addressBook = gates.DefaultGate.Load(fileName);

            AddressBook currentaddressBook = addressBooks.Current.AddressBook;

            AddressBookImporter = new AddressBookImporter(currentaddressBook, addressBook);
            AddressBookImporter.Analyse();

            StringBuilder sb = AddressBookImporter.PerformImport(true);

            Result = sb.ToString();
            OnImportFinished();
        }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnImportFinished()
        {
            EventHandler handler = ImportFinished;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}