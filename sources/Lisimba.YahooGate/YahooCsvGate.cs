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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.Gating.Properties;

namespace DustInTheWind.Lisimba.Gating
{
    public class YahooCsvGate : FileGate
    {
        private readonly Loader loader;
        private readonly Saver saver;

        public override string Id
        {
            get { return "YahooCsvGate"; }
        }

        public override string Name
        {
            get { return "Yahoo CSV Gate"; }
        }

        public override string Description
        {
            get { return "A Gate that knows to save and load address books from a Yahoo csv file."; }
        }

        public override Image Icon16
        {
            get { return Resources.yahoo_icon; }
        }

        public override IEnumerable<FileType> SupportedFileTypes
        {
            get
            {
                return new[]
                {
                    new FileType { FileTypeName = "Csv Files", Extension = "csv" }
                };
            }
        }

        public YahooCsvGate()
        {
            loader = new Loader();
            saver = new Saver();
        }

        public override AddressBook DoLoad(Stream stream)
        {
            warnings.Clear();

            return loader.Load(stream);
        }

        public override void DoSave(AddressBook addressBook, Stream stream)
        {
            warnings.Clear();

            saver.Save(addressBook, stream);
        }
    }
}