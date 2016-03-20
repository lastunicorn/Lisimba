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
using DustInTheWind.Lisimba.Wpf.Commands;
using DustInTheWind.Lisimba.Wpf.Operations;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.Wpf.Setup
{
    internal class CommandProvider : ICommandProvider
    {
        private readonly IUnityContainer unityContainer;

        public CommandProvider(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IEnumerable<CommandBase> GetNewCommands()
        {
            //yield return unityContainer.Resolve<ShowMainOperation>();
            yield return unityContainer.Resolve<NewAddressBookCommand>();
            yield return unityContainer.Resolve<OpenAddressBookCommand>();
            yield return unityContainer.Resolve<SaveAddressBookCommand>();
            yield return unityContainer.Resolve<SaveAsAddressBookCommand>();
            yield return unityContainer.Resolve<CloseAddressBookCommand>();
            //yield return unityContainer.Resolve<ShowAddressBookPropertiesOperation>();
            yield return unityContainer.Resolve<ShowAboutCommand>();
            //yield return unityContainer.Resolve<NewContactOperation>();
            //yield return unityContainer.Resolve<DeleteCurrentContactOperation>();
            yield return unityContainer.Resolve<ApplicationExitCommand>();
            //yield return unityContainer.Resolve<ImportOperation>();
            //yield return unityContainer.Resolve<ExportOperation>();
            //yield return unityContainer.Resolve<OpenRecentFileOperation>();
            yield return unityContainer.Resolve<UndoCommand>();
            yield return unityContainer.Resolve<RedoCommand>();
            //yield return unityContainer.Resolve<ShowBiorhythmOperation>();
        }
    }
}