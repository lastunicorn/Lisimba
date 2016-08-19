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
using System.Collections;
using System.Collections.Generic;
using DustInTheWind.Lisimba.Business.Config;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.Business.Properties;

namespace DustInTheWind.Lisimba.Business.GateManagement
{
    /// <summary>
    /// Manages the gates available in the application.
    /// </summary>
    public class Gates : IEnumerable<IGate>
    {
        private readonly ApplicationConfiguration config;
        private readonly GateProvider gateProvider;
        private readonly List<Exception> warnings;
        private readonly Dictionary<string, IGate> gates;
        private IGate defaultGate;

        public IGate DefaultGate
        {
            get { return defaultGate; }
            set
            {
                defaultGate = value;
                OnGateChanged();
            }
        }

        public string DefaultGateName
        {
            get
            {
                return DefaultGate == null
                    ? string.Empty
                    : string.Format("{0} ({1})", DefaultGate.Name, DefaultGate.Id);
            }
        }

        public string DefaultGateId
        {
            get
            {
                return DefaultGate == null
                    ? string.Empty
                    : DefaultGate.Id;
            }
        }

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        public event EventHandler GateChanged;

        public Gates(GateProvider gateProvider, ApplicationConfiguration config)
        {
            if (gateProvider == null) throw new ArgumentNullException("gateProvider");
            if (config == null) throw new ArgumentNullException("config");

            this.gateProvider = gateProvider;
            this.config = config;

            warnings = new List<Exception>();
            gates = new Dictionary<string, IGate>();

            LoadGates();
        }

        private void LoadGates()
        {
            try
            {
                foreach (IGate gate in gateProvider.GetAllGates())
                    gates.Add(gate.Id, gate);

                SetDefaultGate(config.DefaultGateName);
            }
            catch (Exception ex)
            {
                warnings.Add(ex);
                SetEmptyGate();
            }
        }

        public void SetDefaultGate(string gateId)
        {
            try
            {
                DefaultGate = gates[gateId];
            }
            catch (Exception ex)
            {
                string message = string.Format(Resources.GateNotFoundError, gateId);
                throw new LisimbaException(message, ex);
            }
        }

        public void SetEmptyGate()
        {
            DefaultGate = new EmptyGate();
        }

        public IGate GetGate(string gateId)
        {
            try
            {
                return gates[gateId];
            }
            catch (Exception ex)
            {
                string message = string.Format(Resources.GateNotFoundError, gateId);
                throw new LisimbaException(message, ex);
            }
        }

        public IEnumerable<IGate> GetAllGates()
        {
            return gates.Values;
        }

        protected virtual void OnGateChanged()
        {
            EventHandler handler = GateChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public IEnumerator<IGate> GetEnumerator()
        {
            return gates.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}