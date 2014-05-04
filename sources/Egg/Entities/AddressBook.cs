using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using zcsv;
using System.Collections;
using System.Xml;
using DustInTheWind.Lisimba.Egg;
using System.Reflection;
using System.Xml.XPath;

namespace DustInTheWind.Lisimba.Egg
{
    [XmlRoot("Book")]
    [Serializable()]
    public class AddressBook
    {
        #region Properties and Fields

        private string version = string.Empty;
        // Version
        /// <summary>
        /// The version of the application that created this address book.
        /// </summary>
        [XmlElement("Version")]
        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }

        private string name = "New Address Book";
        // Name
        /// <summary>
        /// Gets or sets the name of the address book.
        /// </summary>
        [XmlElement("Name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private ContactCollection contacts = new ContactCollection();
        //Contacts
        /// <summary>
        /// Gets a collection of Contact.
        /// </summary>
        [XmlArray("Contacts"), XmlArrayItem("Contact")]
        public ContactCollection Contacts
        {
            get { return this.contacts; }
        }

        private string fileName = string.Empty;
        // FileName
        /// <summary>
        /// Gets the full file name of the address book or empty string if is a new one.
        /// </summary>
        [XmlIgnore()]
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        #endregion

        #region Events

        //#region Event AddContact

        //public event AddContactHandler AddContact;
        //public delegate void AddContactHandler(object sender, AddContactEventArgs e);

        //public class AddContactEventArgs : EventArgs
        //{
        //    private bool isProblem;
        //    public bool IsProblem
        //    {
        //        get { return isProblem; }
        //    }

        //    private Contact newContact;
        //    public Contact NewContact
        //    {
        //        get { return newContact; }
        //    }

        //    private Contact equalContact;
        //    public Contact EqualContact
        //    {
        //        get { return equalContact; }
        //    }

        //    private ContactCollection almostEqualContacts;
        //    public ContactCollection AlmostEqualContacts
        //    {
        //        get { return almostEqualContacts; }
        //    }

        //    private AddDecision decision = AddDecision.AddAsNew;
        //    public AddDecision Decision
        //    {
        //        get { return decision; }
        //        set { decision = value; }
        //    }


        //    public AddContactEventArgs(bool isProblem, Contact newContact)
        //    {
        //    }
        //}

        //protected virtual void OnAddContact(AddContactEventArgs e)
        //{
        //    if (AddContact != null)
        //    {
        //        AddContact(this, e);
        //    }
        //}

        //#endregion Event AddContact

        #endregion

        #region Constructors

        public AddressBook()
        {
            //Assembly a = Assembly.GetEntryAssembly();
            //if (a == null)
            //    a = Assembly.GetCallingAssembly();
            //if (a == null)
            //    a = Assembly.GetExecutingAssembly();

            Assembly a = Assembly.GetExecutingAssembly();
            AssemblyName name = a.GetName();

            this.version = name.Version.ToString();
        }

        #endregion

        #region Methods

        #region Add Remove

        public int Add(Contact contact)
        {
            bool allowAdd = true;
            
            for (int i = 0; i < this.contacts.Count; i++)
            {
                if (PersonName.Compare(this.contacts[i].Name, contact.Name) == 0)
                {
                    allowAdd = false;
                    break;
                }
            }

            if (allowAdd)
            {
                return this.contacts.Add(contact);
            }
            else
            {
                return -1;
            }
        }

        public int AddRange(ContactCollection contacts, ImportRuleCollection importRules)
        {
            ImportRule rule = null;
            int countAdded = 0;

            for (int i = 0; i < contacts.Count; i++)
            {
                rule = importRules[contacts[i]];
                if (rule != null)
                {
                    switch (rule.ImportType)
                    {
                        case ImportType.AddAsNew:
                            this.Add(contacts[i]);
                            countAdded++;
                            break;

                        case ImportType.Combine:
                            //if (contacts.Contains(rule.OriginalContact))
                            //    rule.OriginalContact.CopyFrom(rule.NewContact);
                            countAdded++;
                            break;

                        case ImportType.Overwrite:
                            if (contacts.Contains(rule.OriginalContact))
                                rule.OriginalContact.CopyFrom(rule.NewContact);
                            countAdded++;
                            break;

                        case ImportType.DoNotAdd:
                            break;

                        default:
                            break;
                    }

                }
            }

            return countAdded;
        }

        public void Remove(Contact contact)
        {
            this.contacts.Remove(contact);
        }

        public void RemoveAt(int index)
        {
            this.contacts.RemoveAt(index);
        }

        #endregion

        public int Count
        {
            get { return this.contacts.Count; }
        }

        public Contact this[int index]
        {
            get { return this.contacts[index]; }
            set { this.contacts[index] = value; }
        }

        #region Sort

        public void Sort(ContactsSortingType sortType, SortDirection sortDirection)
        {
            this.contacts.Sort(sortType, sortDirection);
        }

        #endregion

        public void CopyFrom(AddressBook addressBook)
        {
            this.version = addressBook.version;
            this.name = addressBook.name;
            this.contacts.CopyFrom(addressBook.contacts);
        }

        #endregion
    }
}
