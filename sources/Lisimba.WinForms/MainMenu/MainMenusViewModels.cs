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
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.MainMenu
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
        public CustomButtonViewModel UndoViewModel { get; private set; }
        public CustomButtonViewModel RedoViewModel { get; private set; }
        public CustomButtonViewModel AboutViewModel { get; private set; }
        public ListMenuItemViewModel ExportViewModel { get; private set; }
        public ImportsMenuItemViewModel ImportViewModel { get; private set; }
        public RecentFilesMenuItemViewModel RecentFilesViewModel { get; private set; }

        public MainMenusViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
        {
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");

            this.viewModelProvider = viewModelProvider;
            this.availableOperations = availableOperations;

            CreateViewModels();
        }

        private void CreateViewModels()
        {
            NewAddressBookOperation operation = availableOperations.GetOperation<NewAddressBookOperation>();
            NewAddressBookViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation);

            OpenAddressBookOperation operation1 = availableOperations.GetOperation<OpenAddressBookOperation>();
            OpenAddressBookViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation1);

            SaveAddressBookOperation operation2 = availableOperations.GetOperation<SaveAddressBookOperation>();
            SaveAddressBookViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation2);

            SaveAsAddressBookOperation operation3 = availableOperations.GetOperation<SaveAsAddressBookOperation>();
            SaveAsAddressBookViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation3);

            CloseAddressBookOperation operation4 = availableOperations.GetOperation<CloseAddressBookOperation>();
            CloseAddressBookViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation4);

            ApplicationExitOperation operation5 = availableOperations.GetOperation<ApplicationExitOperation>();
            ApplicationExitViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation5);

            NewContactOperation operation6 = availableOperations.GetOperation<NewContactOperation>();
            NewContactViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation6);

            DeleteCurrentContactOperation operation7 = availableOperations.GetOperation<DeleteCurrentContactOperation>();
            DeleteContactViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation7);

            ShowAddressBookPropertiesOperation operation8 = availableOperations.GetOperation<ShowAddressBookPropertiesOperation>();
            AddressBookPropertiesViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation8);

            UndoOperation operationUndo = availableOperations.GetOperation<UndoOperation>();
            UndoViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operationUndo);

            RedoOperation operationRedo = availableOperations.GetOperation<RedoOperation>();
            RedoViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operationRedo);

            ShowAboutOperation operationAbout = availableOperations.GetOperation<ShowAboutOperation>();
            AboutViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operationAbout);

            EmptyOperation operation10 = new EmptyOperation(LocalizedResources.ExportsOperationDescription);
            ExportViewModel = viewModelProvider.CreateNew<ExportsMenuItemViewModel>(operation10);
            ExportViewModel.ChildrenOpertion = availableOperations.GetOperation<ExportOperation>();

            EmptyOperation operation11 = new EmptyOperation(LocalizedResources.ImportsOperationDescription);
            ImportViewModel = viewModelProvider.CreateNew<ImportsMenuItemViewModel>(operation11);
            ImportViewModel.ChildrenOpertion = availableOperations.GetOperation<ImportOperation>();

            EmptyOperation operation12 = new EmptyOperation(LocalizedResources.RecentFilesOperationDescription);
            RecentFilesViewModel = viewModelProvider.CreateNew<RecentFilesMenuItemViewModel>(operation12);
            RecentFilesViewModel.ChildrenOpertion = availableOperations.GetOperation<OpenRecentFileOperation>();
        }
    }
}