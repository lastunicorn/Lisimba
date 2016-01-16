﻿// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.Reflection;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Common;
using DustInTheWind.Lisimba.Common.Config;
using DustInTheWind.Lisimba.Common.GateManagement;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Services
{
    internal class LisimbaApplication
    {
        private readonly ApplicationStatus applicationStatus;
        private readonly UserInterface userInterface;
        private readonly ActiveObservers activeObservers;
        private readonly CommandPool commandPool;
        private readonly ProgramArguments programArguments;
        private readonly ApplicationConfiguration applicationConfiguration;
        private readonly RecentFiles recentFiles;
        private readonly AvailableGates availableGates;

        public event EventHandler Started;
        public event EventHandler<CancelEventArgs> Exiting;
        public event EventHandler BeforeExiting;
        public event EventHandler ExitCanceled;

        public string ProgramName
        {
            get
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = executingAssembly.GetName();

                string version = assemblyName.Version.Build == 0
                    ? assemblyName.Version.ToString(2)
                    : assemblyName.Version.ToString(3);

                return string.Format("{0} {1}", Application.ProductName, version);
            }
        }

        public LisimbaApplication(ApplicationStatus applicationStatus, UserInterface userInterface, ActiveObservers activeObservers, CommandPool commandPool,
            ProgramArguments programArguments, ApplicationConfiguration applicationConfiguration, RecentFiles recentFiles, AvailableGates availableGates)
        {
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (activeObservers == null) throw new ArgumentNullException("activeObservers");
            if (commandPool == null) throw new ArgumentNullException("commandPool");
            if (programArguments == null) throw new ArgumentNullException("programArguments");
            if (applicationConfiguration == null) throw new ArgumentNullException("applicationConfiguration");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            this.applicationStatus = applicationStatus;
            this.userInterface = userInterface;
            this.activeObservers = activeObservers;
            this.commandPool = commandPool;
            this.programArguments = programArguments;
            this.applicationConfiguration = applicationConfiguration;
            this.recentFiles = recentFiles;
            this.availableGates = availableGates;
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnExiting(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = Exiting;

            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnBeforeExiting()
        {
            EventHandler handler = BeforeExiting;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnExitCanceled()
        {
            EventHandler handler = ExitCanceled;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Start()
        {
            applicationStatus.DefaultStatusText = LocalizedResources.DefaultStatusText;

            activeObservers.Start();

            OpenInitialCatalog();

            OnStarted();
        }

        public bool Exit()
        {
            OnBeforeExiting();

            CancelEventArgs args = new CancelEventArgs();
            OnExiting(args);

            if (!args.Cancel)
                userInterface.Exit();
            else
                OnExitCanceled();

            return !args.Cancel;
        }

        private void OpenInitialCatalog()
        {
            AddressBookLocationInfo fileNameToOpenAtLoad = CalculateInitiallyOpenedFileName();

            if (fileNameToOpenAtLoad == null)
                commandPool.NewAddressBookOperation.Execute();
            else
                commandPool.OpenAddressBookOperation.Execute(fileNameToOpenAtLoad);
        }

        private AddressBookLocationInfo CalculateInitiallyOpenedFileName()
        {
            if (!string.IsNullOrEmpty(programArguments.FileName))
                return new AddressBookLocationInfo
                {
                    FileName = programArguments.FileName,
                    GateId = availableGates.DefaultGate.Id
                };

            switch (applicationConfiguration.LoadFileAtStart)
            {
                case "new":
                    return null;

                case "last":
                    return recentFiles.GetMostRecentFile();

                case "specified":
                    return applicationConfiguration.FileToLoadAtStart;

                default:
                    return null;
            }
        }
    }
}