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
using System.ComponentModel;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.Config;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class LisimbaViewModel : ViewModelBase
    {
        private readonly LisimbaWindowTitle lisimbaWindowTitle;
        private readonly AddressBookViewModel addressBookViewModel;
        private readonly StartViewModel startViewModel;
        private readonly LisimbaApplication lisimbaApplication;
        private readonly ApplicationConfiguration config;

        private bool allowToCloseWindow;
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

        public LisimbaViewModel(OpenedAddressBooks openedAddressBooks, LisimbaStatusBarViewModel lisimbaStatusBarViewModel,
            LisimbaMainMenuViewModel lisimbaMainMenuViewModel, LisimbaToolBarViewModel lisimbaToolBarViewModel,
            LisimbaWindowTitle lisimbaWindowTitle, AddressBookViewModel addressBookViewModel, StartViewModel startViewModel,
            LisimbaApplication lisimbaApplication, ApplicationConfiguration config)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (lisimbaStatusBarViewModel == null) throw new ArgumentNullException("lisimbaStatusBarViewModel");
            if (lisimbaMainMenuViewModel == null) throw new ArgumentNullException("lisimbaMainMenuViewModel");
            if (lisimbaToolBarViewModel == null) throw new ArgumentNullException("lisimbaToolBarViewModel");
            if (lisimbaWindowTitle == null) throw new ArgumentNullException("lisimbaWindowTitle");
            if (addressBookViewModel == null) throw new ArgumentNullException("addressBookViewModel");
            if (startViewModel == null) throw new ArgumentNullException("startViewModel");
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
            if (config == null) throw new ArgumentNullException("config");

            this.lisimbaWindowTitle = lisimbaWindowTitle;
            this.addressBookViewModel = addressBookViewModel;
            this.startViewModel = startViewModel;
            this.lisimbaApplication = lisimbaApplication;
            this.config = config;

            LisimbaMainMenuViewModel = lisimbaMainMenuViewModel;
            LisimbaToolBarViewModel = lisimbaToolBarViewModel;
            LisimbaStatusBarViewModel = lisimbaStatusBarViewModel;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            lisimbaWindowTitle.ValueChanged += HandleLisimbaTitleValueChanged;
            lisimbaApplication.Ending += HandleApplicationEnding;
            lisimbaApplication.EndCanceled += HandleApplicationEndCanceled;

            Title = lisimbaWindowTitle.Value;
        }

        private void HandleApplicationEnding(object sender, CancelEventArgs e)
        {
            allowToCloseWindow = true;
        }

        private void HandleApplicationEndCanceled(object sender, EventArgs eventArgs)
        {
            allowToCloseWindow = false;
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

        public bool WindowIsClosing()
        {
            if (allowToCloseWindow || config.StartInTray)
                return true;

            lisimbaApplication.Exit();
            return false;
        }
    }
}