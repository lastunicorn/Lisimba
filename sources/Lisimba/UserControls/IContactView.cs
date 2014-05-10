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

using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.UserControls
{
    public interface IContactView
    {
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Nickname { get; set; }
        string Birthday { get; set; }
        string Notes { get; set; }
        PhoneCollection Phones { get; set; }
        EmailCollection Emails { get; set; }
        WebSiteCollection WebSites { get; set; }
        AddressCollection Addresses { get; set; }
        DateCollection Dates { get; set; }
        MessengerIdCollection MessengerIds { get; set; }
    }
}