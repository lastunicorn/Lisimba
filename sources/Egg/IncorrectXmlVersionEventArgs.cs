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

namespace DustInTheWind.Lisimba.Egg
{
    public class IncorrectXmlVersionEventArgs : EventArgs
    {
        public Version EggVersion { get; private set; }

        public Version XmlVersion { get; private set; }

        public string FileName { get; private set; }

        public bool ContinueParsing { get; set; }

        public IncorrectXmlVersionEventArgs(Version eggVersion, Version xmlVersion, string fileName)
        {
            EggVersion = eggVersion;
            XmlVersion = xmlVersion;
            FileName = fileName;
            ContinueParsing = false;
        }
    }
}