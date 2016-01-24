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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Exceptions;
using DustInTheWind.Lisimba.Egg.Properties;

namespace DustInTheWind.Lisimba.Egg
{
    public class EmptyGate : IGate
    {
        public IEnumerable<Exception> Warnings { get; private set; }

        public string Id
        {
            get { return "EmptyGate"; }
        }

        public string Name
        {
            get { return "Empty Gate"; }
        }

        public string Description
        {
            get { return Resources.EmptyGate_Description; }
        }

        public EmptyGate()
        {
            Warnings = new List<Exception>();
        }

        public AddressBook Load(string fileName)
        {
            throw new EggException(Resources.EmptyGate_LoadError);
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            throw new EggException(Resources.EmptyGate_SaveError);
        }
    }
}