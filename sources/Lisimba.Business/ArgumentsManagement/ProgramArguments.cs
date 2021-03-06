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

namespace DustInTheWind.Lisimba.Business.ArgumentsManagement
{
    public class ProgramArguments
    {
        public string FileName { get; private set; }

        public void Initialize(string[] args)
        {
            CommandLineArguments commandLineArguments = new CommandLineArguments(args);

            if (commandLineArguments.Count > 0)
                FileName = commandLineArguments[0];

            //if (argList.ContainsKey("f"))
            //    fileName = argList["f"];

            //if (argList.ContainsKey("filename"))
            //    fileName = argList["filename"];
        }
    }
}