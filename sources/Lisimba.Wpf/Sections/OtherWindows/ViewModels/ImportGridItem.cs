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
using DustInTheWind.Lisimba.Business.Importing;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Wpf.Sections.OtherWindows.ViewModels
{
    internal class ImportGridItem
    {
        public IImporter UnderlyingItem { get; private set; }
        public bool IsContact { get; private set; }
        public object LeftValue { get; set; }
        public object RightValue { get; set; }
        public bool HasMergedValue { get; set; }
        public ImportType ImportType { get; set; }

        public ImportGridItem(IImporter importer)
        {
            if (importer == null) throw new ArgumentNullException("importer");
            
            UnderlyingItem = importer;
            IsContact = importer is ContactImporter;
            LeftValue = importer.DestinationValue;
            RightValue = importer.SourceValue;
            ImportType = importer.ImportType;
            HasMergedValue = importer.MergedValue != null;
        }
    }
}