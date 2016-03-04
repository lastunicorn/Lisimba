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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Sorting;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.ContactList
{
    internal partial class ContactListView : UserControl
    {
        private readonly ContactsToTreeViewBinder contactsToTreeViewBinder;

        private ContactListViewModel viewModel;

        public ContactListViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                viewModel = value;

                if (viewModel != null)
                {
                    viewModel.ContactsToTreeViewBinder = contactsToTreeViewBinder;

                    comboBoxSortBy.DataSource = viewModel.SortingMethods;
                    comboBoxSortBy.DisplayMember = BindingExtensions.GetName<SortingComboBoxItem>(x => x.Text);
                    comboBoxSortBy.ValueMember = BindingExtensions.GetName<SortingComboBoxItem>(x => x.SortingType);

                    comboBoxSortBy.Bind(x => x.SelectedValue, ViewModel, x => x.SelectedSortingMethod, false, DataSourceUpdateMode.OnPropertyChanged);
                    textBoxSearch.Bind(x => x.Text, viewModel, x => x.SearchedText, false, DataSourceUpdateMode.OnPropertyChanged);

                    toolStripMenuItem_List_Add.ViewModel = ViewModel.ContactMenuViewModels.AddContactViewModel;
                    toolStripMenuItem_List_Delete.ViewModel = ViewModel.ContactMenuViewModels.DeleteContactViewModel;
                    toolStripMenuItem_List_ViewBiorhythm.ViewModel = ViewModel.ContactMenuViewModels.BiorhythmViewModel;
                }
            }
        }

        public ContactListView()
        {
            InitializeComponent();

            treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);

            contactsToTreeViewBinder = new ContactsToTreeViewBinder(treeViewContacts);

            contactsToTreeViewBinder.Filter = contact => textBoxSearch.Text.Length == 0 ||
                                                         contact.Name.FirstName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.MiddleName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.LastName.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                                                         contact.Name.Nickname.IndexOf(textBoxSearch.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void treeViewContacts_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewContacts.SelectedNode = e.Node;
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Right)
            //    return;

            //TreeNode node = treeViewContacts.GetNodeAt(e.Location);

            //if (node != null)
            //    treeViewContacts.SelectedNode = node;

            //// Display the menu
            //Contact selectedContact = currentData.Contact;
            //toolStripMenuItem_List_ViewBiorhythm.Enabled = (selectedContact != null && selectedContact.Birthday.IsCompleteDate);
            //contextMenuStripListBox.Show(treeViewContacts, e.Location);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ViewModel.ContactWasSelected();
        }

        private void comboBoxSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ViewModel.SelectedSortingMethod)
            {
                case ContactsSortingType.Birthday:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Birthday);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.BirthDate:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.BirthDate);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.FirstName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.FirstName);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.LastName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.LastName);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.Nickname:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeComparer(ContactsSortingType.Nickname);
                    treeViewContacts.Sort();
                    break;

                case ContactsSortingType.NicknameOrName:
                    treeViewContacts.TreeViewNodeSorter = new TreeNodeByNicknameOrNameComparer();
                    treeViewContacts.Sort();
                    break;
            }
        }

        private void ContactListView_Load(object sender, EventArgs e)
        {
            if (viewModel != null)
                viewModel.ViewWasLoaded();
        }

        public void SortContacts()
        {
            treeViewContacts.Sort();
        }

        public void SelectTreeNode(TreeNode treeNodeToSelect)
        {
            treeViewContacts.SelectedNode = treeNodeToSelect;
        }

        public TreeNode GetSelecteContact()
        {
            return treeViewContacts.SelectedNode;
        }
    }
}