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
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Operations;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Services
{
    class MenuItemViewModelProvider
    {
        private readonly IUnityContainer unityContainer;

        public MenuItemViewModelProvider(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public TViewModel CreateNew<TViewModel>(IOperation operation)
            where TViewModel : ViewModelBase
        {
            if (operation == null) throw new ArgumentNullException("operation");

            ResolverOverride resolverOverride = new DependencyOverride(typeof(IOperation), operation);
            return unityContainer.Resolve<TViewModel>(resolverOverride);
        }
    }
}