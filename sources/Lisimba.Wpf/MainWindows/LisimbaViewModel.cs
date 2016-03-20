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

namespace DustInTheWind.Lisimba.Wpf.Main
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
        public SaveAddressBookCommand SaveAddressBookCommand { get; private set; }
        public SaveAsAddressBookCommand SaveAsAddressBookCommand { get; private set; }
        public CloseAddressBookCommand CloseAddressBookCommand { get; private set; }
        public ShowAddressBookPropertiesCommand ShowAddressBookPropertiesCommand { get; private set; }
        public ShowAboutCommand ShowAboutCommand { get; private set; }
        public NewContactCommand NewContactCommand { get; private set; }
        public DeleteCurrentContactCommand DeleteCurrentContactCommand { get; private set; }
        public UndoCommand UndoCommand { get; private set; }
        public RedoCommand RedoCommand { get; private set; }

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

        public LisimbaStatusBarViewModel LisimbaStatusBarViewModel { get; private set; }

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
            MenuItemViewModelProvider viewModelProvider, LisimbaStatusBarViewModel lisimbaStatusBarViewModel, LisimbaWindowTitle lisimbaWindowTitle)
        {
            if (contactListViewModel == null) throw new ArgumentNullException("contactListViewModel");
            if (contactEditorViewModel == null) throw new ArgumentNullException("contactEditorViewModel");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableCommands == null) throw new ArgumentNullException("availableCommands");
            if (mainMenusViewModels == null) throw new ArgumentNullException("mainMenusViewModels");
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (lisimbaStatusBarViewModel == null) throw new ArgumentNullException("lisimbaStatusBarViewModel");

            this.openedAddressBooks = openedAddressBooks;
            this.availableCommands = availableCommands;
            this.viewModelProvider = viewModelProvider;
            this.lisimbaWindowTitle = lisimbaWindowTitle;

            LisimbaStatusBarViewModel = lisimbaStatusBarViewModel;

            //MainMenusViewModels = mainMenusViewModels;
            //ContactListViewModel = contactListViewModel;
            //ContactEditorViewModel = contactEditorViewModel;

            ApplicationExitCommand = availableCommands.GetCommand<ApplicationExitCommand>();
            NewAddressBookCommand = availableCommands.GetCommand<NewAddressBookCommand>();
            OpenAddressBookCommand = availableCommands.GetCommand<OpenAddressBookCommand>();
            SaveAddressBookCommand = availableCommands.GetCommand<SaveAddressBookCommand>();
            SaveAsAddressBookCommand = availableCommands.GetCommand<SaveAsAddressBookCommand>();
            CloseAddressBookCommand = availableCommands.GetCommand<CloseAddressBookCommand>();
            ShowAddressBookPropertiesCommand = availableCommands.GetCommand<ShowAddressBookPropertiesCommand>();
            ShowAboutCommand = availableCommands.GetCommand<ShowAboutCommand>();
            NewContactCommand = availableCommands.GetCommand<NewContactCommand>();
            DeleteCurrentContactCommand = availableCommands.GetCommand<DeleteCurrentContactCommand>();
            UndoCommand = availableCommands.GetCommand<UndoCommand>();
            RedoCommand = availableCommands.GetCommand<RedoCommand>();

            //NewAddressBookViewModel = CreateViewModel<NewAddressBookCommand>();
            //OpenAddressBookViewModel = CreateViewModel<OpenAddressBookCommand>();

            //ToolStripNewAddressBookViewModel = CreateViewModel<NewAddressBookCommand>();
            //ToolStripOpenAddressBookViewModel = CreateViewModel<OpenAddressBookCommand>();
            //ToolStripSaveAddressBookViewModel = CreateViewModel<SaveAddressBookCommand>();
            //ToolStripUndoViewModel = CreateViewModel<UndoCommand>();
            //ToolStripRedoViewModel = CreateViewModel<RedoCommand>();
            //ToolStripAboutViewModel = CreateViewModel<ShowAboutCommand>();

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