﻿// Lisimba
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
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms.LocationProviders
{
    //        openFileDialog.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
    //        openFileDialog.DefaultExt = "csv";

    internal class FileLocationProvider
    {
        private readonly WindowSystem windowSystem;

        public FileTypeInfo FileTypeInfo { get; private set; }
        public bool DisplayAllFilesFilter { get; set; }

        public FileLocationProvider(WindowSystem windowSystem)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.windowSystem = windowSystem;

            FileTypeInfo = new FileTypeInfo
            {
                Extension = "lsb",
                FileTypeName = "Lisimba Files"
            };
            DisplayAllFilesFilter = true;
        }

        public string AskToSave()
        {
            return windowSystem.AskToSave(FileTypeInfo.Extension, BuildFilter());
        }

        public string AskToOpen()
        {
            return windowSystem.AskToOpen(FileTypeInfo.Extension, BuildFilter());
        }

        private string BuildFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0} (*.{1})|*.{1}", FileTypeInfo.FileTypeName, FileTypeInfo.Extension));

            if (DisplayAllFilesFilter)
                sb.Append("|All Files (*.*)|*.*");

            return sb.ToString();
        }
    }
}
