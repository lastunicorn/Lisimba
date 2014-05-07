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

namespace DustInTheWind.Lisimba
{
    class ProgramArguments
    {
        private readonly string fileName = string.Empty;

        public string FileName
        {
            get { return fileName; }
        }

        public ProgramArguments(string[] args)
        {
            CmdLineArgs argList = new CmdLineArgs(args);

            if (argList.Count > 0)
                fileName = argList[0];

            //if (argList.ContainsKey("f"))
            //    fileName = argList["f"];

            //if (argList.ContainsKey("filename"))
            //    fileName = argList["filename"];
        }
    }
}
