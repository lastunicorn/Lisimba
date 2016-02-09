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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using zcsv;
using System.Drawing;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.Egg.Searching;
using DustInTheWind.Lisimba.Gating.Properties;

namespace DustInTheWind.Lisimba.Gating
{
    public class YahooCsvGate : IGate
    {
        public IEnumerable<Exception> Warnings { get; private set; }

        public string Id
        {
            get { return "YahooCsvGate"; }
        }

        public string Name
        {
            get { return "Yahoo CSV Gate"; }
        }

        public string Description
        {
            get { return "A Gate that knows to save and load address books from a Yahoo csv file."; }
        }

        public Image Icon16
        {
            get { return Resources.yahoo_icon; }
        }

        public YahooCsvGate()
        {
            Warnings = new Exception[0];
        }

        public AddressBook Load(string fileName)
        {
            AddressBook addressBook = new AddressBook
            {
                Version = string.Empty,
                Name = string.Empty
            };

            Csv csv = new Csv();
            csv.oOptions.bFirstLineIsHeader = true;
            csv.LoadFromFile(fileName);

            for (int i = 0; i < csv.Count - 1; i++)
            {
                Contact contact = FromCsvRecord(csv[i]);
                addressBook.Contacts.Add(contact);
            }

            return addressBook;
        }

        private Contact FromCsvRecord(CsvRecord csvRecord)
        {
            Contact contact = new Contact();

            // First
            contact.Name.FirstName = csvRecord[0];

            // Middle
            contact.Name.MiddleName = csvRecord[1];

            // Last
            contact.Name.LastName = csvRecord[2];

            // Nickname
            contact.Name.Nickname = csvRecord[3];


            // Email
            if (csvRecord[4].Length > 0) contact.Items.Add(new Email(csvRecord[4], "Email"));


            // Category
            // Distribution Lists


            // Messenger ID
            if (csvRecord[7].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[7], "Yahoo!"));


            // Home
            if (csvRecord[8].Length > 0) contact.Items.Add(new Phone(csvRecord[8], "Home"));

            // Work
            if (csvRecord[9].Length > 0) contact.Items.Add(new Phone(csvRecord[9], "Work"));

            // Pager
            if (csvRecord[10].Length > 0) contact.Items.Add(new Phone(csvRecord[10], "Pager"));

            // Fax
            if (csvRecord[11].Length > 0) contact.Items.Add(new Phone(csvRecord[11], "Fax"));

            // Mobile
            if (csvRecord[12].Length > 0) contact.Items.Add(new Phone(csvRecord[12], "Mobile"));

            // Other
            if (csvRecord[13].Length > 0) contact.Items.Add(new Phone(csvRecord[13], "Other"));

            // Yahoo! Phone
            if (csvRecord[14].Length > 0) contact.Items.Add(new Phone(csvRecord[14], "Yahoo! Phone"));


            // Primary


            // Alternate Email 1
            if (csvRecord[16].Length > 0) contact.Items.Add(new Email(csvRecord[16], "Alternate Email 1"));

            // Alternate Email 2
            if (csvRecord[17].Length > 0) contact.Items.Add(new Email(csvRecord[17], "Alternate Email 2"));


            // Personal Website
            if (csvRecord[18].Length > 0) contact.Items.Add(new WebSite(csvRecord[18], "Personal Website"));

            // Business Website
            if (csvRecord[19].Length > 0) contact.Items.Add(new WebSite(csvRecord[19], "Business Website"));


            // Title
            if (csvRecord[20].Length > 0) contact.Notes += "Title: " + csvRecord[20] + "\r\n";

            // Company
            if (csvRecord[21].Length > 0) contact.Notes += "Company: " + csvRecord[21] + "\r\n";


            // Work Address
            // Work City
            // Work State
            // Work ZIP
            // Work Country
            if (csvRecord[22].Length > 0 ||
                csvRecord[23].Length > 0 ||
                csvRecord[24].Length > 0 ||
                csvRecord[25].Length > 0 ||
                csvRecord[26].Length > 0)
            {
                contact.Items.Add(new PostalAddress(csvRecord[22], csvRecord[23], csvRecord[24], csvRecord[25], csvRecord[26], "Work Address"));
            }

            // Home Address
            // Home City
            // Home State
            // Home ZIP
            // Home Country
            if (csvRecord[27].Length > 0 ||
                csvRecord[28].Length > 0 ||
                csvRecord[29].Length > 0 ||
                csvRecord[30].Length > 0 ||
                csvRecord[31].Length > 0)
            {
                contact.Items.Add(new PostalAddress(csvRecord[27], csvRecord[28], csvRecord[29], csvRecord[30], csvRecord[31], "Home Address"));
            }


            // Birthday
            if (csvRecord[32].Length > 0) contact.Birthday.FromString(csvRecord[32]);

            // Anniversary
            if (csvRecord[33].Length > 0) contact.Items.Add(Date.Parse(csvRecord[33]));


            // Custom 1
            if (csvRecord[34].Length > 0) contact.Notes += csvRecord[34] + "\r\n";

            // Custom 2
            if (csvRecord[35].Length > 0) contact.Notes += csvRecord[35] + "\r\n";

            // Custom 3
            if (csvRecord[36].Length > 0) contact.Notes += csvRecord[36] + "\r\n";

            // Custom 4
            if (csvRecord[37].Length > 0) contact.Notes += csvRecord[37] + "\r\n";

            // Comments
            if (csvRecord[38].Length > 0) contact.Notes += csvRecord[38];


            // Messenger ID1
            if (csvRecord[39].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[39], "Messenger ID1"));

            // Messenger ID2
            if (csvRecord[40].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[40], "Messenger ID2"));

            // Messenger ID3
            if (csvRecord[41].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[41], "Messenger ID3"));

            // Messenger ID4
            if (csvRecord[42].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[42], "Messenger ID4"));

            // Messenger ID5
            if (csvRecord[43].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[43], "Messenger ID5"));

            // Messenger ID6
            if (csvRecord[44].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[44], "Messenger ID6"));

            // Messenger ID7
            if (csvRecord[45].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[45], "Messenger ID7"));

            // Messenger ID8
            if (csvRecord[46].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[46], "Messenger ID8"));

            // Messenger ID9
            if (csvRecord[47].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[47], "Messenger ID9"));

            // Skype ID
            if (csvRecord[48].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[48], "Skype ID"));

            // IRC ID
            if (csvRecord[49].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[49], "IRC ID"));

            // ICQ ID
            if (csvRecord[50].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[50], "ICQ ID"));

            // Google ID
            if (csvRecord[51].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[51], "Google ID"));

            // MSN ID
            if (csvRecord[52].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[52], "MSN ID"));

            // AIM ID
            if (csvRecord[53].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[53], "AIM ID"));

            // QQ ID
            if (csvRecord[54].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[54], "QQ ID"));

            return contact;
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            Csv csv = new Csv();

            csv.oOptions.bForceQuote = true;
            csv.oOptions.bFirstLineIsHeader = true;
            csv.oOptions.bRemoveEmptyLines = true;
            csv.oOptions.cQuote = '"';
            csv.oOptions.cSep = ',';
            csv.oOptions.eRecSep = RecordSeparator.CRLF;
            csv.oOptions.eTrim = TrimType.Both;

            CsvRecord headerCsvRecord = CreateHeaderCsvRecord();
            csv.Add(headerCsvRecord);

            IEnumerable<CsvRecord> csvRecords = addressBook.Contacts.Select(CreateCsvRecord);
            csv.AddRange(csvRecords);

            csv.WriteToFile(fileName);
        }

        private static CsvRecord CreateHeaderCsvRecord()
        {
            CsvRecord csvRecord = new CsvRecord();

            csvRecord.Add("First");
            csvRecord.Add("Middle");
            csvRecord.Add("Last");
            csvRecord.Add("Nickname");
            csvRecord.Add("Email");
            csvRecord.Add("Category");
            csvRecord.Add("Distribution Lists");
            csvRecord.Add("Messenger ID");
            csvRecord.Add("Home");
            csvRecord.Add("Work");
            csvRecord.Add("Pager");
            csvRecord.Add("Fax");
            csvRecord.Add("Mobile");
            csvRecord.Add("Other");
            csvRecord.Add("Yahoo! Phone");
            csvRecord.Add("Primary");
            csvRecord.Add("Alternate Email 1");
            csvRecord.Add("Alternate Email 2");
            csvRecord.Add("Personal Website");
            csvRecord.Add("Business Website");
            csvRecord.Add("Title");
            csvRecord.Add("Company");
            csvRecord.Add("Work Address");
            csvRecord.Add("Work City");
            csvRecord.Add("Work State");
            csvRecord.Add("Work ZIP");
            csvRecord.Add("Work Country");
            csvRecord.Add("Home Address");
            csvRecord.Add("Home City");
            csvRecord.Add("Home State");
            csvRecord.Add("Home ZIP");
            csvRecord.Add("Home Country");
            csvRecord.Add("Birthday");
            csvRecord.Add("Anniversary");
            csvRecord.Add("Custom 1");
            csvRecord.Add("Custom 2");
            csvRecord.Add("Custom 3");
            csvRecord.Add("Custom 4");
            csvRecord.Add("Comments");
            csvRecord.Add("Messenger ID1");
            csvRecord.Add("Messenger ID2");
            csvRecord.Add("Messenger ID3");
            csvRecord.Add("Messenger ID4");
            csvRecord.Add("Messenger ID5");
            csvRecord.Add("Messenger ID6");
            csvRecord.Add("Messenger ID7");
            csvRecord.Add("Messenger ID8");
            csvRecord.Add("Messenger ID9");
            csvRecord.Add("Skype ID");
            csvRecord.Add("IRC ID");
            csvRecord.Add("ICQ ID");
            csvRecord.Add("Google ID");
            csvRecord.Add("MSN ID");
            csvRecord.Add("AIM ID");
            csvRecord.Add("QQ ID");
            return csvRecord;
        }

        private CsvRecord CreateCsvRecord(Contact contact)
        {
            Phone phone;
            Email email;
            PostalAddress postalAddress;
            Date date;
            SocialProfile socialProfile;

            CsvRecord csvRecord = new CsvRecord();

            // First
            csvRecord.Add(contact.Name.FirstName);

            // Middle
            csvRecord.Add(contact.Name.MiddleName);

            // Last
            csvRecord.Add(contact.Name.LastName);

            // Nickname
            csvRecord.Add(contact.Name.Nickname);


            // Email
            email = contact.Items.SearchByDescription("Email", SearchMode.Exact) as Email;
            csvRecord.Add(email == null ? string.Empty : email.Address);

            // Category
            csvRecord.Add(contact.Category);

            // Distribution Lists
            csvRecord.Add(string.Empty);

            // Messenger ID
            socialProfile = contact.Items.SearchByDescription("Messenger ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Home
            phone = contact.Items.SearchByDescription("Home", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Work
            phone = contact.Items.SearchByDescription("Work", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Pager
            phone = contact.Items.SearchByDescription("Pager", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Fax
            phone = contact.Items.SearchByDescription("Fax", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Mobile
            phone = contact.Items.SearchByDescription("Mobile", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Other
            phone = contact.Items.SearchByDescription("Other", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Yahoo! Phone
            phone = contact.Items.SearchByDescription("Yahoo! Phone", SearchMode.Exact) as Phone;
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Primary
            csvRecord.Add(string.Empty);

            // Alternate Email 1
            email = contact.Items.SearchByDescription("Alternate Email 1", SearchMode.Exact) as Email;
            csvRecord.Add(email == null ? string.Empty : email.Address);

            // Alternate Email 2
            email = contact.Items.SearchByDescription("Alternate Email 2", SearchMode.Exact) as Email;
            csvRecord.Add(email == null ? string.Empty : email.Address);

            // Personal Website
            csvRecord.Add(string.Empty);

            // Business Website
            csvRecord.Add(string.Empty);

            // Title
            csvRecord.Add(string.Empty);

            // Company
            //csvRecord.Add(contact.sCompany);
            csvRecord.Add(string.Empty);

            // Work Address
            postalAddress = contact.Items.SearchByDescription("Work Address", SearchMode.Exact) as PostalAddress;
            if (postalAddress != null)
            {
                csvRecord.Add(postalAddress.Street); // Work Address
                csvRecord.Add(postalAddress.City); // Work City
                csvRecord.Add(postalAddress.State); // Work State
                csvRecord.Add(postalAddress.PostalCode); // Work ZIP
                csvRecord.Add(postalAddress.Country); // Work Country
            }
            else
            {
                csvRecord.Add(string.Empty); // Work Address
                csvRecord.Add(string.Empty); // Work City
                csvRecord.Add(string.Empty); // Work State
                csvRecord.Add(string.Empty); // Work ZIP
                csvRecord.Add(string.Empty); // Work Country
            }

            // Home Address
            postalAddress = contact.Items.SearchByDescription("Home Address", SearchMode.Exact) as PostalAddress;
            if (postalAddress != null)
            {
                csvRecord.Add(postalAddress.Street); // Home Address
                csvRecord.Add(postalAddress.City); // Home City
                csvRecord.Add(postalAddress.State); // Home State
                csvRecord.Add(postalAddress.PostalCode); // Home ZIP
                csvRecord.Add(postalAddress.Country); // Home Country
            }
            else
            {
                csvRecord.Add(string.Empty); // Home Address
                csvRecord.Add(string.Empty); // Home City
                csvRecord.Add(string.Empty); // Home State
                csvRecord.Add(string.Empty); // Home ZIP
                csvRecord.Add(string.Empty); // Home Country
            }

            // Birthday
            if (!contact.Birthday.IsNull())
            {
                date = contact.Birthday;
                string dateAsString = (date.Month > 0 ? date.Month.ToString() : string.Empty) + "/" +
                                      (date.Day > 0 ? date.Day.ToString() : string.Empty) + "/" +
                                      (date.Year > 0 ? date.Year.ToString() : string.Empty);
                csvRecord.Add(dateAsString);
            }
            else
            {
                csvRecord.Add(string.Empty);
            }

            // Anniversary
            csvRecord.Add(string.Empty);

            csvRecord.Add(string.Empty); // Custom 1
            csvRecord.Add(string.Empty); // Custom 2
            csvRecord.Add(string.Empty); // Custom 3
            csvRecord.Add(string.Empty); // Custom 4

            // Comments
            csvRecord.Add(contact.Notes);

            // Messenger ID1
            socialProfile = contact.Items.SearchByDescription("Messenger ID1", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID2
            socialProfile = contact.Items.SearchByDescription("Messenger ID2", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID3
            socialProfile = contact.Items.SearchByDescription("Messenger ID3", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID4
            socialProfile = contact.Items.SearchByDescription("Messenger ID4", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID5
            socialProfile = contact.Items.SearchByDescription("Messenger ID5", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID6
            socialProfile = contact.Items.SearchByDescription("Messenger ID6", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID7
            socialProfile = contact.Items.SearchByDescription("Messenger ID7", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID8
            socialProfile = contact.Items.SearchByDescription("Messenger ID8", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID9
            socialProfile = contact.Items.SearchByDescription("Messenger ID9", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Skype ID
            socialProfile = contact.Items.SearchByDescription("Skype ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // IRC ID
            socialProfile = contact.Items.SearchByDescription("IRC ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // ICQ ID
            socialProfile = contact.Items.SearchByDescription("ICQ ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // Google ID
            socialProfile = contact.Items.SearchByDescription("Google ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // MSN ID
            socialProfile = contact.Items.SearchByDescription("MSN ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // AIM ID
            socialProfile = contact.Items.SearchByDescription("AIM ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            // QQ ID
            socialProfile = contact.Items.SearchByDescription("QQ ID", SearchMode.Exact) as SocialProfile;
            csvRecord.Add(socialProfile == null ? string.Empty : socialProfile.Id);

            return csvRecord;
        }
    }
}