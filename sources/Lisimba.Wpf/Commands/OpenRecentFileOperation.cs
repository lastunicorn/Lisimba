﻿// Lisimba
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
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms.Operations
{
    internal class OpenRecentFileOperation : OperationBase<AddressBookLocationInfo>
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenAddressBookOperationDescription; }
        }

        public OpenRecentFileOperation(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem, AvailableGates availableGates)
            : base(windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
        }

        protected override void DoExecute(AddressBookLocationInfo file)
        {
            if (file == null)
                return;

            IGate gate = availableGates.GetGate(file.GateId);
            openedAddressBooks.OpenAddressBook(file.FileName, gate);
        }
    }
}