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

using System.ServiceProcess;

namespace DustInTheWind.Desmond.WinService
{
    /// <summary>
    /// <para>
    /// The Windows service that instanciates the <see cref="RedEye"/> class.
    /// </para>
    /// <para>
    /// The application may be started as a desktop application or it can be installed as a Windows service.
    /// This class represents the Windows service.
    /// </para>
    /// </summary>
    public partial class DesmondService : ServiceBase
    {
        private readonly RedEye redEye;

        public DesmondService()
        {
            InitializeComponent();

            redEye = new RedEye();
        }

        protected override void OnStart(string[] args)
        {
            redEye.Start();
        }

        protected override void OnStop()
        {
            redEye.Stop();
        }
    }
}
