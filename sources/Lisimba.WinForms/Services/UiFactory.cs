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
using System.ComponentModel;
using System.Windows.Forms;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Operations;
using Unity;
using Unity.Resolution;

namespace DustInTheWind.Lisimba.WinForms.Services
{
    internal class UiFactory
    {
        private readonly IUnityContainer unityContainer;

        public UiFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        public T CreateForm<T>()
            where T : Form
        {
            return unityContainer.Resolve<T>();
        }

        public T CreateComponent<T>()
            where T : Component
        {
            return unityContainer.Resolve<T>();
        }

        public TViewModel CreateNewViewModel<TViewModel>(IOperation operation)
            where TViewModel : ViewModelBase
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));

            ResolverOverride resolverOverride = new DependencyOverride(typeof(IOperation), operation);
            return unityContainer.Resolve<TViewModel>(resolverOverride);
        }
    }
}