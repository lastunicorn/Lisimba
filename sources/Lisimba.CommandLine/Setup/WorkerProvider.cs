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
using DustInTheWind.Lisimba.Business.WorkerModel;
using DustInTheWind.Lisimba.CommandLine.Workers;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba.CommandLine.Setup
{
    internal class WorkerProvider : IWorkerProvider
    {
        private readonly IUnityContainer unityContainer;

        public WorkerProvider(IUnityContainer unityContainer)
        {
            if (unityContainer == null) throw new ArgumentNullException("unityContainer");
            this.unityContainer = unityContainer;
        }

        public IEnumerable<IWorker> GetNewWorkers()
        {
            yield return unityContainer.Resolve<AddressBookOpenSuccessWorker>();
            yield return unityContainer.Resolve<AddressBookOpenWarningsWorker>();
            yield return unityContainer.Resolve<AddressBookSaveNewWorker>();
            yield return unityContainer.Resolve<AddressBookSaveSuccessWorker>();
            yield return unityContainer.Resolve<AddressBookCloseGuardWorker>();
            yield return unityContainer.Resolve<AddressBookCloseSuccessWorker>();
        }
    }
}