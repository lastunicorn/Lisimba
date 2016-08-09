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
using System.IO;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.GateModel
{
    public abstract class FileGate : GateBase
    {
        public abstract IEnumerable<FileType> SupportedFileTypes { get; }

        protected FileGate()
        {
            warnings = new List<Exception>();
        }

        public AddressBook Load(string fileName)
        {
            warnings.Clear();

            if (!File.Exists(fileName))
            {
                string message = string.Format("Cannot open address book. File '{0}' does not exist.", fileName);
                throw new GateException(message);
            }

            try
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    return DoLoad(fileStream);
                }
            }
            catch (GateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string message = string.Format("Error opening address book from file '{0}'.", fileName);
                throw new GateException(message, ex);
            }
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            warnings.Clear();

            try
            {
                using (FileStream fileStream = File.OpenWrite(fileName))
                {
                    DoSave(addressBook, fileStream);
                }
            }
            catch (GateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string message = string.Format("Error saving address book to file '{0}'.", fileName);
                throw new GateException(message, ex);
            }
        }

        public override AddressBook Load(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            warnings.Clear();

            try
            {
                return DoLoad(stream);
            }
            catch (GateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GateException("Error opening address book,", ex);
            }
        }

        public override void Save(AddressBook addressBook, Stream stream)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");
            if (stream == null) throw new ArgumentNullException("stream");

            warnings.Clear();

            try
            {
                DoSave(addressBook, stream);
            }
            catch (GateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GateException("Error saving address book.", ex);
            }
        }

        public abstract AddressBook DoLoad(Stream stream);
        public abstract void DoSave(AddressBook addressBook, Stream stream);
    }
}