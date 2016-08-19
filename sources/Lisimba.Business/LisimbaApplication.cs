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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using DustInTheWind.Lisimba.Business.AddressBookManagement;
using DustInTheWind.Lisimba.Business.ArgumentsManagement;

namespace DustInTheWind.Lisimba.Business
{
    /// <summary>
    /// - announces start/stop
    /// </summary>
    public class LisimbaApplication
    {
        private readonly InitialCatalogOpener initialCatalogOpener;
        private readonly AddressBooks addressBooks;
        private readonly IUserInterface userInterface;
        private readonly ProgramArguments programArguments;

        public event EventHandler Started;
        public event EventHandler<CancelEventArgs> Ending;
        public event EventHandler EndCanceled;
        public event EventHandler Ended;

        private readonly List<Exception> warnings = new List<Exception>();

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

                return string.Format("{0} {1} alpha 2", fileVersionInfo.ProductName, version);
            }
        }

        public LisimbaApplication(InitialCatalogOpener initialCatalogOpener, AddressBooks addressBooks,
            IUserInterface userInterface, ProgramArguments programArguments)
        {
            if (initialCatalogOpener == null) throw new ArgumentNullException("initialCatalogOpener");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (programArguments == null) throw new ArgumentNullException("programArguments");

            this.initialCatalogOpener = initialCatalogOpener;
            this.addressBooks = addressBooks;
            this.userInterface = userInterface;
            this.programArguments = programArguments;
        }

        public void Run(string[] args)
        {
            programArguments.Initialize(args);
            userInterface.Initialize();
            OpenInitialAddressBook();

            OnStarted();
            userInterface.Start();
            OnEnded();
        }

        private void OpenInitialAddressBook()
        {
            try
            {
                initialCatalogOpener.OpenInitialCatalog();
            }
            catch (Exception ex)
            {
                warnings.Add(ex);
            }
        }

        public void Exit()
        {
            bool allowToContinue = addressBooks.CloseCurrentAddressBook();
            if (!allowToContinue)
                return;

            CancelEventArgs args = new CancelEventArgs();
            OnEnding(args);

            if (args.Cancel)
            {
                OnEndCanceled();
                return;
            }

            userInterface.Exit();
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