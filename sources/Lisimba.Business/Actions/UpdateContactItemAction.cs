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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Actions
{
    public class UpdateContactItemAction : IAction
    {
        private readonly ContactItem contactItem;
        private readonly ContactItem newContactItem;
        private ContactItem oldContactItem;

        public UpdateContactItemAction(ContactItem contactItem, ContactItem newContactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");
            if (newContactItem == null) throw new ArgumentNullException("newContactItem");

            this.contactItem = contactItem;
            this.newContactItem = newContactItem;
        }

        public void Do()
        {
            oldContactItem = contactItem.Clone();

            contactItem.CopyFrom(newContactItem);
        }

        public void Undo()
        {
            contactItem.CopyFrom(oldContactItem);
        }
    }
}