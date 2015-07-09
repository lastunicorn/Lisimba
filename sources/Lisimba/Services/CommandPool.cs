// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using DustInTheWind.Lisimba.Commands;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Services
{
    class CommandPool
    {
        private readonly IUnityContainer servicePrvider;

        public CreateNewAddressBookCommand CreateNewAddressBookCommand
        {
            get { return servicePrvider.Resolve<CreateNewAddressBookCommand>(); }
        }

        public OpenAddressBookCommand OpenAddressBookCommand
        {
            get { return servicePrvider.Resolve<OpenAddressBookCommand>(); }
        }

        public SaveAddressBookCommand SaveAddressBookCommand
        {
            get { return servicePrvider.Resolve<SaveAddressBookCommand>(); }
        }

        public SaveAsAddressBookCommand SaveAsAddressBookCommand
        {
            get { return servicePrvider.Resolve<SaveAsAddressBookCommand>(); }
        }

        public ExportYahooCsvCommand ExportYahooCsvCommand
        {
            get { return servicePrvider.Resolve<ExportYahooCsvCommand>(); }
        }

        public ShowAddressBookPropertiesCommand ShowAddressBookPropertiesCommand
        {
            get { return servicePrvider.Resolve<ShowAddressBookPropertiesCommand>(); }
        }

        public ShowAboutCommand ShowAboutCommand
        {
            get { return servicePrvider.Resolve<ShowAboutCommand>(); }
        }

        public ImportYahooCsvCommand ImportYahooCsvCommand
        {
            get { return servicePrvider.Resolve<ImportYahooCsvCommand>(); }
        }

        public DeleteCurrentContactCommand DeleteCurrentContactCommand
        {
            get { return servicePrvider.Resolve<DeleteCurrentContactCommand>(); }
        }

        public CreateNewContactCommand CreateNewContactCommand
        {
            get { return servicePrvider.Resolve<CreateNewContactCommand>(); }
        }

        public ApplicationExitCommand ApplicationExitCommand
        {
            get { return servicePrvider.Resolve<ApplicationExitCommand>(); }
        }

        public CommandPool(IUnityContainer servicePrvider)
        {
            if (servicePrvider == null)
                throw new ArgumentNullException("servicePrvider");

            this.servicePrvider = servicePrvider;
        }
    }
}
