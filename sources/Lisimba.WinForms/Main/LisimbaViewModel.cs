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
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.ContactList;
using DustInTheWind.Lisimba.MainMenu;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Main
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableOperations availableOperations;
        private readonly AvailableGates availableGates;
        private readonly UserInterface userInterface;
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly ApplicationStatus applicationStatus;
        private readonly ApplicationBackEnd applicationBackEnd;

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
            ApplicationBackEnd applicationBackEnd, ApplicationStatus applicationStatus, OpenedAddressBooks openedAddressBooks,
            AvailableOperations availableOperations, AvailableGates availableGates, UserInterface userInterface,
            MainMenusViewModels mainMenusViewModels, MenuItemViewModelProvider viewModelProvider)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (applicationBackEnd == null) throw new ArgumentNullException("applicationBackEnd");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (mainMenusViewModels == null) throw new ArgumentNullException("mainMenusViewModels");
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");

            this.applicationBackEnd = applicationBackEnd;
            this.applicationStatus = applicationStatus;
            this.openedAddressBooks = openedAddressBooks;
            this.availableOperations = availableOperations;
            this.availableGates = availableGates;
            this.userInterface = userInterface;
            this.viewModelProvider = viewModelProvider;

            MainMenusViewModels = mainMenusViewModels;
            ContactListViewModel = contactListViewModel;
            ContactEditorViewModel = contactEditorViewModel;

            NewAddressBookViewModel = CreateViewModel<NewAddressBookOperation>();
            OpenAddressBookViewModel = CreateViewModel<OpenAddressBookOperation>();

            ToolStripNewAddressBookViewModel = CreateViewModel<NewAddressBookOperation>();
            ToolStripOpenAddressBookViewModel = CreateViewModel<OpenAddressBookOperation>();
            ToolStripSaveAddressBookViewModel = CreateViewModel<SaveAddressBookOperation>();
            ToolStripAboutViewModel = CreateViewModel<ShowAboutOperation>();

            availableGates.GateChanged += HandleDefaultGateChanged;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.ContactChanged += HandleContactChanged;
            openedAddressBooks.AddressBookClosing += HandleAddressBooksClosing;
            openedAddressBooks.AddressBookOpened += HandleAddressBooksOpened;

            if (openedAddressBooks.Current != null)
                openedAddressBooks.Current.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            applicationStatus.StatusTextChanged += HandleStatusTextChanged;

            IsContactEditVisible = openedAddressBooks.Contact != null;
            IsAddressBookViewVisible = openedAddressBooks.Current != null;

            DefaultGate = availableGates.DefaultGate == null
                ? string.Empty
                : availableGates.DefaultGate.Name;

            StatusText = applicationStatus.StatusText;
            Title = BuildFormTitle();
        }

        private CustomButtonViewModel CreateViewModel<T>()
            where T : class, IOperation
        {
            T newAddressBookOperation = availableOperations.GetOperation<T>();
            return viewModelProvider.GetNewViewModel<CustomButtonViewModel>(newAddressBookOperation);
        }

        private void HandleDefaultGateChanged(object sender, EventArgs e)
        {
            DefaultGate = availableGates.DefaultGate == null
                ? string.Empty
                : availableGates.DefaultGate.Name;
        }

        private void HandleAddressBooksOpened(object sender, EventArgs e)
        {
            openedAddressBooks.Current.StatusChanged += HandleAddressBookStatusChanged;
        }

        private void HandleAddressBooksClosing(object sender, EventArgs e)
        {
            openedAddressBooks.Current.StatusChanged -= HandleAddressBookStatusChanged;
        }

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = openedAddressBooks.Contact != null;
            ContactEditorViewModel.Contact = openedAddressBooks.Contact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
                e.OldAddressBook.AddressBook.Changed -= HandleCurrentAddressBookContentChanged;

            if (e.NewAddressBook != null)
                e.NewAddressBook.AddressBook.Changed += HandleCurrentAddressBookContentChanged;

            Title = BuildFormTitle();
            IsAddressBookViewVisible = openedAddressBooks.Current != null;
        }

        private void HandleAddressBookStatusChanged(object sender, EventArgs e)
        {
            Title = BuildFormTitle();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs e)
        {
            Title = BuildFormTitle();
        }

        private void HandleStatusTextChanged(object sender, EventArgs e)
        {
            StatusText = applicationStatus.StatusText;
        }

        private string BuildFormTitle()
        {
            if (openedAddressBooks.Current == null)
                return applicationBackEnd.ProgramName;

            string addressBookName = openedAddressBooks.Current.GetFriendlyName();
            bool isModified = openedAddressBooks.Current != null && openedAddressBooks.Current.Status == AddressBookStatus.Modified;
            string unsavedSign = isModified ? " *" : string.Empty;
            string programName = applicationBackEnd.ProgramName;

            return string.Format("{0}{1} - {2}", addressBookName, unsavedSign, programName);
        }

        public bool WindowIsClosing()
        {
            return true;
        }

        public void DefaultGateWasClicked(Point point)
        {
            userInterface.ShowGateSelector(point);
        }
    }
}