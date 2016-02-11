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
using System.Collections.ObjectModel;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Forms
{
    public class GateSelectorViewModel : ViewModelBase
    {
        private ObservableCollection<IGate> gates;

        public ObservableCollection<IGate> Gates
        {
            get { return gates; }
            set
            {
                gates = value;
                OnPropertyChanged();
            }
        }

        public AvailableGates AvailableGates { get; private set; }

        public GateSelectorViewModel(AvailableGates availableGates)
        {
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            AvailableGates = availableGates;

            Gates = new ObservableCollection<IGate>(availableGates.GetAllGates());
        }
    }
}