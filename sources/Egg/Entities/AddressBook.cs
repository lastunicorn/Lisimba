// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.Reflection;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [XmlRoot("Book")]
    [Serializable()]
    public class AddressBook
    {
        // Version
        /// <summary>
        /// The version of the application that created this address book.
        /// </summary>
        [XmlElement("Version")]
        public string Version { get; set; }

        // Name
        /// <summary>
        /// Gets or sets the name of the address book.
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        private ContactCollection contacts;
        //Contacts
        /// <summary>
        /// Gets a collection of Contact.
        /// </summary>
        [XmlArray("Contacts"), XmlArrayItem("Contact")]
        public ContactCollection Contacts
        {
            get { return contacts; }
        }

        // FileName
        /// <summary>
        /// Gets the full file name of the address book or empty string if is a new one.
        /// </summary>
        [XmlIgnore()]
        public string FileName { get; set; }

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

        public AddressBook()
        {
            Name = "New Address Book";
            contacts = new ContactCollection();
            FileName = string.Empty;

            Version = GetCurrentAssemblyVersion();
        }

        private static string GetCurrentAssemblyVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();

            return assemblyName.Version.ToString();
        }

        public int Add(Contact contact)
        {
            bool allowAdd = IsAllowToAdd(contact);

            if (!allowAdd)
                return -1;

            return Contacts.Add(contact);
        }

        private bool IsAllowToAdd(Contact contact)
        {
            for (int i = 0; i < Contacts.Count; i++)
            {
                if (PersonName.Compare(Contacts[i].Name, contact.Name) == 0)
                    return false;
            }

            return true;
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
                            Add(contacts[i]);
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
                    }

                }
            }

            return countAdded;
        }

        public void Remove(Contact contact)
        {
            Contacts.Remove(contact);
        }

        public void RemoveAt(int index)
        {
            Contacts.RemoveAt(index);
        }

        public int Count
        {
            get { return Contacts.Count; }
        }

        public Contact this[int index]
        {
            get { return Contacts[index]; }
            set { Contacts[index] = value; }
        }

        public void Sort(ContactsSortingType sortType, SortDirection sortDirection)
        {
            Contacts.Sort(sortType, sortDirection);
        }
    }
}
