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
using DustInTheWind.Lisimba.Operations;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Services
{
    class CommandPool
    {
        private readonly IUnityContainer servicePrvider;

        public CreateNewAddressBookOperation CreateNewAddressBookOperation
        {
            get { return servicePrvider.Resolve<CreateNewAddressBookOperation>(); }
        }

        public OpenAddressBookOperation OpenAddressBookOperation
        {
            get { return servicePrvider.Resolve<OpenAddressBookOperation>(); }
        }

        public SaveAddressBookOperation SaveAddressBookOperation
        {
            get { return servicePrvider.Resolve<SaveAddressBookOperation>(); }
        }

        public SaveAsAddressBookOperation SaveAsAddressBookOperation
        {
            get { return servicePrvider.Resolve<SaveAsAddressBookOperation>(); }
        }

        public CloseAddressBookOperation CloseAddressBookOperation
        {
            get { return servicePrvider.Resolve<CloseAddressBookOperation>(); }
        }

        //public ExportYahooCsvOperation ExportYahooCsvOperation
        //{
        //    get { return servicePrvider.Resolve<ExportYahooCsvOperation>(); }
        //}

        public ShowAddressBookPropertiesOperation ShowAddressBookPropertiesOperation
        {
            get { return servicePrvider.Resolve<ShowAddressBookPropertiesOperation>(); }
        }

        public ShowAboutOperation ShowAboutOperation
        {
            get { return servicePrvider.Resolve<ShowAboutOperation>(); }
        }

        //public ImportYahooCsvOperation ImportYahooCsvOperation
        //{
        //    get { return servicePrvider.Resolve<ImportYahooCsvOperation>(); }
        //}

        public DeleteCurrentContactOperation DeleteCurrentContactOperation
        {
            get { return servicePrvider.Resolve<DeleteCurrentContactOperation>(); }
        }

        public CreateNewContactOperation CreateNewContactOperation
        {
            get { return servicePrvider.Resolve<CreateNewContactOperation>(); }
        }

        public ApplicationExitOperation ApplicationExitOperation
        {
            get { return servicePrvider.Resolve<ApplicationExitOperation>(); }
        }

        public CommandPool(IUnityContainer servicePrvider)
        {
            if (servicePrvider == null)
                throw new ArgumentNullException("servicePrvider");

            this.servicePrvider = servicePrvider;
        }
    }
}
