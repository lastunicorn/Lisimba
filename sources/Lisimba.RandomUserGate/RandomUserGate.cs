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
using System.Collections.Generic;
using System.Drawing;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.RandomUserGate.Properties;

namespace DustInTheWind.Lisimba.RandomUserGate
{
    public class RandomUserGate : GateBase
    {
        public override string Id
        {
            get { return "RandomUserGate"; }
        }

        public override string Name
        {
            get { return "Random User Gate"; }
        }

        public override string Description
        {
            get { return "Randomly generates a number of contacts using a free web service."; }
        }

        public override Image Icon16
        {
            get { return Resources.randomuser_16; }
        }

        public override bool CanLoad
        {
            get { return true; }
        }

        public override bool CanSave
        {
            get { return false; }
        }

        public override void Save(AddressBook addressBook, string connectionString)
        {
            throw new NotSupportedException();
        }

        public override AddressBook Load(string connectionString)
        {
            AddressBook addressBook = new AddressBook();

            List<Contact> contacts = RandomUserProvider.RetrieveUsers(100);
            addressBook.Contacts.AddRange(contacts);

            return addressBook;
        }
    }
}