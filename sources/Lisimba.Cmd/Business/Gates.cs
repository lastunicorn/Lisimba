// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using Lisimba.Cmd.Properties;

namespace Lisimba.Cmd.Business
{
    class Gates
    {
        private readonly ApplicationConfiguration config;
        private readonly GateProvider gateProvider;

        public IGate DefaultGate { get; set; }

        public string DefaultGateName
        {
            get { return DefaultGate == null ? string.Empty : DefaultGate.Id; }
        }

        public Gates(ApplicationConfiguration config, GateProvider gateProvider)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (gateProvider == null) throw new ArgumentNullException("gateProvider");

            this.config = config;
            this.gateProvider = gateProvider;

            InitializeDefaultGate();
        }

        private void InitializeDefaultGate()
        {
            try
            {
                DefaultGate = gateProvider.GetGate(config.DefaultGateName);
            }
            catch
            {
                DefaultGate = new EmptyGate();
            }
        }

        public void SetDefaultGate(string gateId)
        {
            try
            {
                DefaultGate = gateProvider.GetGate(gateId);
            }
            catch (Exception ex)
            {
                string message = string.Format(Resources.GateNotFoundError, gateId);
                throw new Exception(message, ex);
            }
        }
    }
}