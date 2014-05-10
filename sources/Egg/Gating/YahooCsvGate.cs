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

using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using zcsv;

namespace DustInTheWind.Lisimba.Egg.Gating
{
    class YahooCsvGate
    {
        public AddressBook Load(string fileName)
        {
            AddressBook addressBook = new AddressBook
            {
                Version = string.Empty,
                Name = string.Empty,
                FileName = fileName
            };

            Csv csv = new Csv();
            csv.oOptions.bFirstLineIsHeader = true;
            csv.LoadFromFile(fileName);
            for (int i = 0; i < csv.Count - 1; i++)
            {
                Contact contact = new Contact();

                // First
                contact.Name.FirstName = csv[i][0];

                // Middle
                contact.Name.MiddleName = csv[i][1];

                // Last
                contact.Name.LastName = csv[i][2];

                // Nickname
                contact.Name.Nickname = csv[i][3];


                // Email
                if (csv[i][4].Length > 0) contact.Emails.Add(new Email(csv[i][4], "Email"));


                // Category
                // Distribution Lists


                // Messenger ID
                if (csv[i][7].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][7], "Yahoo!"));


                // Home
                if (csv[i][8].Length > 0) contact.Phones.Add(new Phone(csv[i][8], "Home"));

                // Work
                if (csv[i][9].Length > 0) contact.Phones.Add(new Phone(csv[i][9], "Work"));

                // Pager
                if (csv[i][10].Length > 0) contact.Phones.Add(new Phone(csv[i][10], "Pager"));

                // Fax
                if (csv[i][11].Length > 0) contact.Phones.Add(new Phone(csv[i][11], "Fax"));

                // Mobile
                if (csv[i][12].Length > 0) contact.Phones.Add(new Phone(csv[i][12], "Mobile"));

                // Other
                if (csv[i][13].Length > 0) contact.Phones.Add(new Phone(csv[i][13], "Other"));

                // Yahoo! Phone
                if (csv[i][14].Length > 0) contact.Phones.Add(new Phone(csv[i][14], "Yahoo! Phone"));


                // Primary


                // Alternate Email 1
                if (csv[i][16].Length > 0) contact.Emails.Add(new Email(csv[i][16], "Alternate Email 1"));

                // Alternate Email 2
                if (csv[i][17].Length > 0) contact.Emails.Add(new Email(csv[i][17], "Alternate Email 2"));


                // Personal Website
                if (csv[i][18].Length > 0) contact.WebSites.Add(new WebSite(csv[i][18], "Personal Website"));

                // Business Website
                if (csv[i][19].Length > 0) contact.WebSites.Add(new WebSite(csv[i][19], "Business Website"));


                // Title
                if (csv[i][20].Length > 0) contact.Notes += "Title: " + csv[i][20] + "\r\n";

                // Company
                if (csv[i][21].Length > 0) contact.Notes += "Company: " + csv[i][21] + "\r\n";


                // Work Address
                // Work City
                // Work State
                // Work ZIP
                // Work Country
                if (csv[i][22].Length > 0 ||
                    csv[i][23].Length > 0 ||
                    csv[i][24].Length > 0 ||
                    csv[i][25].Length > 0 ||
                    csv[i][26].Length > 0)
                {
                    contact.Addresses.Add(new Address(csv[i][22], csv[i][23], csv[i][24], csv[i][25], csv[i][26], "Work Address"));
                }

                // Home Address
                // Home City
                // Home State
                // Home ZIP
                // Home Country
                if (csv[i][27].Length > 0 ||
                    csv[i][28].Length > 0 ||
                    csv[i][29].Length > 0 ||
                    csv[i][30].Length > 0 ||
                    csv[i][31].Length > 0)
                {
                    contact.Addresses.Add(new Address(csv[i][27], csv[i][28], csv[i][29], csv[i][30], csv[i][31], "Home Address"));
                }


                // Birthday
                if (csv[i][32].Length > 0) contact.Birthday.FromString(csv[i][32]);

                // Anniversary
                if (csv[i][33].Length > 0) contact.Dates.Add(Date.Parse(csv[i][33]));


                // Custom 1
                if (csv[i][34].Length > 0) contact.Notes += csv[i][34] + "\r\n";

                // Custom 2
                if (csv[i][35].Length > 0) contact.Notes += csv[i][35] + "\r\n";

                // Custom 3
                if (csv[i][36].Length > 0) contact.Notes += csv[i][36] + "\r\n";

                // Custom 4
                if (csv[i][37].Length > 0) contact.Notes += csv[i][37] + "\r\n";

                // Comments
                if (csv[i][38].Length > 0) contact.Notes += csv[i][38];


                // Messenger ID1
                if (csv[i][39].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][39], "Messenger ID1"));

                // Messenger ID2
                if (csv[i][40].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][40], "Messenger ID2"));

                // Messenger ID3
                if (csv[i][41].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][41], "Messenger ID3"));

                // Messenger ID4
                if (csv[i][42].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][42], "Messenger ID4"));

                // Messenger ID5
                if (csv[i][43].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][43], "Messenger ID5"));

                // Messenger ID6
                if (csv[i][44].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][44], "Messenger ID6"));

                // Messenger ID7
                if (csv[i][45].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][45], "Messenger ID7"));

                // Messenger ID8
                if (csv[i][46].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][46], "Messenger ID8"));

                // Messenger ID9
                if (csv[i][47].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][47], "Messenger ID9"));

                // Skype ID
                if (csv[i][48].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][48], "Skype ID"));

                // IRC ID
                if (csv[i][49].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][49], "IRC ID"));

                // ICQ ID
                if (csv[i][50].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][50], "ICQ ID"));

                // Google ID
                if (csv[i][51].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][51], "Google ID"));

                // MSN ID
                if (csv[i][52].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][52], "MSN ID"));

                // AIM ID
                if (csv[i][53].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][53], "AIM ID"));

                // QQ ID
                if (csv[i][54].Length > 0) contact.MessengerIds.Add(new MessengerId(csv[i][54], "QQ ID"));


                // Add the new contact to the collection.
                addressBook.Add(contact);
            }

            return addressBook;
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            Csv csv = new Csv();
            CsvRecord csvRecord;
            Contact contact;
            Phone phone;
            Email email;
            Address address;
            Date date;
            MessengerId messengerId;
            //string[] header = new string[]
            //{
            //    "First",
            //    "Middle",
            //    "Last",
            //    "Nickname",
            //    "Email",
            //    "Category",
            //    "Distribution Lists",
            //    "Messenger ID",
            //    "Home",
            //    "Work",
            //    "Pager",
            //    "Fax",
            //    "Mobile",
            //    "Other",
            //    "Yahoo! Phone",
            //    "Primary",
            //    "Alternate Email 1",
            //    "Alternate Email 2",
            //    "Personal Website",
            //    "Business Website",
            //    "Title",
            //    "Company",
            //    "Work Address",
            //    "Work City",
            //    "Work State",
            //    "Work ZIP",
            //    "Work Country",
            //    "Home Address",
            //    "Home City",
            //    "Home State",
            //    "Home ZIP",
            //    "Home Country",
            //    "Birthday",
            //    "Anniversary",
            //    "Custom 1",
            //    "Custom 2",
            //    "Custom 3",
            //    "Custom 4",
            //    "Comments",
            //    "Messenger ID1",
            //    "Messenger ID2",
            //    "Messenger ID3",
            //    "Messenger ID4",
            //    "Messenger ID5",
            //    "Messenger ID6",
            //    "Messenger ID7",
            //    "Messenger ID8",
            //    "Messenger ID9",
            //    "Skype ID",
            //    "IRC ID",
            //    "ICQ ID",
            //    "Google ID",
            //    "MSN ID",
            //    "AIM ID",
            //    "QQ ID"
            //};

            csv.oOptions.bForceQuote = true;
            csv.oOptions.bFirstLineIsHeader = true;
            csv.oOptions.bRemoveEmptyLines = true;
            csv.oOptions.cQuote = '"';
            csv.oOptions.cSep = ',';
            csv.oOptions.eRecSep = RecordSeparator.CRLF;
            csv.oOptions.eTrim = TrimType.Both;

            csvRecord = new CsvRecord();

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

            csv.Add(csvRecord);

            for (int i = 0; i < addressBook.Count; i++)
            {
                csvRecord = new CsvRecord();

                contact = addressBook[i];


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
                if (email != null)
                    csvRecord.Add(email.Address);
                else
                    csvRecord.Add("");

                // Category
                csvRecord.Add(contact.Category);

                // Distribution Lists
                csvRecord.Add("");

                // Messenger ID
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Home
                phone = contact.Phones.SearchByDescription("Home", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Work
                phone = contact.Phones.SearchByDescription("Work", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Pager
                phone = contact.Phones.SearchByDescription("Pager", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Fax
                phone = contact.Phones.SearchByDescription("Fax", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Mobile
                phone = contact.Phones.SearchByDescription("Mobile", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Other
                phone = contact.Phones.SearchByDescription("Other", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                // Yahoo! Phone
                phone = contact.Phones.SearchByDescription("Yahoo! Phone", SearchMode.Exact);
                if (phone != null)
                    csvRecord.Add(phone.Number);
                else
                    csvRecord.Add("");

                csvRecord.Add(""); // Primary

                // Alternate Email 1
                email = contact.Emails.SearchByDescription("Alternate Email 1", SearchMode.Exact);
                if (email != null)
                    csvRecord.Add(email.Address);
                else
                    csvRecord.Add("");

                // Alternate Email 2
                email = contact.Emails.SearchByDescription("Alternate Email 2", SearchMode.Exact);
                if (email != null)
                    csvRecord.Add(email.Address);
                else
                    csvRecord.Add("");

                csvRecord.Add(""); // Personal Website
                csvRecord.Add(""); // Business Website
                csvRecord.Add(""); // Title

                // Company
                //csvRecord.Add(contact.sCompany);
                csvRecord.Add("");

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
                    csvRecord.Add(""); // Work Address
                    csvRecord.Add(""); // Work City
                    csvRecord.Add(""); // Work State
                    csvRecord.Add(""); // Work ZIP
                    csvRecord.Add(""); // Work Country
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
                    csvRecord.Add(""); // Home Address
                    csvRecord.Add(""); // Home City
                    csvRecord.Add(""); // Home State
                    csvRecord.Add(""); // Home ZIP
                    csvRecord.Add(""); // Home Country
                }

                // Birthday
                if (!contact.Birthday.IsNull())
                {
                    date = contact.Birthday;
                    csvRecord.Add(
                        (date.Month > 0 ? date.Month.ToString() : "") + "/" +
                        (date.Day > 0 ? date.Day.ToString() : "") + "/" +
                        (date.Year > 0 ? date.Year.ToString() : ""));
                }
                else
                {
                    csvRecord.Add("");
                }

                // Anniversary
                csvRecord.Add("");

                csvRecord.Add(""); // Custom 1
                csvRecord.Add(""); // Custom 2
                csvRecord.Add(""); // Custom 3
                csvRecord.Add(""); // Custom 4

                // Comments
                csvRecord.Add(contact.Notes);

                // Messenger ID1
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID1", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID2
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID2", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID3
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID3", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID4
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID4", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID5
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID5", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID6
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID6", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID7
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID7", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID8
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID8", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Messenger ID9
                messengerId = contact.MessengerIds.SearchByDescription("Messenger ID9", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Skype ID
                messengerId = contact.MessengerIds.SearchByDescription("Skype ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // IRC ID
                messengerId = contact.MessengerIds.SearchByDescription("IRC ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // ICQ ID
                messengerId = contact.MessengerIds.SearchByDescription("ICQ ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // Google ID
                messengerId = contact.MessengerIds.SearchByDescription("Google ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // MSN ID
                messengerId = contact.MessengerIds.SearchByDescription("MSN ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // AIM ID
                messengerId = contact.MessengerIds.SearchByDescription("AIM ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");

                // QQ ID
                messengerId = contact.MessengerIds.SearchByDescription("QQ ID", SearchMode.Exact);
                if (messengerId != null)
                    csvRecord.Add(messengerId.Id);
                else
                    csvRecord.Add("");


                csv.Add(csvRecord);
            }

            csv.WriteToFile(fileName);
        }
    }
}
