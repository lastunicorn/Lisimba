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

using System.Collections.Generic;
using DustInTheWind.Lisimba.Business;

namespace DustInTheWind.Lisimba.CommandLine.FlowOptions
{
    internal class CompareFlowOptions
    {
        public string AddressBookLocation { get; set; }
        public bool DisplayDetails { get; set; }
        public bool DisplayOnlyDiff { get; set; }

        public CompareFlowOptions(IList<string> parameters)
        {
            if (parameters.Count == 0)
                throw new LisimbaException("Specify the address book to compare to.");

            AddressBookLocation = parameters[0];

            for (int i = 1; i < parameters.Count; i++)
            {
                string param = parameters[i];

                switch (param)
                {
                    case "-d":
                    case "--details":
                        DisplayDetails = true;
                        break;

                    case "-D":
                    case "--diff":
                        DisplayOnlyDiff = true;
                        break;
                }
            }
        }
    }
}