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
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Main
{
    class MainMenusViewModels
    {
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly AvailableOperations availableOperations;

        public CustomButtonViewModel NewAddressBookViewModel { get; private set; }
        public CustomButtonViewModel OpenAddressBookViewModel { get; private set; }
        public CustomButtonViewModel SaveAddressBookViewModel { get; private set; }
        public CustomButtonViewModel SaveAsAddressBookViewModel { get; private set; }
        public CustomButtonViewModel CloseAddressBookViewModel { get; private set; }
        public CustomButtonViewModel ApplicationExitViewModel { get; private set; }
        public CustomButtonViewModel NewContactViewModel { get; private set; }
        public CustomButtonViewModel DeleteContactViewModel { get; private set; }
        public CustomButtonViewModel AddressBookPropertiesViewModel { get; private set; }
        public CustomButtonViewModel AboutViewModel { get; private set; }
        public CustomButtonViewModel ExportViewModel { get; private set; }
        public CustomButtonViewModel ImportViewModel { get; private set; }
        public RecentFilesMenuItemViewModel RecentFilesViewModel { get; private set; }

        public MainMenusViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
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

        private CustomButtonViewModel CreateViewModel<TOperation>()
            where TOperation : class, IOperation
        {
            TOperation operation = availableOperations.GetOperation<TOperation>();
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(operation);
        }

        private RecentFilesMenuItemViewModel CreateRecentFilesViewModel()
        {
            EmptyOperation operation = new EmptyOperation("Open previously closed address books.");
            RecentFilesMenuItemViewModel viewModel = viewModelProvider.GetNewViewModel<RecentFilesMenuItemViewModel>(operation);
            viewModel.ChildrenOpertion = availableOperations.GetOperation<OpenAddressBookOperation>();
            return viewModel;
        }

        private CustomButtonViewModel CreateEmptyViewModel(string description)
        {
            EmptyOperation operation = new EmptyOperation(description);
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(operation);
        }
    }
}