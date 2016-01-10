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
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class CreateNewAddressBookOperation : ExecutableViewModelBase<string>
    {
        private readonly AddressBooks addressBooks;
        private readonly UserInterface userInterface;

        public override string ShortDescription
        {
            get { return LocalizedResources.CreateNewAddressBookOperationDescription; }
        }

        public CreateNewAddressBookOperation(AddressBooks addressBooks, UserInterface userInterface, ApplicationStatus applicationStatus)
            : base(applicationStatus)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.addressBooks = addressBooks;
            this.userInterface = userInterface;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                bool succeeded = addressBooks.CreateNewAddressBook(null);

                if (succeeded)
                    applicationStatus.StatusText = "A new address book was created.";
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }
    }
}