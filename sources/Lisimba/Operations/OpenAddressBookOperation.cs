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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Gating;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class OpenAddressBookOperation : ExecutableViewModelBase<string>
    {
        private readonly AddressBookShell addressBookShell;
        private readonly UserInterface userInterface;
        private readonly RecentFiles recentFiles;

        public override string ShortDescription
        {
            get { return LocalizedResources.OpenAddressBookOperationDescription; }
        }

        public OpenAddressBookOperation(AddressBookShell addressBookShell, UserInterface userInterface, ApplicationStatus applicationStatus, RecentFiles recentFiles)
            : base(applicationStatus)
        {
            if (addressBookShell == null) throw new ArgumentNullException("addressBookShell");
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (recentFiles == null) throw new ArgumentNullException("recentFiles");

            this.addressBookShell = addressBookShell;
            this.userInterface = userInterface;
            this.recentFiles = recentFiles;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                ZipXmlGate gate = new ZipXmlGate();
                bool succeeded = addressBookShell.LoadFrom(gate, fileName);

                if (!succeeded)
                    return;

                DisplaySuccessStatusText();
                AddFileToRecentFileList();
                DisplayWarnings(gate);
                DisplayBirthdays();
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }

        private void DisplaySuccessStatusText()
        {
            int contactsCount = addressBookShell.AddressBook.Contacts.Count;
            applicationStatus.StatusText = string.Format(Resources.OpenAddressBook_SuccessStatusText, contactsCount);
        }

        private void AddFileToRecentFileList()
        {
            string fileFullPath = Path.GetFullPath(addressBookShell.FileName);
            recentFiles.AddRecentFile(fileFullPath);
        }

        private void DisplayWarnings(ZipXmlGate gate)
        {
            if (!gate.Warnings.Any())
                return;

            StringBuilder sb = new StringBuilder();

            foreach (Exception warning in gate.Warnings)
            {
                sb.AppendLine(warning.Message);
                sb.AppendLine();
            }

            userInterface.DisplayWarning(sb.ToString());
        }

        private void DisplayBirthdays()
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);
            List<Contact> contacts = addressBookShell.AddressBook.GetBirthdays(startDate, endDate).ToList();

            if (contacts.Count <= 0)
                return;

            StringBuilder sb = new StringBuilder();

            double totalDays = (endDate - startDate).TotalDays;
            sb.AppendLine("The birthdays for the next " + totalDays + " days are:");
            sb.AppendLine();

            foreach (Contact contact in contacts)
            {
                string line = string.Format("{0} - {1}", contact.Name, contact.Birthday.ToShortString());
                sb.AppendLine(line);
            }

            userInterface.DisplayInfo(sb.ToString());
        }
    }
}