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
using DustInTheWind.Lisimba.WinForms.Operations;
using DustInTheWind.WinFormsCommon.Operations;
using Unity;

namespace DustInTheWind.Lisimba.WinForms.Setup
{
    internal class OperationProvider : IOperationProvider
    {
        private readonly IUnityContainer unityContainer;

        public OperationProvider(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        public IEnumerable<IOperation> GetNewOperations()
        {
            yield return unityContainer.Resolve<ShowMainOperation>();
            yield return unityContainer.Resolve<NewAddressBookOperation>();
            yield return unityContainer.Resolve<OpenAddressBookOperation>();
            yield return unityContainer.Resolve<SaveAddressBookOperation>();
            yield return unityContainer.Resolve<SaveAsAddressBookOperation>();
            yield return unityContainer.Resolve<CloseAddressBookOperation>();
            yield return unityContainer.Resolve<ShowAddressBookPropertiesOperation>();
            yield return unityContainer.Resolve<ShowAboutOperation>();
            yield return unityContainer.Resolve<NewContactOperation>();
            yield return unityContainer.Resolve<DeleteCurrentContactOperation>();
            yield return unityContainer.Resolve<ApplicationExitOperation>();
            yield return unityContainer.Resolve<ImportOperation>();
            yield return unityContainer.Resolve<ExportOperation>();
            yield return unityContainer.Resolve<OpenRecentFileOperation>();
            yield return unityContainer.Resolve<UndoOperation>();
            yield return unityContainer.Resolve<RedoOperation>();
            yield return unityContainer.Resolve<ShowBiorhythmOperation>();
        }
    }
}