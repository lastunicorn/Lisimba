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
using System.Diagnostics;
using System.Reflection;
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.Business
{
    /// <summary>
    /// - announces start/stop
    /// </summary>
    public class ApplicationBackEnd
    {
        private readonly InitialCatalogOpener initialCatalogOpener;
        private readonly OpenedAddressBooks openedAddressBooks;

        public event EventHandler Started;
        public event EventHandler<CancelEventArgs> Ending;
        public event EventHandler EndCanceled;
        public event EventHandler Ended;

        public string ProgramName
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = assembly.GetName();

                string version = assemblyName.Version.Build == 0
                    ? assemblyName.Version.ToString(2)
                    : assemblyName.Version.ToString(3);

                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                return string.Format("{0} {1}", fileVersionInfo.ProductName, version);
            }
        }

        public ApplicationBackEnd(InitialCatalogOpener initialCatalogOpener, OpenedAddressBooks openedAddressBooks)
        {
            if (initialCatalogOpener == null) throw new ArgumentNullException("initialCatalogOpener");
            if (openedAddressBooks == null) throw new ArgumentNullException("openedAddressBooks");

            this.initialCatalogOpener = initialCatalogOpener;
            this.openedAddressBooks = openedAddressBooks;
        }

        public void Start()
        {
            initialCatalogOpener.OpenInitialCatalog();

            OnStarted();
        }

        public void Exit()
        {
            bool allowToContinue = openedAddressBooks.CloseAddressBook();
            if (!allowToContinue)
                return;

            CancelEventArgs args = new CancelEventArgs();
            OnEnding(args);

            if (args.Cancel)
                OnEndCanceled();
            else
                OnEnded();
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnEnding(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = Ending;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnEndCanceled()
        {
            EventHandler handler = EndCanceled;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            EventHandler handler = Ended;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}