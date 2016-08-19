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
using System.Drawing;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.WinForms.ContactEdit;
using DustInTheWind.Lisimba.WinForms.ContactList;
using DustInTheWind.Lisimba.WinForms.MainMenu;
using DustInTheWind.Lisimba.WinForms.Operations;
using DustInTheWind.Lisimba.WinForms.Services;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Controls;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.WinForms.Main
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly AddressBooks addressBooks;
        private readonly AvailableOperations availableOperations;
        private readonly Gates gates;
        private readonly WindowSystem windowSystem;
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly LisimbaWindowTitle lisimbaWindowTitle;
        private readonly ApplicationStatus applicationStatus;

        private string title;
        private string statusText;
        private bool isContactEditVisible;
        private bool isAddressBookViewVisible;
        private string defaultGate;

        public MainMenusViewModels MainMenusViewModels { get; private set; }
        public ContactListViewModel ContactListViewModel { get; private set; }
        public ContactEditorViewModel ContactEditorViewModel { get; private set; }
        public CustomButtonViewModel NewAddressBookViewModel { get; private set; }
        public CustomButtonViewModel OpenAddressBookViewModel { get; private set; }
        public CustomButtonViewModel ToolStripNewAddressBookViewModel { get; private set; }
        public CustomButtonViewModel ToolStripOpenAddressBookViewModel { get; private set; }
        public CustomButtonViewModel ToolStripSaveAddressBookViewModel { get; private set; }
        public CustomButtonViewModel ToolStripUndoViewModel { get; private set; }
        public CustomButtonViewModel ToolStripRedoViewModel { get; private set; }
        public CustomButtonViewModel ToolStripAboutViewModel { get; private set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get { return statusText; }
            set
            {
                statusText = value;
                OnPropertyChanged();
            }
        }

        public bool IsContactEditVisible
        {
            get { return isContactEditVisible; }
            set
            {
                isContactEditVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddressBookViewVisible
        {
            get { return isAddressBookViewVisible; }
            set
            {
                isAddressBookViewVisible = value;
                OnPropertyChanged();
            }
        }

        public string DefaultGate
        {
            get { return defaultGate; }
            set
            {
                defaultGate = value;
                OnPropertyChanged();
            }
        }

        public LisimbaViewModel(ContactListViewModel contactListViewModel, ContactEditorViewModel contactEditorViewModel,
            ApplicationStatus applicationStatus, AddressBooks addressBooks,
            AvailableOperations availableOperations, Gates gates, WindowSystem windowSystem,
            MainMenusViewModels mainMenusViewModels, MenuItemViewModelProvider viewModelProvider, LisimbaWindowTitle lisimbaWindowTitle)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");
            if (gates == null) throw new ArgumentNullException("gates");
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");
            if (mainMenusViewModels == null) throw new ArgumentNullException("mainMenusViewModels");
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (lisimbaWindowTitle == null) throw new ArgumentNullException("lisimbaWindowTitle");

            this.applicationStatus = applicationStatus;
            this.addressBooks = addressBooks;
            this.availableOperations = availableOperations;
            this.gates = gates;
            this.windowSystem = windowSystem;
            this.viewModelProvider = viewModelProvider;
            this.lisimbaWindowTitle = lisimbaWindowTitle;

            MainMenusViewModels = mainMenusViewModels;
            ContactListViewModel = contactListViewModel;
            ContactEditorViewModel = contactEditorViewModel;

            NewAddressBookViewModel = CreateViewModel<NewAddressBookOperation>();
            OpenAddressBookViewModel = CreateViewModel<OpenAddressBookOperation>();

            ToolStripNewAddressBookViewModel = CreateViewModel<NewAddressBookOperation>();
            ToolStripOpenAddressBookViewModel = CreateViewModel<OpenAddressBookOperation>();
            ToolStripSaveAddressBookViewModel = CreateViewModel<SaveAddressBookOperation>();
            ToolStripUndoViewModel = CreateViewModel<UndoOperation>();
            ToolStripRedoViewModel = CreateViewModel<RedoOperation>();
            ToolStripAboutViewModel = CreateViewModel<ShowAboutOperation>();

            gates.GateChanged += HandleDefaultGateChanged;

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBooks.ContactChanged += HandleContactChanged;
            
            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            IsContactEditVisible = addressBooks.CurrentContact != null;
            IsAddressBookViewVisible = addressBooks.Current != null;

            DefaultGate = gates.DefaultGate == null
                ? string.Empty
                : gates.DefaultGate.Name;

            StatusText = applicationStatus.StatusText;

            lisimbaWindowTitle.ValueChanged += HandleLisimbaTitleValueChanged;
            Title = lisimbaWindowTitle.Value;
        }

        private void HandleLisimbaTitleValueChanged(object sender, EventArgs e)
        {
            Title = lisimbaWindowTitle.Value;
        }

        private CustomButtonViewModel CreateViewModel<T>()
            where T : class, IOperation
        {
            T newAddressBookOperation = availableOperations.GetOperation<T>();
            return viewModelProvider.CreateNew<CustomButtonViewModel>(newAddressBookOperation);
        }

        private void HandleDefaultGateChanged(object sender, EventArgs e)
        {
            DefaultGate = gates.DefaultGate == null
                ? string.Empty
                : gates.DefaultGate.Name;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = addressBooks.CurrentContact != null;
            ContactEditorViewModel.ActionQueue = addressBooks.Current.ActionQueue;
            ContactEditorViewModel.Contact = addressBooks.CurrentContact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsAddressBookViewVisible = addressBooks.Current != null;
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = applicationStatus.StatusText;
        }

        public bool WindowIsClosing()
        {
            return true;
        }

        public void DefaultGateWasClicked(Point point)
        {
            windowSystem.ShowGateSelector(point);
        }
    }
}