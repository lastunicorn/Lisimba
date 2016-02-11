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
    public class YahooCsvGate : IGate
    {
        private readonly Loader loader;
        private readonly Saver saver;

        public IEnumerable<Exception> Warnings { get; private set; }

        public string Id
        {
            get { return "YahooCsvGate"; }
        }

        public string Name
        {
            get { return "Yahoo CSV Gate"; }
        }

        public string Description
        {
            get { return "A Gate that knows to save and load address books from a Yahoo csv file."; }
        }

        public Image Icon16
        {
            get { return Resources.yahoo_icon; }
        }

        public YahooCsvGate()
        {
            Warnings = new Exception[0];
            loader = new Loader();
            saver = new Saver();
        }

        //public AddressBook Load(string fileName)
        //{
        //    return loader.Load(fileName);
        //}

        //public void Save(AddressBook addressBook, string fileName)
        //{
        //    saver.Save(addressBook, fileName);
        //}

        public AddressBook Load(string connectionData)
        {
            if (!File.Exists(connectionData))
            {
                string message = string.Format("Cannot open address book. File '{0}' does not exist.", connectionData);
                throw new GateException(message);
            }

            try
            {
                using (FileStream fileStream = File.OpenRead(connectionData))
                {
                    AddressBook addressBook = loader.Load(fileStream);

                    return addressBook;
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error opening address book from file '{0}'.", connectionData);
                throw new GateException(message, ex);
            }
        }

        public void Save(AddressBook addressBook, string connectionData)
        {
            try
            {
                using (FileStream fileStream = File.OpenWrite(connectionData))
                {
                    saver.Save(addressBook, fileStream);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error saving address book to file '{0}'.", connectionData);
                throw new GateException(message, ex);
            }
        }
    }
}