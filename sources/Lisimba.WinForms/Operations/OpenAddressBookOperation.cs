﻿// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Common.AddressBookManagement;
using DustInTheWind.Lisimba.Common.GateManagement;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class OpenAddressBookOperation : ExecutableViewModelBase<string>
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly UserInterface userInterface;
        private readonly AvailableGates availableGates;

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenAddressBookOperationDescription; }
        }

        public OpenAddressBookOperation(OpenedAddressBooks openedAddressBooks, UserInterface userInterface,
            ApplicationStatus applicationStatus, AvailableGates availableGates)
            : base(applicationStatus)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.openedAddressBooks = openedAddressBooks;
            this.userInterface = userInterface;
            this.availableGates = availableGates;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = userInterface.AskToOpenLsbFile();

                    if (fileName == null)
                        return;
                }

                openedAddressBooks.OpenAddressBook(fileName, availableGates.DefaultGate);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }
    }
}