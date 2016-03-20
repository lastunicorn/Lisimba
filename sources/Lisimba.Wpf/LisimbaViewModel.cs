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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Wpf.Commands;
using DustInTheWind.Lisimba.Wpf.Operations;

namespace DustInTheWind.Lisimba.Wpf
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableCommands availableCommands;
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly LisimbaWindowTitle lisimbaWindowTitle;

        private string title;
        private bool isContactEditVisible;
        private bool isAddressBookViewVisible;

        public ApplicationExitCommand ApplicationExitCommand { get; private set; }
        public NewAddressBookCommand NewAddressBookCommand { get; private set; }
        public OpenAddressBookCommand OpenAddressBookCommand { get; private set; }

        //public MainMenusViewModels MainMenusViewModels { get; private set; }
        //public ContactListViewModel ContactListViewModel { get; private set; }
        //public ContactEditorViewModel ContactEditorViewModel { get; private set; }
        //public CustomButtonViewModel NewAddressBookViewModel { get; private set; }
        //public CustomButtonViewModel OpenAddressBookViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripNewAddressBookViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripOpenAddressBookViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripSaveAddressBookViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripUndoViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripRedoViewModel { get; private set; }
        //public CustomButtonViewModel ToolStripAboutViewModel { get; private set; }

        public StatusBarViewModel StatusBarViewModel { get; private set; }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
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

        public LisimbaViewModel(ContactListViewModel contactListViewModel, ContactEditorViewModel contactEditorViewModel,
            OpenedAddressBooks openedAddressBooks, AvailableCommands availableCommands, MainMenusViewModels mainMenusViewModels,
            MenuItemViewModelProvider viewModelProvider, StatusBarViewModel statusBarViewModel, LisimbaWindowTitle lisimbaWindowTitle)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableCommands == null) throw new ArgumentNullException("availableCommands");
            if (mainMenusViewModels == null) throw new ArgumentNullException("mainMenusViewModels");
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (statusBarViewModel == null) throw new ArgumentNullException("statusBarViewModel");

            this.openedAddressBooks = openedAddressBooks;
            this.availableCommands = availableCommands;
            this.viewModelProvider = viewModelProvider;
            this.lisimbaWindowTitle = lisimbaWindowTitle;

            StatusBarViewModel = statusBarViewModel;

            //MainMenusViewModels = mainMenusViewModels;
            //ContactListViewModel = contactListViewModel;
            //ContactEditorViewModel = contactEditorViewModel;

            ApplicationExitCommand = availableCommands.GetCommand<ApplicationExitCommand>();
            NewAddressBookCommand = availableCommands.GetCommand<NewAddressBookCommand>();
            OpenAddressBookCommand = availableCommands.GetCommand<OpenAddressBookCommand>();

            //NewAddressBookViewModel = CreateViewModel<NewAddressBookCommand>();
            //OpenAddressBookViewModel = CreateViewModel<OpenAddressBookCommand>();

            //ToolStripNewAddressBookViewModel = CreateViewModel<NewAddressBookCommand>();
            //ToolStripOpenAddressBookViewModel = CreateViewModel<OpenAddressBookCommand>();
            //ToolStripSaveAddressBookViewModel = CreateViewModel<SaveAddressBookOperation>();
            //ToolStripUndoViewModel = CreateViewModel<UndoOperation>();
            //ToolStripRedoViewModel = CreateViewModel<RedoOperation>();
            //ToolStripAboutViewModel = CreateViewModel<ShowAboutOperation>();

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            openedAddressBooks.ContactChanged += HandleContactChanged;
            lisimbaWindowTitle.ValueChanged += HandleLisimbaTitleValueChanged;

            Title = lisimbaWindowTitle.Value;
        }

        private void HandleLisimbaTitleValueChanged(object sender, EventArgs eventArgs)
        {
            Title = lisimbaWindowTitle.Value;
        }

        //private CustomButtonViewModel CreateViewModel<T>()
        //    where T : class, IOperation
        //{
        //    T newAddressBookOperation = AvailableCommands.GetCommand<T>();
        //    return viewModelProvider.CreateNew<CustomButtonViewModel>(newAddressBookOperation);
        //}

        private void HandleContactChanged(object sender, EventArgs e)
        {
            IsContactEditVisible = openedAddressBooks.CurrentContact != null;
            //ContactEditorViewModel.ActionQueue = openedAddressBooks.Current.ActionQueue;
            //ContactEditorViewModel.Contact = openedAddressBooks.CurrentContact;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsAddressBookViewVisible = openedAddressBooks.Current != null;
        }

        public bool WindowIsClosing()
        {
            return true;
        }
    }
}