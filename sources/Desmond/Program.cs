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

using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System;
using System.Windows.Forms;

namespace DustInTheWind.Desmond
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Arguments arguments = new Arguments(args);

            string startAs = arguments["startas"];
            if (startAs != null && startAs.ToLower().Equals("app"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DesmondForm());
            }
            else if (startAs == null || startAs.ToLower().Equals("service"))
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new DesmondService() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}