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
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Wpf.LocationProviders;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class OpenFromCommand : CommandBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly FileLocationProvider fileLocationProvider;

        public OpenFromCommand(WindowSystem windowSystem, OpenedAddressBooks openedAddressBooks, FileLocationProvider fileLocationProvider)
            : base(windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");

            this.openedAddressBooks = openedAddressBooks;
            this.fileLocationProvider = fileLocationProvider;
        }

        public OpenFromCommand(WindowSystem windowSystem)
            : base(windowSystem)
        {
        }

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenFromOperationDescription; }
        }

        protected override void DoExecute(object parameter)
        {
            if (parameter == null) throw new ArgumentNullException("parameter");

            IGate gate = parameter as IGate;

            if (gate == null)
                throw new ArgumentException("Invalid parameter type. IGate is required.", "parameter");

            string fileName = null;

            FileGate fileGate = gate as FileGate;

            if (fileGate != null)
            {
                fileName = AskForFileToOpen(fileGate);

                if (fileName == null)
                    return;
            }

            openedAddressBooks.OpenAddressBook(fileName, gate);
        }

        private string AskForFileToOpen(FileGate fileGate)
        {
            List<FileType> fileTypes = new List<FileType>(fileGate.SupportedFileTypes)
            {
                new FileType { FileTypeName = "All Files", Extension = "*" }
            };

            return fileLocationProvider.AskToOpen(fileTypes, fileTypes[0]);
        }
    }
}