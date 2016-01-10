using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactList
{
    internal class ContactListViewModel : ViewModelBase
    {
        private readonly ConfigurationService configurationService;
        private readonly AddressBooks addressBooks;
        private ContactsSortingType selectedSortingMethod;
        private string searchedText;

        private bool ignoreCurrentContactChange;

        public List<SortingComboBoxItem> SortingMethods { get; private set; }
        public CreateNewContactOperation CreateNewContactOperation { get; private set; }
        public DeleteCurrentContactOperation DeleteCurrentContactOperation { get; private set; }

        public ContactListView View { get; set; }

        public ContactsSortingType SelectedSortingMethod
        {
            get { return selectedSortingMethod; }
            set
            {
                selectedSortingMethod = value;
                OnPropertyChanged();
            }
        }

        public string SearchedText
        {
            get { return searchedText; }
            set
            {
                searchedText = value;

                ContactsToTreeViewBinder.RefreshDisplayedNodes();

                OnPropertyChanged();
            }
        }

        public ContactsToTreeViewBinder ContactsToTreeViewBinder { get; set; }

        public ContactListViewModel(ConfigurationService configurationService, AddressBooks addressBooks, CommandPool commandPool)
        {
            if (configurationService == null) throw new ArgumentNullException("configurationService");
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            if (commandPool == null) throw new ArgumentNullException("commandPool");

            this.configurationService = configurationService;
            this.addressBooks = addressBooks;

            CreateNewContactOperation = commandPool.CreateNewContactOperation;
            DeleteCurrentContactOperation = commandPool.DeleteCurrentContactOperation;

            SortingMethods = new List<SortingComboBoxItem>
            {
                new SortingComboBoxItem {Text = "Birthday (without year)", SortingType = ContactsSortingType.Birthday},
                new SortingComboBoxItem {Text = "Birth Date (age)", SortingType = ContactsSortingType.BirthDate},
                new SortingComboBoxItem {Text = "First Name", SortingType = ContactsSortingType.FirstName},
                new SortingComboBoxItem {Text = "Last Name", SortingType = ContactsSortingType.LastName},
                new SortingComboBoxItem {Text = "Nickname", SortingType = ContactsSortingType.Nickname},
                new SortingComboBoxItem {Text = "Nickname or Name", SortingType = ContactsSortingType.NicknameOrName}
            };

            SelectedSortingMethod = GetSortingType();

            addressBooks.AddressBookChanged += HandleCurrentAddressBookChanged;
            addressBooks.AddressBookSaved += HandleCurrentAddressBookSaved;
            addressBooks.ContactChanged += HandleCurrentContactChanged;

            if (addressBooks.AddressBook != null)
            {
                addressBooks.AddressBook.Changed += HandleCurrentAddressBookContentChanged;
                addressBooks.AddressBook.ContactContentChanged += HandleContactContentChanged;
            }
        }

        private void HandleCurrentAddressBookChanged(object sender, AddressBookChangedEventArgs e)
        {
            if (e.OldAddressBook != null)
            {
                e.OldAddressBook.Changed -= HandleCurrentAddressBookContentChanged;
                e.OldAddressBook.ContactContentChanged -= HandleContactContentChanged;
            }

            if (e.NewAddressBook != null)
            {
                e.NewAddressBook.Changed += HandleCurrentAddressBookContentChanged;
                e.NewAddressBook.ContactContentChanged += HandleContactContentChanged;
            }

            searchedText = string.Empty;
            RepopulateFromCurrentAddressBook();
        }

        private void HandleCurrentAddressBookSaved(object sender, EventArgs eventArgs)
        {
            ResetModifiedFlags();
        }

        private void HandleCurrentAddressBookContentChanged(object sender, EventArgs eventArgs)
        {
            RepopulateFromCurrentAddressBook();
        }

        private void HandleContactContentChanged(object sender, ContactContentChangedEventArgs e)
        {
            Contact c = e.Contact;

            if (c == null)
                return;

            SetContactChangedFlag(c);

            TreeNode treeNode = ContactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            treeNode.Text = treeNode.Tag.ToString();

            View.SortContacts();
        }

        private void HandleCurrentContactChanged(object sender, EventArgs eventArgs)
        {
            if (ignoreCurrentContactChange || View == null)
                return;

            Contact contactToSelect = addressBooks.Contact;

            TreeNode treeNodeToSelect = (contactToSelect == null || !ContactsToTreeViewBinder.treeNodesByContact.ContainsKey(contactToSelect))
                ? null
                : ContactsToTreeViewBinder.treeNodesByContact[contactToSelect];

            View.SelectTreeNode(treeNodeToSelect);
        }

        private void SetContactChangedFlag(Contact c)
        {
            if (c == null)
                return;

            TreeNode treeNode = ContactsToTreeViewBinder.treeNodesByContact[c];

            if (treeNode == null)
                return;

            if (!ContactsToTreeViewBinder.modifiedContacts[c])
            {
                ContactsToTreeViewBinder.modifiedContacts[c] = true;
                HighlightTreeNode(treeNode, true);
            }
        }

        private void RepopulateFromCurrentAddressBook()
        {
            bool currentDataContainsContacts =
                addressBooks != null &&
                addressBooks.AddressBook != null &&
                addressBooks.AddressBook.Contacts != null;

            ContactCollection contacts = currentDataContainsContacts
                ? addressBooks.AddressBook.Contacts
                : null;

            ContactsToTreeViewBinder.Contacts = contacts;
        }

        private ContactsSortingType GetSortingType()
        {
            if (configurationService == null)
                return ContactsSortingType.Birthday;

            switch (configurationService.LisimbaConfigSection.SortBy.Value)
            {
                default:
                case "Birthday":
                    return ContactsSortingType.Birthday;

                case "BirthDate":
                    return ContactsSortingType.BirthDate;

                case "FirstName":
                    return ContactsSortingType.FirstName;

                case "LastName":
                    return ContactsSortingType.LastName;

                case "Nickname":
                    return ContactsSortingType.Nickname;

                case "NicknameOrName":
                    return ContactsSortingType.NicknameOrName;
            }
        }

        public void ContactWasSelected()
        {
            ignoreCurrentContactChange = true;

            try
            {
                TreeNode selectedNode = View.GetSelecteContact();
                Contact selectedContact = selectedNode != null ? (Contact) selectedNode.Tag : null;

                addressBooks.Contact = selectedContact;
            }
            finally
            {
                ignoreCurrentContactChange = false;
            }
        }

        private void ResetModifiedFlags()
        {
            foreach (TreeNode treeNode in ContactsToTreeViewBinder.treeNodesByContact.Values)
            {
                Contact c = treeNode.Tag as Contact;

                if (c == null)
                    continue;

                if (ContactsToTreeViewBinder.modifiedContacts[c])
                {
                    ContactsToTreeViewBinder.modifiedContacts[c] = false;
                    HighlightTreeNode(treeNode, false);
                }
            }
        }

        private static void HighlightTreeNode(TreeNode treeNode, bool value)
        {
            if (value)
            {
                treeNode.ForeColor = Color.Blue;
                treeNode.BackColor = Color.FromArgb(0xf0, 0xf0, 0xf0);
            }
            else
            {
                treeNode.ForeColor = Color.Black;
                treeNode.BackColor = Color.White;
            }
        }

        public void ViewWasLoaded()
        {
            RepopulateFromCurrentAddressBook();
        }
    }
}