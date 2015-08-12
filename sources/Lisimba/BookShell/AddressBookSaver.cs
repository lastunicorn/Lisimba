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
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.BookShell
{
    class AddressBookSaver
    {
        private readonly UserInterface userInterface;
        private readonly ApplicationStatus applicationStatus;

        public IGate Gate { get; set; }
        public AddressBook AddressBook { get; set; }
        public string FileName { get; set; }

        public AddressBookSaver(UserInterface userInterface, ApplicationStatus applicationStatus)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.userInterface = userInterface;
            this.applicationStatus = applicationStatus;
        }

        public AddressBookSaverResult Save()
        {
            if (Gate == null)
                throw new Exception("The Gate was not provided.");

            if (AddressBook == null)
                throw new Exception("The AddressBook was not provided.");

            bool isSavedInNewLocation = FileName == null;

            if (FileName == null)
                FileName = userInterface.AskToSaveLsbFile();

            if (FileName == null)
                return new AddressBookSaverResult { Success = false };

            Gate.Save(AddressBook, FileName);

            DisplaySuccessStatusText();

            return new AddressBookSaverResult
            {
                Success = true,
                FileName = FileName,
                IsSavedInNewLocation = isSavedInNewLocation
            };
        }

        private void DisplaySuccessStatusText()
        {
            int contactCount = AddressBook.Contacts.Count;
            applicationStatus.StatusText = string.Format(Resources.AddressBookSaved_StatusText, contactCount);
        }
    }
}