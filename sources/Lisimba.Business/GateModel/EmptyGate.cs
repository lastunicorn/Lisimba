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
using System.IO;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.Properties;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Egg.GateModel
{
    public class EmptyGate : IGate
    {
        private static readonly Bitmap icon16;

        static EmptyGate()
        {
            icon16 = new Bitmap(16, 16);
        }

        public IReadOnlyList<Exception> Warnings { get; private set; }

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

        public Image Icon16
        {
            get { return icon16; }
        }

        public EmptyGate()
        {
            Warnings = new Exception[0];
        }

        public AddressBook Load(object connectionData)
        {
            throw new LisimbaException(Resources.EmptyGate_LoadError);
        }

        public void Save(AddressBook addressBook, object connectionData)
        {
            throw new LisimbaException(Resources.EmptyGate_SaveError);
        }

        public AddressBook Load(Stream stream)
        {
            throw new LisimbaException(Resources.EmptyGate_LoadError);
        }

        public void Save(AddressBook addressBook, Stream stream)
        {
            throw new LisimbaException(Resources.EmptyGate_SaveError);
        }
    }
}