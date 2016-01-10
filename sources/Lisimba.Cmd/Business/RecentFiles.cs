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

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class RecentFiles
    {
        private readonly ApplicationConfiguration config;

        public event EventHandler<EventArgs> FileNameAdded;

        protected virtual void OnFileNameAdded(EventArgs e)
        {
            EventHandler<EventArgs> handler = FileNameAdded;

            if (handler != null)
                handler(this, e);
        }

        public RecentFiles(ApplicationConfiguration config)
        {
            if (config == null) throw new ArgumentNullException("config");

            this.config = config;
        }

        public AddressBookLocationInfo GetMostRecentFileName()
        {
            return config.LastAddressBook;
        }

        public void AddRecentFile(string fileName, IGate gate)
        {
            config.LastAddressBook = new AddressBookLocationInfo
            {
                FileName = fileName,
                GateId = gate.Id
            };
        }
    }
}