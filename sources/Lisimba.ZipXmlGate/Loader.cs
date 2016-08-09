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
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.GateModel;
using DustInTheWind.Lisimba.ZipXmlGate.Entities;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.ZipXmlGate
{
    internal class Loader
    {
        private readonly List<Exception> warnings;
        private readonly Dictionary<string, byte[]> pictures;
        private AddressBookEntity addressBookEntity;
        private readonly Version eggVersion;

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        public Loader()
        {
            warnings = new List<Exception>();
            pictures = new Dictionary<string, byte[]>();

            eggVersion = Assembly.GetExecutingAssembly().GetName().Version;
        }

        public AddressBook Load(Stream stream)
        {
            warnings.Clear();
            pictures.Clear();
            addressBookEntity = null;

            // Create unzipper.
            using (ZipInputStream zipStream = new ZipInputStream(stream))
            {
                while (true)
                {
                    ZipEntry zipEntry = zipStream.GetNextEntry();

                    if (zipEntry == null)
                        break;

                    if (zipEntry.Name.Equals(Configuration.MainFileName))
                        DeserializeMainFile(zipStream);
                    else if (zipEntry.Name.StartsWith(Configuration.DataDirectoryName + "/"))
                        DeserializePictureFile(zipStream, zipEntry);
                }

                if (addressBookEntity == null)
                    throw new LisimbaException("Incorrect file. The archive does not contain the \"file.xml\" file.");

                AttachPicturesToContacts();
            }

            AddressBook book = EntityConverter.FromEntity(addressBookEntity);
            book.Version = eggVersion.ToString();

            return book;
        }

        private void AttachPicturesToContacts()
        {
            foreach (ContactEntity contactEntity in addressBookEntity.Contacts)
            {
                if (contactEntity.PictureHash != null)
                {
                    if (pictures.ContainsKey(contactEntity.PictureHash))
                        contactEntity.Picture = pictures[contactEntity.PictureHash];
                    else
                        warnings.Add(new LisimbaException("Picture not found. Picture hash: " + contactEntity.PictureHash));
                }
            }
        }

        private void DeserializePictureFile(Stream zipStream, ZipEntry zipEntry)
        {
            string pictureHash = Path.GetFileName(zipEntry.Name);

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = new byte[10240];

                while (true)
                {
                    int size = zipStream.Read(buffer, 0, buffer.Length);

                    if (size > 0)
                        ms.Write(buffer, 0, size);
                    else
                        break;
                }

                pictures.Add(pictureHash, ms.ToArray());
            }
        }

        private void DeserializeMainFile(Stream zipStream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ReadAllStream(zipStream, ms);

                ms.Position = 0;
                ValidateVersion(ms);

                ms.Position = 0;
                XmlTextReader xmlTextReader = new XmlTextReader(ms);
                XmlSerializer serializer = new XmlSerializer(typeof(AddressBookEntity));
                addressBookEntity = (AddressBookEntity)serializer.Deserialize(xmlTextReader);
            }
        }

        private static void ReadAllStream(Stream source, Stream destination)
        {
            byte[] buffer = new byte[10240];

            while (true)
            {
                int size = source.Read(buffer, 0, buffer.Length);

                if (size > 0)
                    destination.Write(buffer, 0, size);
                else
                    break;
            }
        }

        private void ValidateVersion(Stream ms)
        {
            // Read lsb version
            Version lsbVersion = ReadLsbVersion(ms);

            if (lsbVersion == null)
            {
                IncorrectEggVersionException warning = new IncorrectEggVersionException("The version of the file could not be determined.");
                warnings.Add(warning);
            }
            else if (lsbVersion.ToString(2) != eggVersion.ToString(2))
            {
                string warningText = string.Format("The file is created with another version of the Egg.\n\nCurrent Egg version = {0}\nFile was created by Egg version = {1}",
                        eggVersion.ToString(2),
                        lsbVersion.ToString(2));

                IncorrectEggVersionException warning = new IncorrectEggVersionException(warningText);
                warnings.Add(warning);
            }
        }

        private static Version ReadLsbVersion(Stream stream)
        {
            Version ver = null;
            XmlTextReader xmlTextReader = new XmlTextReader(stream)
            {
                WhitespaceHandling = WhitespaceHandling.None
            };
            xmlTextReader.MoveToContent();

            while (xmlTextReader.Read())
            {
                if (!xmlTextReader.Name.Equals("Version") || xmlTextReader.NodeType != XmlNodeType.Element)
                    continue;

                if (xmlTextReader.IsEmptyElement)
                    break;

                if (xmlTextReader.Read() && xmlTextReader.NodeType == XmlNodeType.Text)
                    ver = new Version(xmlTextReader.Value);

                break;
            }

            return ver;
        }
    }
}