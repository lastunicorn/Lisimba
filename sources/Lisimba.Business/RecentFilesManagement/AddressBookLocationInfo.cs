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
using System.Text;

namespace DustInTheWind.Lisimba.Business.RecentFilesManagement
{
    public class AddressBookLocationInfo
    {
        public string FileName { get; set; }
        public string GateId { get; set; }

        public AddressBookLocationInfo()
        {
        }

        public AddressBookLocationInfo(string text)
        {
            if (text == null) throw new ArgumentNullException("text");

            Parse(text);
        }

        private void Parse(string text)
        {
            string[] parts = text.Split(';');
            if (parts.Length == 0)
                throw new LisimbaException("Invalid 'LastAddressBook' value in configuration file.");

            if (parts.Length >= 1)
            {
                FileName = parts[0].Trim();

                if (FileName.Length == 0)
                    throw new LisimbaException("Invalid 'LastAddressBook' value in configuration file.");
            }

            if (parts.Length >= 2)
            {
                GateId = parts[1].Trim();

                if (GateId.Length == 0)
                    throw new LisimbaException("Invalid 'LastAddressBook' value in configuration file.");
            }
        }

        public override string ToString()
        {
            if (FileName == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append(FileName);

            if (GateId != null)
                sb.Append(";").Append(GateId);

            return sb.ToString();
        }
    }
}