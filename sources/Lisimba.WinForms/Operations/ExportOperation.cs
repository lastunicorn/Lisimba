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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class ExportOperation : OperationBase<IGate>
    {
        private readonly OpenedAddressBooks openedAddressBooks;

        public override string ShortDescription
        {
            get { return LocalizedResources.ExportOperationDescription; }
        }

        public ExportOperation(UserInterface userInterface, OpenedAddressBooks openedAddressBooks)
            : base(userInterface)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.openedAddressBooks = openedAddressBooks;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = openedAddressBooks.Current != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = openedAddressBooks.Current != null;
        }

        protected override void DoExecute(IGate gate)
        {
            if (openedAddressBooks.Current == null)
                throw new LisimbaException(LocalizedResources.NoAddessBookOpenedError);

            string newLocation = userInterface.AskToSaveLsbFile();

            if (newLocation == null)
                return;

            openedAddressBooks.Current.Export(newLocation, gate);
        }
    }
}