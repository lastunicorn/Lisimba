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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.ActionManagement
{
    public class CreateContactItemAction : IAction
    {
        private readonly CustomObservableCollection<ContactItem> contactItems;
        private readonly ContactItem contactItem;

        public CreateContactItemAction(CustomObservableCollection<ContactItem> contactItems, ContactItem contactItem)
        {
            if (contactItems == null) throw new ArgumentNullException("contactItems");

            this.contactItems = contactItems;
            this.contactItem = contactItem;
        }

        public void Do()
        {
            contactItems.Add(contactItem);
        }

        public void Undo()
        {
            contactItems.Remove(contactItem);
        }
    }
}