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
using DustInTheWind.ConsoleCommon;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.ObservingModel;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Observers
{
    internal class AddressBookSaveObserver : IObserver
    {
        private readonly EnhancedConsole console;
        private readonly AddressBooks addressBooks;
        private readonly Gates gates;

        public AddressBookSaveObserver(EnhancedConsole console, AddressBooks addressBooks, Gates gates)
        {
            if (console == null) throw new ArgumentNullException("console");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (gates == null) throw new ArgumentNullException("gates");

            this.console = console;
            this.addressBooks = addressBooks;
            this.gates = gates;
        }

        public void Start()
        {
            addressBooks.NewLocationNeeded += HandleAddressBooksNewLocationNeeded;
            addressBooks.GateNeeded += HandleAddressBooksGateNeeded;
            addressBooks.AddressBookSaved += HandleAddressBookSaved;
        }

        public void Stop()
        {
            addressBooks.NewLocationNeeded -= HandleAddressBooksNewLocationNeeded;
            addressBooks.GateNeeded -= HandleAddressBooksGateNeeded;
            addressBooks.AddressBookSaved -= HandleAddressBookSaved;
        }

        private void HandleAddressBooksNewLocationNeeded(object sender, NewLocationNeededEventArgs e)
        {
            string newLocation = AskForNewLocation();

            if (string.IsNullOrEmpty(newLocation))
                e.Cancel = true;
            else
                e.NewLocation = newLocation;
        }

        public string AskForNewLocation()
        {
            console.WriteNormal(Resources.AskForNewLocation);
            return console.ReadLine();
        }

        private void HandleAddressBooksGateNeeded(object sender, GateNeededEventArgs e)
        {
            IGate newGate = AskForNewGate();

            if (newGate == null)
                e.Cancel = true;
            else
                e.Gate = newGate;
        }

        private IGate AskForNewGate()
        {
            console.WriteLineNormal(Resources.GateListTitle);

            List<IGate> gates = this.gates.GetAllGates().ToList();

            DisplayGates(gates);

            int? selectedIndex = ReadSelectedGateIndex();

            if (selectedIndex == null || selectedIndex < 0 || selectedIndex > gates.Count - 1)
                return null;

            return gates[selectedIndex.Value];
        }

        private int? ReadSelectedGateIndex()
        {
            console.WriteNormal(Resources.AskForNewGate);
            string userValue = console.ReadLine();

            int selectedIndex;

            return int.TryParse(userValue, out selectedIndex)
                ? selectedIndex - 1
                : (int?)null;
        }

        private void DisplayGates(IReadOnlyList<IGate> gates)
        {
            for (int i = 0; i < gates.Count; i++)
                console.WriteLineNormal(string.Format("{0} - {1}", i + 1, gates[i].Name));
        }

        private void HandleAddressBookSaved(object sender, EventArgs e)
        {
            string addressBookName = addressBooks.Current.GetFriendlyName();
            string addressBookLocation = addressBooks.Current.Location;
            string text = string.Format(Resources.SaveAddressBookSuccess, addressBookName, addressBookLocation);

            console.WriteLineSuccess(text);
        }
    }
}