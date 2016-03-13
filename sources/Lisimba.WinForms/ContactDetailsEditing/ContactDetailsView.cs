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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.Properties;

namespace DustInTheWind.Lisimba.WinForms.ContactDetailsEditing
{
    internal partial class ContactDetailsView : UserControl
    {
        private CustomObservableCollection<ContactItem> contactItems;
        private List<ContactItemSet> sets;

        private List<ContactDetailsSetView> setViews;
        private List<ContactDetailsSetViewModel> setViewModels;

        public CustomObservableCollection<ContactItem> ContactItems
        {
            get { return contactItems; }
            set
            {
                if (ReferenceEquals(contactItems, value))
                    return;

                contactItems = value;
                DisplayContactItems();
            }
        }

        private List<ContactItemSet> Sets
        {
            get { return sets; }
            set
            {
                sets = value;

                if (sets != null)
                {
                    setViewModels = sets
                        .Select(CreateSetViewModel)
                        .ToList();

                    setViews = setViewModels
                        .Select(CreateSetView)
                        .ToList();
                }

                tableLayoutPanel1.RowCount = setViews.Count;

                setViews.ForEach(x => tableLayoutPanel1.Controls.Add(x));
            }
        }

        private static ContactDetailsSetViewModel CreateSetViewModel(ContactItemSet set)
        {
            return new ContactDetailsSetViewModel
            {
                Image = set.Icon,
                AddButtonImage = set.AddIcon,
                Title = set.Title
            };
        }

        private static ContactDetailsSetView CreateSetView(ContactDetailsSetViewModel setViewModel, int index)
        {
            return new ContactDetailsSetView
            {
                Dock = DockStyle.Fill,
                TabIndex = index,
                ViewModel = setViewModel
            };
        }

        public ContactDetailsView()
        {
            InitializeComponent();

            Sets = new List<ContactItemSet>
            {
                new ContactItemSet
                {
                    Icon = Resources.phone,
                    AddIcon = Resources.phone_add,
                    Title = "Phones",
                    ItemType = typeof(Phone)
                },
                new ContactItemSet
                {
                    Icon = Resources.email,
                    AddIcon = Resources.email_add,
                    Title = "Emails",
                    ItemType = typeof(Email)
                },
                new ContactItemSet
                {
                    Icon = Resources.webaddress,
                    AddIcon = Resources.webaddress_add,
                    Title = "Web Sites",
                    ItemType = typeof(WebSite)
                },
                new ContactItemSet
                {
                    Icon = Resources.address,
                    AddIcon = Resources.address_add,
                    Title = "Addresses",
                    ItemType = typeof(PostalAddress)
                },
                new ContactItemSet
                {
                    Icon = Resources.date,
                    AddIcon = Resources.date_add,
                    Title = "Dates",
                    ItemType = typeof(Date)
                },
                new ContactItemSet
                {
                    Icon = Resources.mesengerid,
                    AddIcon = Resources.mesengerid_add,
                    Title = "Social Profiles",
                    ItemType = typeof(SocialProfile)
                }
            };
        }

        private void DisplayContactItems()
        {
            setViewModels.ForEach(x => x.ContactItems.Clear());

            if (contactItems == null)
                return;

            foreach (ContactItem contactItem in contactItems)
            {
                ContactItemSet set = sets
                    .FirstOrDefault(x => x.ItemType == contactItem.GetType());

                if (set == null)
                    continue;

                int setIndex = sets.IndexOf(set);
                ContactDetailsSetViewModel setViewModel = setViewModels[setIndex];

                setViewModel.ContactItems.Add(contactItem);
            }
        }
    }
}