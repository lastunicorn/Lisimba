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
using DustInTheWind.Lisimba.MainMenu;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Utils;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Main
{
    class MenuItemViewModelProvider
    {
        private readonly IUnityContainer unityContainer;

        public MenuItemViewModelProvider(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");

            this.unityContainer = unityContainer;
        }

        public CustomMenuItemViewModel GetNewViewModel<T>(T operation)
            where T : class, IExecutableViewModel
        {
            if (operation == null) throw new ArgumentNullException("operation");

            ResolverOverride resolverOverride = new DependencyOverride(typeof(IExecutableViewModel), operation);
            return unityContainer.Resolve<CustomMenuItemViewModel>(resolverOverride);
        }

        public T GetViewModel<T>(IExecutableViewModel operation)
            where T : ViewModelBase
        {
            if (operation == null) throw new ArgumentNullException("operation");

            ResolverOverride resolverOverride = new DependencyOverride(typeof(IExecutableViewModel), operation);
            return unityContainer.Resolve<T>(resolverOverride);
        }

        public T GetViewModel<T>()
            where T : CustomMenuItemViewModel
        {
            return unityContainer.Resolve<T>();
        }
    }
}