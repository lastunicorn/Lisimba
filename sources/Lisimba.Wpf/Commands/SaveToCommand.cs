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
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Wpf.LocationProviders;
using DustInTheWind.Lisimba.Wpf.Properties;

namespace DustInTheWind.Lisimba.Wpf.Commands
{
    internal class SaveToCommand : CommandBase
    {
        private readonly AddressBooks addressBooks;
        private readonly FileLocationProvider fileLocationProvider;

        public override string ShortDescription
        {
            get { return LocalizedResources.ExportOperationDescription; }
        }

        public SaveToCommand(WindowSystem windowSystem, AddressBooks addressBooks, FileLocationProvider fileLocationProvider)
            : base(windowSystem)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (fileLocationProvider == null) throw new ArgumentNullException("fileLocationProvider");

            this.addressBooks = addressBooks;
            this.fileLocationProvider = fileLocationProvider;

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            IsEnabled = addressBooks.Current != null;
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            IsEnabled = addressBooks.Current != null;
        }

        protected override void DoExecute(object parameter)
        {
            if (parameter == null) throw new ArgumentNullException("parameter");

            IGate gate = parameter as IGate;

            if (gate == null)
                throw new ArgumentException("Invalid parameter type. IGate is required.", "parameter");

            if (addressBooks.Current == null)
                throw new LisimbaException(LocalizedResources.NoAddessBookOpenedError);

            string fileName = null;

            FileGate fileGate = gate as FileGate;

            if (fileGate != null)
            {
                fileName = AskForFileToSave(fileGate);

                if (fileName == null)
                    return;
            }

            addressBooks.Current.Export(fileName, gate);
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