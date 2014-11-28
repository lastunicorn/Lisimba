// Lisimba
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
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Gating;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class OpenAddressBookCommand : CommandBase<string>
    {
        private readonly CurrentData currentData;
        private readonly UiService uiService;
        private readonly StatusService statusService;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return "Open address book from file."; }
        }

        public Func<bool> AskIfAllowToContinue;

        public OpenAddressBookCommand(CurrentData currentData, UiService uiService, StatusService statusService, RecentFiles recentFiles)
        {
            if (currentData == null) throw new ArgumentNullException("currentData");
            if (uiService == null) throw new ArgumentNullException("uiService");
            if (statusService == null) throw new ArgumentNullException("statusService");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.currentData = currentData;
            this.uiService = uiService;
            this.statusService = statusService;
            this.recentFiles = recentFiles;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                bool allowToContinue = AskIfAllowToContinue == null || AskIfAllowToContinue();

                if (!allowToContinue)
                    return;

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = uiService.AskToOpenLsbFile();

                    if (fileName == null)
                        return;
                }

                ZipXmlGate gate = new ZipXmlGate();
                AddressBook openedAddressBook = gate.Load(fileName);
                openedAddressBook.SetAsSaved();

                currentData.AddressBook = openedAddressBook;

                statusService.StatusText = string.Format("{0} contacts oppened.", openedAddressBook.Contacts.Count);
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
