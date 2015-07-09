﻿// Lisimba
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
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class CreateNewAddressBookCommand : CommandBase<string>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;
        private readonly ApplicationStatus applicationStatus;

        public override string ShortDescription
        {
            get { return "Create a new address book."; }
        }

        public CreateNewAddressBookCommand(AddressBookShell addressBookShell, UiService uiService, ApplicationStatus applicationStatus)
        {
            if (addressBookShell == null)
                throw new ArgumentNullException("addressBookShell");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            if (applicationStatus == null)
                throw new ArgumentNullException("applicationStatus");

            this.addressBookShell = addressBookShell;
            this.uiService = uiService;
            this.applicationStatus = applicationStatus;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                addressBookShell.LoadNew();
                applicationStatus.StatusText = "A new address book was created.";
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}
