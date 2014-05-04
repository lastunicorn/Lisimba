using System;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    [XmlRoot("Contact")]
    public class Contact : IComparable
    {
        #region Fields and Properties

        #region Name

        // Name
        private PersonName name = new PersonName();
        [XmlElement("Name")]
        public PersonName Name
        {
            get { return name; }
            set { if (value != null) name = value; }
        }

        // FirstName
        [XmlIgnore()]
        public string FirstName
        {
            get { return name.FirstName; }
            set { name.FirstName = value; }
        }

        // MiddleName
        [XmlIgnore()]
        public string MiddleName
        {
            get { return name.MiddleName; }
            set { name.MiddleName = value; }
        }

        // LastName
        [XmlIgnore()]
        public string LastName
        {
            get { return name.LastName; }
            set { name.LastName = value; }
        }

        // Nickname
        [XmlIgnore()]
        public string Nickname
        {
            get { return name.Nickname; }
            set { name.Nickname = value; }
        }

        #endregion

        #region Birthday

        private Date birthday = new Date();
        // Birthday
        [XmlElement("Birthday")]
        public Date Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        #endregion

        //#region Sign

        //private ZodiacalSign sign;
        //public ZodiacalSign Sign
        //{
        //    get { return sign; }
        //    set
        //    {
        //        if (this.birthday.IsNull())
        //        {
        //            sign = value;
        //        }
        //    }
        //}

        //#endregion

        private string category = string.Empty;
        // Category
        [XmlAttribute("Category")]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
	


        #region Collections

        private PhoneCollection phones = new PhoneCollection();
        // Phones
        [XmlArray("Phones"), XmlArrayItem("Phone")]
        public PhoneCollection Phones
        {
            get { return phones; }
        }

        private EmailCollection emails = new EmailCollection();
        // Emails
        [XmlArray("Emails"), XmlArrayItem("Email")]
        public EmailCollection Emails
        {
            get { return emails; }
        }

        private WebSiteCollection webSites = new WebSiteCollection();
        // WebSites
        [XmlArray("WebSites"), XmlArrayItem("WebSite")]
        public WebSiteCollection WebSites
        {
            get { return webSites; }
        }

        private AddressCollection addresses = new AddressCollection();
        // Addresses
        [XmlArray("Addresses"), XmlArrayItem("Address")]
        public AddressCollection Addresses
        {
            get { return addresses; }
        }

        private DateCollection dates = new DateCollection();
        // Dates
        [XmlArray("Dates"), XmlArrayItem("Date")]
        public DateCollection Dates
        {
            get { return dates; }
        }

        private MessengerIdCollection messengerIds = new MessengerIdCollection();
        // MessengerIds
        [XmlArray("MessengerIds"), XmlArrayItem("MessengerId")]
        //[XmlIgnore()]
        public MessengerIdCollection MessengerIds
        {
            get { return messengerIds; }
        }

        #endregion

        #region Notes

        private string notes = string.Empty;
        // Notes
        [XmlElement("Notes")]
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        #endregion
	

        #endregion Properties

        #region Constructors

        public Contact()
        {
        }

        public Contact(Contact contact)
            : this()
        {
            if (contact != null)
                CopyFrom(contact);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Remove all the information from the current instance.
        /// </summary>
        public void Clear()
        {
            name.Clear();
            birthday.Clear();
            phones.Clear();
            emails.Clear();
            webSites.Clear();
            addresses.Clear();
            dates.Clear();
            messengerIds.Clear();
            notes = string.Empty;
        }

        /// <summary>
        /// Copy all the informations from the specified Contact object to the current instance. All the existing data will be lost.
        /// </summary>
        /// <param name="contact">The Contact object to be copied.</param>
        public void CopyFrom(Contact contact)
        {
            name.CopyFrom(contact.name);

            birthday.CopyFrom(contact.birthday);

            phones.CopyFrom(contact.phones);
            emails.CopyFrom(contact.emails);
            webSites.CopyFrom(contact.webSites);
            addresses.CopyFrom(contact.addresses);
            dates.CopyFrom(contact.dates);
            messengerIds.CopyFrom(contact.messengerIds);

            notes = contact.notes;
        }

        /// <summary>
        /// Returns a string that represent the current Contact.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            string name = string.Empty;

            name = (this.name.FirstName.Length > 0 ? this.name.FirstName : string.Empty);
            name += (this.name.MiddleName.Length > 0 ? (name.Length > 0 ? " " : string.Empty) + this.name.MiddleName : string.Empty);
            name += (this.name.LastName.Length > 0 ? (name.Length > 0 ? " " : string.Empty) + this.name.LastName : string.Empty);

            if (this.name.Nickname.Length > 0)
                str += this.name.Nickname + " (" + name + ")";
            else
                str += name;

            if (str.Length == 0)
                str = "< NA >";

            return str;
        }

        public ZodiacSign ZogiacSign
        {
            get
            {
                if (birthday.Month == 0 || birthday.Day == 0)
                    return ZodiacSign.NotSpecified;

                switch (birthday.Month)
                {
                    case 1:
                        if (birthday.Day <= 19)
                            return ZodiacSign.Capricorn;
                        else
                            return ZodiacSign.Aquarius;

                    case 2:
                        if (birthday.Day <= 18)
                            return ZodiacSign.Aquarius;
                        else
                            return ZodiacSign.Pisces;

                    case 3:
                        if (birthday.Day <= 20)
                            return ZodiacSign.Pisces;
                        else
                            return ZodiacSign.Aries;

                    case 4:
                        if (birthday.Day <= 19)
                            return ZodiacSign.Aries;
                        else
                            return ZodiacSign.Taurus;

                    case 5:
                        if (birthday.Day <= 20)
                            return ZodiacSign.Taurus;
                        else
                            return ZodiacSign.Gemini;

                    case 6:
                        if (birthday.Day <= 20)
                            return ZodiacSign.Gemini;
                        else
                            return ZodiacSign.Cancer;

                    case 7:
                        if (birthday.Day <= 22)
                            return ZodiacSign.Cancer;
                        else
                            return ZodiacSign.Leo;

                    case 8:
                        if (birthday.Day <= 22)
                            return ZodiacSign.Leo;
                        else
                            return ZodiacSign.Virgo;

                    case 9:
                        if (birthday.Day <= 22)
                            return ZodiacSign.Virgo;
                        else
                            return ZodiacSign.Libra;

                    case 10:
                        if (birthday.Day <= 22)
                            return ZodiacSign.Libra;
                        else
                            return ZodiacSign.Scorpio;

                    case 11:
                        if (birthday.Day <= 21)
                            return ZodiacSign.Scorpio;
                        else
                            return ZodiacSign.Sagittarius;

                    case 12:
                        if (birthday.Day <= 21)
                            return ZodiacSign.Sagittarius;
                        else
                            return ZodiacSign.Capricorn;

                    default:
                        return ZodiacSign.NotSpecified;
                }
            }
        }

        #region Search

        /// <summary>
        /// Returns the Phone object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The Phone object that match or null.</returns>
        public Phone SearchPhoneByDescription(string text, SearchMode searchMode)
        {
            Phone p;

            for (int i = 0; i < phones.Count; i++)
            {
                p = phones[i];

                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (p.Description.CompareTo(text) == 0)
                            return p;
                        break;

                    case SearchMode.StartingWith:
                        if (p.Description.StartsWith(text))
                            return p;
                        break;

                    case SearchMode.EndingWith:
                        if (p.Description.EndsWith(text))
                            return p;
                        break;

                    case SearchMode.Containing:
                        if (p.Description.IndexOf(text) > 0)
                            return p;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the Email object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The Email object that match or null.</returns>
        public Email SearchEmailByDescription(string text, SearchMode searchMode)
        {
            Email e;

            for (int i = 0; i < emails.Count; i++)
            {
                e = emails[i];

                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (e.Description.CompareTo(text) == 0)
                            return e;
                        break;

                    case SearchMode.StartingWith:
                        if (e.Description.StartsWith(text))
                            return e;
                        break;

                    case SearchMode.EndingWith:
                        if (e.Description.EndsWith(text))
                            return e;
                        break;

                    case SearchMode.Containing:
                        if (e.Description.IndexOf(text) > 0)
                            return e;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the WebSite object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The WebSite object that match or null.</returns>
        public WebSite SearchWebSiteByDescription(string text, SearchMode searchMode)
        {
            WebSite w;

            for (int i = 0; i < webSites.Count; i++)
            {
                w = webSites[i];

                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (w.Description.CompareTo(text) == 0)
                            return w;
                        break;

                    case SearchMode.StartingWith:
                        if (w.Description.StartsWith(text))
                            return w;
                        break;

                    case SearchMode.EndingWith:
                        if (w.Description.EndsWith(text))
                            return w;
                        break;

                    case SearchMode.Containing:
                        if (w.Description.IndexOf(text) > 0)
                            return w;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the Address object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The Address object that match or null.</returns>
        public Address SearchAddressByDescription(string text, SearchMode searchMode)
        {
            Address a;

            for (int i = 0; i < addresses.Count; i++)
            {
                a = addresses[i];

                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (a.Description.CompareTo(text) == 0)
                            return a;
                        break;

                    case SearchMode.StartingWith:
                        if (a.Description.StartsWith(text))
                            return a;
                        break;

                    case SearchMode.EndingWith:
                        if (a.Description.EndsWith(text))
                            return a;
                        break;

                    case SearchMode.Containing:
                        if (a.Description.IndexOf(text) > 0)
                            return a;
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the MessengerId object that match the description.
        /// </summary>
        /// <param name="text">The text to search in the description field.</param>
        /// <param name="searchMode">Indicates the search mode. (Ex: StartingWith, Containing, etc...)</param>
        /// <returns>The MessengerId object that match or null.</returns>
        public MessengerId SearchMessengeIdByDescription(string text, SearchMode searchMode)
        {
            MessengerId id;

            for (int i = 0; i < messengerIds.Count; i++)
            {
                id = messengerIds[i];

                switch (searchMode)
                {
                    case SearchMode.Exact:
                        if (id.Description.CompareTo(text) == 0)
                            return id;
                        break;

                    case SearchMode.StartingWith:
                        if (id.Description.StartsWith(text))
                            return id;
                        break;

                    case SearchMode.EndingWith:
                        if (id.Description.EndsWith(text))
                            return id;
                        break;

                    case SearchMode.Containing:
                        if (id.Description.IndexOf(text) > 0)
                            return id;
                        break;
                }
            }

            return null;
        }

        #endregion Search

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Contact)
            {
                Contact contact = (Contact)obj;
                int returnValue = 0;

                if ((returnValue = name.Nickname.CompareTo(contact.name.Nickname)) == 0)
                {
                    if ((returnValue = name.FirstName.CompareTo(contact.name.FirstName)) == 0)
                    {
                        if ((returnValue = name.MiddleName.CompareTo(contact.name.MiddleName)) == 0)
                        {
                            returnValue = name.LastName.CompareTo(contact.name.LastName);
                        }
                    }
                }
                return returnValue;
            }

            throw new ArgumentException("object is not a Contact.");
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (!(obj is Contact)) return false;

            Contact contact = (Contact)obj;

            if (!name.Equals(contact.name)) return false;

            if (!birthday.Equals(contact.birthday)) return false;

            if (!phones.Equals(contact.phones)) return false;
            if (!emails.Equals(contact.emails)) return false;
            if (!webSites.Equals(contact.webSites)) return false;
            if (!addresses.Equals(contact.addresses)) return false;
            if (!dates.Equals(contact.dates)) return false;
            if (!messengerIds.Equals(contact.messengerIds)) return false;

            if (!notes.Equals(contact.notes)) return false;

            return true;
        }

        #endregion Methods

    }
}