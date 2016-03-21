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
using System.Drawing;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactDetailsEditing
{
    internal class PhoneEditor : IContactItemEditor
    {
        private readonly CustomObservableCollection<ContactItem> contactItems;
        private readonly ActionQueue actionQueue;

        public PhoneEditor(CustomObservableCollection<ContactItem> contactItems, ActionQueue actionQueue)
        {
            if (contactItems == null) throw new ArgumentNullException("contactItems");
            if (actionQueue == null) throw new ArgumentNullException("actionQueue");

            this.contactItems = contactItems;
            this.actionQueue = actionQueue;
        }

        public virtual void CreateNewItem(Point displayLocation)
        {
            PhoneEditForm form = new PhoneEditForm
            {
                EditMode = EditMode.Create,
                ActionQueue = actionQueue,
                ContactItems = contactItems,
                Location = displayLocation
            };

            form.Show();
            form.Focus();
        }

        public virtual void EditItem(ContactItem contactItem, Point displayLocation)
        {
            Phone phone = contactItem as Phone;

            if (phone == null)
                return;

            PhoneEditForm form = new PhoneEditForm
            {
                ActionQueue = actionQueue,
                Phone = phone,
                Location = displayLocation,
                EditMode = EditMode.Edit
            };

            form.Show();
            form.Focus();
        }
    }
}