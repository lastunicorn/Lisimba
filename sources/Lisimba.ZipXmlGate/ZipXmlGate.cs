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
using System.Drawing;
using System.IO;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.ZipXmlGate.Properties;

namespace DustInTheWind.Lisimba.ZipXmlGate
{
    public class ZipXmlGate : FileGate
    {
        private readonly ZipXmlGateEncoder gateEncoder;

        public override string Id
        {
            get { return "ZipXmlGate"; }
        }

        public override string Name
        {
            get { return "Zipped Xml Gate"; }
        }

        public override string Description
        {
            get { return "A Gate that knows to save and load address books from a xml file that is zipped."; }
        }

        public override Image Icon16
        {
            get { return Resources.lisimba_icon; }
        }

        public override bool CanLoad
        {
            get { return true; }
        }

        public override bool CanSave
        {
            get { return true; }
        }

        public override IEnumerable<FileType> SupportedFileTypes
        {
            get
            {
                return new[]
                {
                    new FileType { FileTypeName = "Lisimba Files", Extension = "lsb" }
                };
            }
        }

        public ZipXmlGate()
        {
            gateEncoder = new ZipXmlGateEncoder();
        }

        public override AddressBook DoLoad(Stream stream)
        {
            warnings.Clear();

            AddressBook addressBook = gateEncoder.Decode(stream);
            warnings.AddRange(gateEncoder.Warnings);

            return addressBook;
        }

        public override void DoSave(AddressBook addressBook, Stream stream)
        {
            warnings.Clear();

            gateEncoder.Encode(addressBook, stream);
            warnings.AddRange(gateEncoder.Warnings);
        }
    }
}