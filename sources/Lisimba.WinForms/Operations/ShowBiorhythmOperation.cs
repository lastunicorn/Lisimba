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
using DustInTheWind.Lisimba.WinForms.Properties;
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms.Operations
{
    internal class ShowBiorhythmOperation : OperationBase<object>
    {
        private readonly AddressBooks addressBooks;

        public override string ShortDescription
        {
            get { return LocalizedResources.BiorhythmOperationDescription; }
        }

        public ShowBiorhythmOperation(AddressBooks addressBooks, WindowSystem windowSystem)
            : base(windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");

            this.addressBooks = addressBooks;

            addressBooks.ContactChanged += HandleContactChanged;

            IsEnabled = CalculateEnableState();
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsEnabled = CalculateEnableState();
        }

        private bool CalculateEnableState()
        {
            return addressBooks.CurrentContact != null &&
                   addressBooks.CurrentContact.Birthday.IsCompleteDate;
        }

        protected override void DoExecute(object parameter)
        {
            windowSystem.DisplayBiorhythm(addressBooks.CurrentContact);
        }
    }
}