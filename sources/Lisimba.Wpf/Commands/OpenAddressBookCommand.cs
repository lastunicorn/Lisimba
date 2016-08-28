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
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Wpf.LocationProviders;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class OpenAddressBookCommand : CommandBase
    {
        private readonly AddressBooks addressBooks;
        private readonly Gates gates;
        private readonly FileLocationProvider fileLocationProvider;

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenAddressBookOperationDescription; }
        }

        public OpenAddressBookCommand(AddressBooks addressBooks, WindowSystem windowSystem,
            Gates gates, FileLocationProvider fileLocationProvider)
            : base(windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (gates == null) throw new ArgumentNullException("gates");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");

            this.addressBooks = addressBooks;
            this.gates = gates;
            this.fileLocationProvider = fileLocationProvider;
        }

        protected override void DoExecute(object parameter)
        {
            string fileName = parameter as string;

            if (fileName == null)
            {
                FileGate fileGate = gates.DefaultGate as FileGate;

                if (fileGate != null)
                {
                    fileName = AskForFileToOpen(fileGate);

                    if (fileName == null)
                        return;
                }
            }

            addressBooks.OpenAddressBook(gates.DefaultGate, fileName);
        }

        private string AskForFileToOpen(FileGate fileGate)
        {
            List<FileType> fileTypes = new List<FileType>(fileGate.SupportedFileTypes)
            {
                new FileType {FileTypeName = "All Files", Extension = "*"}
            };

            return fileLocationProvider.AskToOpen(fileTypes, fileTypes[0]);
        }
    }
}