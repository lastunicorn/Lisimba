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
using DustInTheWind.Lisimba.Egg.Gating;
using System;

namespace DustInTheWind.Lisimba.Egg
{
    public class AddressBookManager
    {
        #region Event IncorrectXmlVersion

        public event EventHandler<IncorrectXmlVersionEventArgs> IncorrectXmlVersion;

        protected virtual void OnIncorrectXmlVersion(IncorrectXmlVersionEventArgs e)
        {
            if (IncorrectXmlVersion == null)
                return;

            IncorrectXmlVersion(null, e);
        }

        #endregion

        public ImportRuleCollection CreateImportRules(ContactCollection newContacts)
        {
            ImportRuleCollection rules = new ImportRuleCollection();

            for (int i = 0; i < newContacts.Count; i++)
            {
                rules.Add(new ImportRule(newContacts[i]));
            }

            return rules;
        }

        #region Load

        public AddressBook LoadFromFile(string fileName)
        {
            EggXmlGate gate = new EggXmlGate();
            gate.IncorrectVersion += HandleGateOnIncorrectVersion;

            return gate.Load(fileName);
        }

        private void HandleGateOnIncorrectVersion(object sender, IncorrectXmlVersionEventArgs e)
        {
            OnIncorrectXmlVersion(e);
        }

        public ContactCollection ImportFromFile(string fileName, FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.Egg:
                    AddressBook book = LoadFromFile(fileName);
                    return book.Contacts;

                case FileFormat.CsvYahoo:
                    YahooCsvGate yahooCsvGate = new YahooCsvGate();
                    AddressBook addressBook = yahooCsvGate.Load(fileName);
                    return addressBook.Contacts;

                default:
                    return new ContactCollection();
            }
        }

        #endregion

        #region Save

        public void SaveToFile(AddressBook addressBook)
        {
            bool hasFileName = addressBook.FileName.Length > 0;

            if (hasFileName)
                SaveToFile(addressBook, addressBook.FileName);
        }

        //public bool SaveToFile(string fileName)
        //{
        //    bool returnValue = false;

        //    //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        //    //Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        //    //formatter.Serialize(stream, this);
        //    //stream.Close();

        //    //return true;

        //    XmlSerializer serializer = null;
        //    XmlWriter xw = null;
        //    XmlWriterSettings settings = null;

        //    try
        //    {
        //        // Create serializer
        //        serializer = new XmlSerializer(typeof(AddressBook));

        //        // Create settings object
        //        settings = new XmlWriterSettings();
        //        settings.Indent = true;
        //        settings.IndentChars = "  ";
        //        settings.NewLineChars = "\r\n";
        //        settings.Encoding = Encoding.UTF8;
        //        settings.ConformanceLevel = ConformanceLevel.Document;
        //        settings.NewLineHandling = NewLineHandling.None;

        //        // Create writer
        //        xw = XmlWriter.Create(fileName, settings);

        //        // Serialize
        //        serializer.Serialize(xw, this);

        //        this.fileName = fileName;
        //        returnValue = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new EggException("Error saving. [" + ex.Message + "]", ex);
        //    }
        //    finally
        //    {
        //        if (xw != null)
        //        {
        //            xw.Close();
        //        }
        //    }

        //    return returnValue;
        //}

        public void SaveToFile(AddressBook addressBook, string fileName)
        {
            EggXmlGate gate = new EggXmlGate();
            gate.Save(addressBook, fileName);
        }

        public void ExportToFile(AddressBook addressBook, string fileName, FileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormat.CsvYahoo:
                    YahooCsvGate gate = new YahooCsvGate();
                    gate.Save(addressBook, fileName);
                    break;
            }
        }

        #endregion
    }
}
