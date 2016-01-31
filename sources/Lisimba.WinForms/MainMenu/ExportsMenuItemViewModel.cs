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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.MainMenu
{
    class ExportsMenuItemViewModel : ListMenuItemViewModel
    {
        private readonly AvailableGates availableGates;

        public ExportsMenuItemViewModel(ApplicationStatus applicationStatus, UserInterface userInterface, IOperation operation, AvailableGates availableGates)
            : base(applicationStatus, userInterface, operation)
        {
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.availableGates = availableGates;
        }

        protected override IEnumerable<CustomButtonViewModel> GetItems()
        {
            return availableGates.GetAllGates()
                .Select(x => new ImportMenuItemViewModel(applicationStatus, userInterface, ChildrenOpertion)
                {
                    Text = x.Name,
                    Gate = x
                });
        }
    }
}