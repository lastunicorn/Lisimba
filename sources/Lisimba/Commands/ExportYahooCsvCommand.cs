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
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class ExportYahooCsvCommand : CommandBase<object>
    {
        private readonly CurrentData currentData;
        private readonly UiService uiService;

        public override string ShortDescription
        {
            get { return "Export current opened address book in Yahoo! csv format."; }
        }

        public ExportYahooCsvCommand(CurrentData currentData, UiService uiService)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.currentData = currentData;
            this.uiService = uiService;

            currentData.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = currentData.AddressBook != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = currentData.AddressBook != null;
        }

        protected override void DoExecute(object parameter)
        {
            try
            {
                string fileName = uiService.AskToSaveYahooCsvFile();

                if (fileName == null)
                    return;

                YahooCsvGate gate = new YahooCsvGate();
                gate.Save(currentData.AddressBook, fileName);
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}
