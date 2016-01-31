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
using DustInTheWind.Lisimba.Main;
using DustInTheWind.Lisimba.MainMenu;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.ContactList
{
    class ContactMenuViewModels
    {
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly AvailableOperations availableOperations;

        public CustomButtonViewModel AddContactViewModel { get; private set; }
        public CustomButtonViewModel DeleteContactViewModel { get; private set; }
        public CustomButtonViewModel BiorhythmViewModel { get; private set; }

        public ContactMenuViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
        {
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");

            this.viewModelProvider = viewModelProvider;
            this.availableOperations = availableOperations;

            AddContactViewModel = CreateViewModel<NewContactOperation>();
            DeleteContactViewModel = CreateViewModel<DeleteCurrentContactOperation>();
            BiorhythmViewModel = CreateEmptyViewModel("Display the biorhythm of the selected person.");
        }

        private CustomButtonViewModel CreateViewModel<T>()
            where T : class, IOperation
        {
            T newAddressBookOperation = availableOperations.GetOperation<T>();
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(newAddressBookOperation);
        }

        private CustomButtonViewModel CreateEmptyViewModel(string description)
        {
            EmptyOperation operation = new EmptyOperation(description);
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(operation);
        }
    }
}