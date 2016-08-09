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
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly AvailableGates availableGates;
        private readonly FileLocationProvider fileLocationProvider;

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenAddressBookOperationDescription; }
        }

        public OpenAddressBookCommand(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem,
            AvailableGates availableGates, FileLocationProvider fileLocationProvider)
            : base(windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (availableGates == null) throw new ArgumentNullException("availableGates");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");

            this.openedAddressBooks = openedAddressBooks;
            this.availableGates = availableGates;
            this.fileLocationProvider = fileLocationProvider;
        }

        protected override void DoExecute(object parameter)
        {
            string fileName = parameter as string;

            if (fileName == null)
            {
                FileGate fileGate = availableGates.DefaultGate as FileGate;

                if (fileGate != null)
                {
                    fileName = AskForFileToOpen(fileGate);

                    if (fileName == null)
                        return;
                }
            }

            openedAddressBooks.OpenAddressBook(fileName, availableGates.DefaultGate);
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