﻿// Lisimba
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
using System.Windows.Input;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly LisimbaWindowTitle lisimbaWindowTitle;
        private readonly AddressBookViewModel addressBookViewModel;
        private readonly StartViewModel startViewModel;

        private string title;
        private ViewModelBase currentPageViewModel;

        public ViewModelBase CurrentPageViewModel
        {
            get { return currentPageViewModel; }
            set
            {
                currentPageViewModel = value;
                OnPropertyChanged();
            }
        }

        public LisimbaMainMenuViewModel LisimbaMainMenuViewModel { get; private set; }
        public LisimbaToolBarViewModel LisimbaToolBarViewModel { get; private set; }
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

        //public MainWindowClosingCommand WindowClosing { get; set; }

        public LisimbaViewModel(OpenedAddressBooks openedAddressBooks, LisimbaStatusBarViewModel lisimbaStatusBarViewModel,
            LisimbaMainMenuViewModel lisimbaMainMenuViewModel, LisimbaToolBarViewModel lisimbaToolBarViewModel,
            LisimbaWindowTitle lisimbaWindowTitle, AddressBookViewModel addressBookViewModel, StartViewModel startViewModel)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (lisimbaStatusBarViewModel == null) throw new ArgumentNullException("lisimbaStatusBarViewModel");
            if (lisimbaMainMenuViewModel == null) throw new ArgumentNullException("lisimbaMainMenuViewModel");
            if (lisimbaToolBarViewModel == null) throw new ArgumentNullException("lisimbaToolBarViewModel");
            if (lisimbaWindowTitle == null) throw new ArgumentNullException("lisimbaWindowTitle");
            if (addressBookViewModel == null) throw new ArgumentNullException("addressBookViewModel");
            if (startViewModel == null) throw new ArgumentNullException("startViewModel");

            this.lisimbaWindowTitle = lisimbaWindowTitle;
            this.addressBookViewModel = addressBookViewModel;
            this.startViewModel = startViewModel;

            LisimbaMainMenuViewModel = lisimbaMainMenuViewModel;
            LisimbaToolBarViewModel = lisimbaToolBarViewModel;
            LisimbaStatusBarViewModel = lisimbaStatusBarViewModel;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            lisimbaWindowTitle.ValueChanged += HandleLisimbaTitleValueChanged;

            Title = lisimbaWindowTitle.Value;
        }

        private void HandleLisimbaTitleValueChanged(object sender, EventArgs e)
        {
            Title = lisimbaWindowTitle.Value;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.NewAddressBook == null)
                CurrentPageViewModel = startViewModel;
            else
                CurrentPageViewModel = addressBookViewModel;
        }
    }

    //class MainWindowClosingCommand : ICommand
    //{
    //    private readonly OpenedAddressBooks openedAddressBooks;
    //    private readonly WindowSystem windowSystem;

    //    public event EventHandler CanExecuteChanged;

    //    public MainWindowClosingCommand(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem)
    //    {
    //        if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
    //        if (windowSystem == null) throw new ArgumentNullException("windowSystem");

    //        this.openedAddressBooks = openedAddressBooks;
    //        this.windowSystem = windowSystem;
    //    }

    //    public bool CanExecute(object parameter)
    //    {

    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (openedAddressBooks.Current.Status == AddressBookStatus.Modified)
    //        {
    //            bool? needToSave = AskToSaveAddressBook();

    //            if (needToSave == null)
    //                e.Cancel = true;
    //            else
    //                e.SaveAddressBook = needToSave.Value;
    //        }
    //        else
    //        {
    //            e.SaveAddressBook = false;
    //        }
    //    }
    //}
}