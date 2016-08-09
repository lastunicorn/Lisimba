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
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Actions
{
    public class RenameAddressBookAction : IAction
    {
        private readonly AddressBook addressBook;
        private readonly string name;
        private string oldName;

        public RenameAddressBookAction(AddressBook addressBook, string name)
        {
            if (addressBook == null) throw new ArgumentNullException("addressBook");

            this.addressBook = addressBook;
            this.name = name;
        }

        public void Do()
        {
            oldName = addressBook.Name;
            addressBook.Name = name;
        }

        public void Undo()
        {
            addressBook.Name = oldName;
        }
    }
}