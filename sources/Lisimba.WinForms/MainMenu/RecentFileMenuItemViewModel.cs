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
using DustInTheWind.Lisimba.Business.RecentFilesManagement;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.MainMenu
{
    class RecentFileMenuItemViewModel : CustomButtonViewModel
    {
        private AddressBookLocationInfo file;
        private int index;

        public AddressBookLocationInfo File
        {
            get { return file; }
            set
            {
                file = value;

                UpdateText();
            }
        }

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                UpdateText();
            }
        }

        public RecentFileMenuItemViewModel(ApplicationStatus applicationStatus, UserInterface userInterface, IOperation operation)
            : base(applicationStatus, userInterface, operation)
        {
        }

        public override void Execute()
        {
            try
            {
                if (File == null)
                    return;

                operation.Execute(File.FileName);
            }
            catch (Exception ex)
            {
                userInterface.DisplayError(ex.Message);
            }
        }

        private void UpdateText()
        {
            Text = file != null
                ? string.Format("{0} {1}", index, file.FileName)
                : string.Empty;
        }
    }
}