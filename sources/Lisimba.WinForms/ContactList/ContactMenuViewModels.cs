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
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.Lisimba.ContactList
{
    internal class ContactMenuViewModels
    {
        private readonly MenuItemViewModelProvider viewModelProvider;
        private readonly AvailableOperations availableOperations;

        public CustomButtonViewModel AddContactViewModel { get; private set; }
        public CustomButtonViewModel DeleteContactViewModel { get; private set; }
        public CustomButtonViewModel BiorhythmViewModel { get; private set; }

        public ContactMenuViewModels(MenuItemViewModelProvider viewModelProvider, AvailableOperations availableOperations)
        {
            if (viewModelProvider == null) throw new ArgumentNullException("viewModelProvider");
            if (availableOperations == null) throw new ArgumentNullException("availableOperations");

            this.viewModelProvider = viewModelProvider;
            this.availableOperations = availableOperations;

            CreateViewModels();
        }

        private void CreateViewModels()
        {
            NewContactOperation operation = availableOperations.GetOperation<NewContactOperation>();
            AddContactViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation);

            DeleteCurrentContactOperation operation1 = availableOperations.GetOperation<DeleteCurrentContactOperation>();
            DeleteContactViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation1);

            ShowBiorhythmOperation operation2 = availableOperations.GetOperation<ShowBiorhythmOperation>();
            BiorhythmViewModel = viewModelProvider.CreateNew<CustomButtonViewModel>(operation2);
        }
    }
}