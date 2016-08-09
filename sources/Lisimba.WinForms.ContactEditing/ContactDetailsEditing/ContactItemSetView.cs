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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Utils;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactDetailsEditing
{
    internal partial class ContactItemSetView : UserControl
    {
        private ContactItemSetViewModel viewModel;
        private ObservableCollection<ContactItem> contactItems;

        public ContactItemSetViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                pictureBoxIcon.DataBindings.Clear();

                viewModel = value;

                if (viewModel != null)
                {
                    pictureBoxIcon.Bind(x => x.Image, viewModel, x => x.Image, true, DataSourceUpdateMode.OnPropertyChanged);
                    buttonAdd.Bind(x => x.Image, viewModel, x => x.AddButtonImage, true, DataSourceUpdateMode.OnPropertyChanged);
                    labelTitle.Bind(x => x.Text, viewModel, x => x.Title, true, DataSourceUpdateMode.OnPropertyChanged);

                    this.Bind(x => x.ContactItems, viewModel, x => x.ContactItems, true, DataSourceUpdateMode.Never);
                }
            }
        }

        public ObservableCollection<ContactItem> ContactItems
        {
            get { return contactItems; }
            set
            {
                if (contactItems != null)
                    contactItems.CollectionChanged -= HandleContactItemsCollectionChanged;

                flowLayoutPanel1.Controls.Clear();

                contactItems = value;

                if (contactItems != null)
                {
                    contactItems.CollectionChanged += HandleContactItemsCollectionChanged;
                    AddItems(contactItems, 0);
                }
            }
        }

        public IContactItemEditor ItemEditor { get; set; }

        public ContactItemSetView()
        {
            InitializeComponent();
        }

        private void HandleContactItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e.NewItems, e.NewStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    flowLayoutPanel1.Controls.Clear();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ContactItemView GetControlFor(ContactItem contactItem)
        {
            return flowLayoutPanel1.Controls
                .OfType<ContactItemView>()
                .FirstOrDefault(x => x.ContactItem == contactItem);
        }

        private void AddItems(IEnumerable newItems, int newStartingIndex)
        {
            Control[] controls = newItems
                .OfType<ContactItem>()
                .Select(x => new ContactItemView
                {
                    ContactItem = x
                })
                .Cast<Control>()
                .Reverse()
                .ToArray();

            flowLayoutPanel1.SuspendLayout();

            foreach (Control control in controls)
            {
                flowLayoutPanel1.Controls.Add(control);
                flowLayoutPanel1.Controls.SetChildIndex(control, newStartingIndex);
            }

            flowLayoutPanel1.ResumeLayout();
        }

        private void RemoveItems(IEnumerable oldItems)
        {
            IEnumerable<Control> controls = oldItems
                .OfType<ContactItem>()
                .Select(GetControlFor)
                .Where(x => x != null);

            flowLayoutPanel1.SuspendLayout();

            foreach (Control control in controls)
            {
                flowLayoutPanel1.Controls.Remove(control);
            }

            flowLayoutPanel1.ResumeLayout();
        }

        private void HandleButtonAddClick(object sender, EventArgs e)
        {
            if (ItemEditor != null)
                ItemEditor.CreateNewItem(buttonAdd.GetBottomLeftCorner());
        }

        private void HandleResize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void HandleLabelTitleResize(object sender, EventArgs e)
        {
            labelTitle.Invalidate();
        }

        private void HandleFlowLayoutPanel1Resize(object sender, EventArgs e)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.MinimumSize = new Size(flowLayoutPanel1.Width, 0);
                control.MaximumSize = new Size(flowLayoutPanel1.Width, 0);
            }
        }
    }
}