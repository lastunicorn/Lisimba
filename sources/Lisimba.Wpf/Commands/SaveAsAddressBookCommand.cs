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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Wpf.LocationProviders;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class SaveAsAddressBookCommand : CommandBase
    {
        private readonly OpenedAddressBooks openedAddressBooks;
        private readonly FileLocationProvider fileLocationProvider;
        private readonly Gates gates;

        public override string ShortDescription
        {
            get { return LocalizedResources.SaveAsAddressBookOperationDescription; }
        }

        public SaveAsAddressBookCommand(OpenedAddressBooks openedAddressBooks, WindowSystem windowSystem,
            FileLocationProvider fileLocationProvider, Gates gates)
            : base(windowSystem)
        {
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");
            if (gates == null) throw new ArgumentNullException("gates");

            this.openedAddressBooks = openedAddressBooks;
            this.fileLocationProvider = fileLocationProvider;
            this.gates = gates;

            openedAddressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = openedAddressBooks.Current != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = openedAddressBooks.Current != null;
        }

        protected override void DoExecute(object parameter)
        {
            if (openedAddressBooks.Current == null)
                throw new LisimbaException(LocalizedResources.NoAddessBookOpenedError);

            string fileName = null;

            FileGate fileGate = gates.DefaultGate as FileGate;

            if (fileGate != null)
            {
                fileName = AskForFileToSave(fileGate);

                if (fileName == null)
                    return;
            }

            openedAddressBooks.Current.SaveAddressBook(fileName);
        }

        private string AskForFileToSave(FileGate fileGate)
        {
            List<FileType> fileTypes = new List<FileType>(fileGate.SupportedFileTypes)
            {
                new FileType { FileTypeName = "All Files", Extension = "*" }
            };

            return fileLocationProvider.AskToSave(fileTypes, fileTypes[0]);
        }
    }
}