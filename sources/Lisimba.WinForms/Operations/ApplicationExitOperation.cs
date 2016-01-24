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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class ApplicationExitOperation : ExecutableViewModelBase<object>
    {
        private readonly ApplicationBackEnd applicationBackEnd;

        public override string ShortDescription
        {
            get { return LocalizedResources.ApplicationExitOperationDescription; }
        }

        public ApplicationExitOperation(ApplicationBackEnd applicationBackEnd, ApplicationStatus applicationStatus)
            : base(applicationStatus)
        {
            if (applicationBackEnd == null) throw new ArgumentNullException("applicationBackEnd");

            this.applicationBackEnd = applicationBackEnd;
        }

        protected override void DoExecute(object parameter)
        {
            applicationBackEnd.Exit();
        }
    }
}