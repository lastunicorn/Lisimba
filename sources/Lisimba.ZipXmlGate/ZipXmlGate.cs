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
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Gating.Properties;

namespace DustInTheWind.Lisimba.Gating
{
    public class ZipXmlGate : IGate
    {
        private readonly Saver saver;
        private readonly Loader loader;

        public IEnumerable<Exception> Warnings
        {
            get { return loader.Warnings; }
        }

        public string Id
        {
            get { return "ZipXmlGate"; }
        }

        public string Name
        {
            get { return "Zipped Xml Gate"; }
        }

        public string Description
        {
            get { return "A Gate that knows to save and load address books from a xml file that is zipped."; }
        }

        public Image Icon16
        {
            get { return Resources.lisimba_icon; }
        }

        public ZipXmlGate()
        {
            loader = new Loader();
            saver = new Saver();
        }

        public AddressBook Load(string fileName)
        {
            return loader.Load(fileName);
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            saver.Save(addressBook, fileName);
        }
    }
}