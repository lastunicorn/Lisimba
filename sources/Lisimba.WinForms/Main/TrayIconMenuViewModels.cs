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
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Main
{
    class TrayIconMenuViewModels
    {
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly AvailableOperations availableOperations;

        public CustomButtonViewModel ShowMainViewModel { get; private set; }
        public CustomButtonViewModel ApplicationExitViewModel { get; private set; }
        public CustomButtonViewModel AboutViewModel { get; private set; }

        public TrayIconMenuViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
        {
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");

            this.viewModelProvider = viewModelProvider;
            this.availableOperations = availableOperations;

            ShowMainViewModel = CreateViewModel<ShowMainOperation>();
            ApplicationExitViewModel = CreateViewModel<ApplicationExitOperation>();
            AboutViewModel = CreateViewModel<ShowAboutOperation>();
        }

        private CustomButtonViewModel CreateViewModel<T>()
            where T : class, IOperation
        {
            T newAddressBookOperation = availableOperations.GetOperation<T>();
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(newAddressBookOperation);
        }
    }
}