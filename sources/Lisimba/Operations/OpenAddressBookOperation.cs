﻿// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.IO;
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    class OpenAddressBookOperation : OperationBase<string>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UiService uiService;
        private readonly ApplicationStatus applicationStatus;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return Resources.OpenAddressBookOperationDescription; }
        }

        public OpenAddressBookOperation(AddressBookShell addressBookShell, UiService uiService, ApplicationStatus applicationStatus, RecentFiles recentFiles)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.addressBookShell = addressBookShell;
            this.uiService = uiService;
            this.applicationStatus = applicationStatus;
            this.recentFiles = recentFiles;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                bool allowToContinue = addressBookShell.EnsureIsSaved();

                if (!allowToContinue)
                    return;

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = uiService.AskToOpenLsbFile();

                    if (fileName == null)
                        return;
                }

                ZipXmlGate gate = new ZipXmlGate();
                addressBookShell.LoadFrom(gate, fileName);

                applicationStatus.StatusText = string.Format("{0} contacts oppened.", addressBookShell.AddressBook.Contacts.Count);
                recentFiles.AddRecentFile(Path.GetFullPath(fileName));

                if (gate.Warnings.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (Exception warning in gate.Warnings)
                    {
                        sb.AppendLine(warning.Message);
                        sb.AppendLine();
                    }

                    uiService.DisplayWarning(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}