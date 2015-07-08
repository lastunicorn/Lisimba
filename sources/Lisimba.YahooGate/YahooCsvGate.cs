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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using zcsv;

namespace DustInTheWind.Lisimba.Gating
{
    public class YahooCsvGate : IGate
    {
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
            if (csvRecord[4].Length > 0) contact.Emails.Add(new Email(csvRecord[4], "Email"));


            // Category
            // Distribution Lists


            // Messenger ID
            if (csvRecord[7].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[7], "Yahoo!"));


            // Home
            if (csvRecord[8].Length > 0) contact.Phones.Add(new Phone(csvRecord[8], "Home"));

            // Work
            if (csvRecord[9].Length > 0) contact.Phones.Add(new Phone(csvRecord[9], "Work"));

            // Pager
            if (csvRecord[10].Length > 0) contact.Phones.Add(new Phone(csvRecord[10], "Pager"));

            // Fax
            if (csvRecord[11].Length > 0) contact.Phones.Add(new Phone(csvRecord[11], "Fax"));

            // Mobile
            if (csvRecord[12].Length > 0) contact.Phones.Add(new Phone(csvRecord[12], "Mobile"));

            // Other
            if (csvRecord[13].Length > 0) contact.Phones.Add(new Phone(csvRecord[13], "Other"));

            // Yahoo! Phone
            if (csvRecord[14].Length > 0) contact.Phones.Add(new Phone(csvRecord[14], "Yahoo! Phone"));


            // Primary


            // Alternate Email 1
            if (csvRecord[16].Length > 0) contact.Emails.Add(new Email(csvRecord[16], "Alternate Email 1"));

            // Alternate Email 2
            if (csvRecord[17].Length > 0) contact.Emails.Add(new Email(csvRecord[17], "Alternate Email 2"));


            // Personal Website
            if (csvRecord[18].Length > 0) contact.WebSites.Add(new WebSite(csvRecord[18], "Personal Website"));

            // Business Website
            if (csvRecord[19].Length > 0) contact.WebSites.Add(new WebSite(csvRecord[19], "Business Website"));


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
                contact.Addresses.Add(new Address(csvRecord[22], csvRecord[23], csvRecord[24], csvRecord[25], csvRecord[26], "Work Address"));
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
                contact.Addresses.Add(new Address(csvRecord[27], csvRecord[28], csvRecord[29], csvRecord[30], csvRecord[31], "Home Address"));
            }


            // Birthday
            if (csvRecord[32].Length > 0) contact.Birthday.FromString(csvRecord[32]);

            // Anniversary
            if (csvRecord[33].Length > 0) contact.Dates.Add(Date.Parse(csvRecord[33]));


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
            if (csvRecord[39].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[39], "Messenger ID1"));

            // Messenger ID2
            if (csvRecord[40].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[40], "Messenger ID2"));

            // Messenger ID3
            if (csvRecord[41].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[41], "Messenger ID3"));

            // Messenger ID4
            if (csvRecord[42].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[42], "Messenger ID4"));

            // Messenger ID5
            if (csvRecord[43].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[43], "Messenger ID5"));

            // Messenger ID6
            if (csvRecord[44].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[44], "Messenger ID6"));

            // Messenger ID7
            if (csvRecord[45].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[45], "Messenger ID7"));

            // Messenger ID8
            if (csvRecord[46].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[46], "Messenger ID8"));

            // Messenger ID9
            if (csvRecord[47].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[47], "Messenger ID9"));

            // Skype ID
            if (csvRecord[48].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[48], "Skype ID"));

            // IRC ID
            if (csvRecord[49].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[49], "IRC ID"));

            // ICQ ID
            if (csvRecord[50].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[50], "ICQ ID"));

            // Google ID
            if (csvRecord[51].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[51], "Google ID"));

            // MSN ID
            if (csvRecord[52].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[52], "MSN ID"));

            // AIM ID
            if (csvRecord[53].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[53], "AIM ID"));

            // QQ ID
            if (csvRecord[54].Length > 0) contact.MessengerIds.Add(new MessengerId(csvRecord[54], "QQ ID"));

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
            Address address;
            Date date;
            MessengerId messengerId;

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
            email = contact.Emails.SearchByDescription("Email", SearchMode.Exact);
            csvRecord.Add(email == null ? string.Empty : email.Address);

            // Category
            csvRecord.Add(contact.Category);

            // Distribution Lists
            csvRecord.Add(string.Empty);

            // Messenger ID
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Home
            phone = contact.Phones.SearchByDescription("Home", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Work
            phone = contact.Phones.SearchByDescription("Work", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Pager
            phone = contact.Phones.SearchByDescription("Pager", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Fax
            phone = contact.Phones.SearchByDescription("Fax", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Mobile
            phone = contact.Phones.SearchByDescription("Mobile", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Other
            phone = contact.Phones.SearchByDescription("Other", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Yahoo! Phone
            phone = contact.Phones.SearchByDescription("Yahoo! Phone", SearchMode.Exact);
            csvRecord.Add(phone == null ? string.Empty : phone.Number);

            // Primary
            csvRecord.Add(string.Empty);

            // Alternate Email 1
            email = contact.Emails.SearchByDescription("Alternate Email 1", SearchMode.Exact);
            csvRecord.Add(email == null ? string.Empty : email.Address);

            // Alternate Email 2
            email = contact.Emails.SearchByDescription("Alternate Email 2", SearchMode.Exact);
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
            address = contact.Addresses.SearchByDescription("Work Address", SearchMode.Exact);
            if (address != null)
            {
                csvRecord.Add(address.Street); // Work Address
                csvRecord.Add(address.City); // Work City
                csvRecord.Add(address.State); // Work State
                csvRecord.Add(address.PostalCode); // Work ZIP
                csvRecord.Add(address.Country); // Work Country
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
            address = contact.Addresses.SearchByDescription("Home Address", SearchMode.Exact);
            if (address != null)
            {
                csvRecord.Add(address.Street); // Home Address
                csvRecord.Add(address.City); // Home City
                csvRecord.Add(address.State); // Home State
                csvRecord.Add(address.PostalCode); // Home ZIP
                csvRecord.Add(address.Country); // Home Country
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
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID1", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID2
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID2", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID3
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID3", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID4
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID4", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID5
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID5", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID6
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID6", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID7
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID7", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID8
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID8", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Messenger ID9
            messengerId = contact.MessengerIds.SearchByDescription("Messenger ID9", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Skype ID
            messengerId = contact.MessengerIds.SearchByDescription("Skype ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // IRC ID
            messengerId = contact.MessengerIds.SearchByDescription("IRC ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // ICQ ID
            messengerId = contact.MessengerIds.SearchByDescription("ICQ ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // Google ID
            messengerId = contact.MessengerIds.SearchByDescription("Google ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // MSN ID
            messengerId = contact.MessengerIds.SearchByDescription("MSN ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // AIM ID
            messengerId = contact.MessengerIds.SearchByDescription("AIM ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            // QQ ID
            messengerId = contact.MessengerIds.SearchByDescription("QQ ID", SearchMode.Exact);
            csvRecord.Add(messengerId == null ? string.Empty : messengerId.Id);

            return csvRecord;
        }
    }
}
