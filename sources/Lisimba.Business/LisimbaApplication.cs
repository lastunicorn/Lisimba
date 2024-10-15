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
            this.initialCatalogOpener = initialCatalogOpener ?? throw new ArgumentNullException(nameof(initialCatalogOpener));
            this.addressBooks = addressBooks ?? throw new ArgumentNullException(nameof(addressBooks));
            this.userInterface = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            this.programArguments = programArguments ?? throw new ArgumentNullException(nameof(programArguments));
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
            Started?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnding(CancelEventArgs e)
        {
            Ending?.Invoke(this, e);
        }

        protected virtual void OnEndCanceled()
        {
            EndCanceled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            Ended?.Invoke(this, EventArgs.Empty);
        }
    }
}