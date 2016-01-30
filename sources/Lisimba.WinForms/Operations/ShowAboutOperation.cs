﻿// Lisimba
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

using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class ShowAboutOperation : ExecutableViewModelBase<object>
    {
        public override string ShortDescription
        {
            get { return LocalizedResources.ShowAboutOperationDescription; }
        }

        public ShowAboutOperation(ApplicationStatus applicationStatus, UserInterface userInterface)
            : base(applicationStatus, userInterface)
        {
        }

        protected override void DoExecute(object parameter)
        {
            userInterface.ShowAbout();
        }
    }
}