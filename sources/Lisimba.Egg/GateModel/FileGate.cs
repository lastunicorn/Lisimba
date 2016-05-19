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
using System.Drawing;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Egg.GateModel
{
    public abstract class FileGate : IGate
    {
        protected readonly List<Exception> warnings;
        public abstract string Id { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Image Icon16 { get; }

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        public abstract string ExtensionFilter { get; }

        protected FileGate()
        {
            warnings = new List<Exception>();
        }

        protected void AddWarning(Exception warning)
        {
            warnings.Add(warning);
        }

        public abstract AddressBook Load(object connectionData);
        public abstract void Save(AddressBook addressBook, object connectionData);
    }
}