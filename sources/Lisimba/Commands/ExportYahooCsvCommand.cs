// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg.BookShell;
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class ExportYahooCsvCommand : CommandBase<object>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;

        public override string ShortDescription
        {
            get { return "Export current opened address book in Yahoo! csv format."; }
        }

        public ExportYahooCsvCommand(AddressBookShell addressBookShell, UiService uiService)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.addressBookShell = addressBookShell;
            this.uiService = uiService;

            addressBookShell.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = addressBookShell.AddressBook != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = addressBookShell.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                string fileName = uiService.AskToSaveYahooCsvFile();

                if (fileName == null)
                    return;

                YahooCsvGate gate = new YahooCsvGate();
                addressBookShell.ExportTo(gate, fileName);
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}
