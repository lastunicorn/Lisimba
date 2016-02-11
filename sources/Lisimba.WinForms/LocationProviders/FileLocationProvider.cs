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
using System.Text;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.LocationProviders
{
    class FileLocationProvider
    {
        private readonly UserInterface userInterface;

        public string Extension { get; set; }
        public string FileTypeName { get; set; }
        public bool DisplayAllFilesFilter { get; set; }

        public FileLocationProvider(UserInterface userInterface)
        {
            if (userInterface == null) throw new ArgumentNullException("userInterface");

            this.userInterface = userInterface;

            Extension = "lsb";
            FileTypeName = "Lisimba Files";
            DisplayAllFilesFilter = true;
        }

        public string AskToSave()
        {
            return userInterface.AskToSave(Extension, BuildFilter());
        }

        public string AskToOpen()
        {
            return userInterface.AskToOpen(Extension, BuildFilter());
        }

        private string BuildFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} (*.{1})|*.{1}", FileTypeName, Extension));

            if (DisplayAllFilesFilter)
                sb.Append("|All Files (*.*)|*.*");

            return sb.ToString();
        }
    }
}
