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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.Wpf.LocationProviders;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class ImportAddressBookCommand : CommandBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly FileLocationProvider fileLocationProvider;

        public ImportAddressBookCommand(WindowSystem windowSystem, OpenedAddressBooks openedAddressBooks, FileLocationProvider fileLocationProvider)
            : base(windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");

            this.openedAddressBooks = openedAddressBooks;
            this.fileLocationProvider = fileLocationProvider;
        }

        public ImportAddressBookCommand(WindowSystem windowSystem)
            : base(windowSystem)
        {
        }

        public override string ShortDescription
        {
            get { return LocalizedResources.ImportOperationDescription; }
        }

        protected override void DoExecute(object parameter)
        {
            IGate gate = parameter as IGate;

            if (gate == null)
                return;
            
            string location = fileLocationProvider.AskToOpen();

            if (location == null)
                return;

            openedAddressBooks.Import(location, gate);

            //WindowSystem.DisplayInfo("Import using " + gate.Name + ".");
        }
    }
}