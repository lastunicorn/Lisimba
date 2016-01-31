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
using DustInTheWind.Lisimba.MainMenu;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.Main
{
    class MenuItemViewModels
    {
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly AvailableOperations availableOperations;

        public CustomMenuItemViewModel NewAddressBookViewModel { get; private set; }
        public CustomMenuItemViewModel OpenAddressBookViewModel { get; private set; }
        public CustomMenuItemViewModel SaveAddressBookViewModel { get; private set; }
        public CustomMenuItemViewModel SaveAsAddressBookViewModel { get; private set; }
        public CustomMenuItemViewModel CloseAddressBookViewModel { get; private set; }
        public CustomMenuItemViewModel ApplicationExitViewModel { get; private set; }
        public CustomMenuItemViewModel NewContactViewModel { get; private set; }
        public CustomMenuItemViewModel DeleteContactViewModel { get; private set; }
        public CustomMenuItemViewModel AddressBookPropertiesViewModel { get; private set; }
        public CustomMenuItemViewModel AboutViewModel { get; private set; }
        public CustomMenuItemViewModel ExportViewModel { get; private set; }
        public CustomMenuItemViewModel ImportViewModel { get; private set; }
        public RecentFilesMenuItemViewModel RecentFilesViewModel { get; private set; }

        public MenuItemViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
        {
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");

            this.viewModelProvider = viewModelProvider;
            this.availableOperations = availableOperations;

            NewAddressBookViewModel = CreateViewModel<NewAddressBookOperation>();
            OpenAddressBookViewModel = CreateViewModel<OpenAddressBookOperation>();
            SaveAddressBookViewModel = CreateViewModel<SaveAddressBookOperation>();
            SaveAsAddressBookViewModel = CreateViewModel<SaveAsAddressBookOperation>();
            CloseAddressBookViewModel = CreateViewModel<CloseAddressBookOperation>();
            ApplicationExitViewModel = CreateViewModel<ApplicationExitOperation>();
            NewContactViewModel = CreateViewModel<NewContactOperation>();
            DeleteContactViewModel = CreateViewModel<DeleteCurrentContactOperation>();
            AddressBookPropertiesViewModel = CreateViewModel<ShowAddressBookPropertiesOperation>();
            AboutViewModel = CreateViewModel<ShowAboutOperation>();
            ExportViewModel = CreateEmptyViewModel("Export current opened address book in another format.");
            ImportViewModel = CreateEmptyViewModel("Import address book from another format.");

            RecentFilesViewModel = CreateRecentFilesViewModel();
        }

        private CustomMenuItemViewModel CreateViewModel<T>()
            where T : class, IExecutableViewModel
        {
            T newAddressBookOperation = availableOperations.GetOperation<T>();
            return viewModelProvider.GetNewViewModel(newAddressBookOperation);
        }

        private RecentFilesMenuItemViewModel CreateRecentFilesViewModel()
        {
            EmptyOperation operation = new EmptyOperation { ShortDescription = "Open previously closed address books." };
            RecentFilesMenuItemViewModel viewModel = viewModelProvider.GetViewModel<RecentFilesMenuItemViewModel>(operation);
            viewModel.ChildrenOpertion = availableOperations.GetOperation<OpenAddressBookOperation>();
            return viewModel;
        }

        private CustomMenuItemViewModel CreateEmptyViewModel(string description)
        {
            EmptyOperation operation = new EmptyOperation { ShortDescription = description };
            return viewModelProvider.GetNewViewModel(operation);
        }
    }
}