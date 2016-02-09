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
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.ZipXmlGate.Entities;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.ZipXmlGate
{
    internal class Saver
    {
        public void Save(AddressBook addressBook, FileStream fileStream)
        {
            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(fileStream))
                {
                    zipStream.SetLevel(9);

                    ZipEntry zipEntry = new ZipEntry("file.xml")
                    {
                        CompressionMethod = CompressionMethod.Deflated
                    };

                    zipStream.PutNextEntry(zipEntry);

                    SerializeAddressBook(addressBook, zipStream);

                    zipStream.Finish();
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error saving address book. Inner error: {0}", ex.Message);
                throw new EggException(message, ex);
            }
        }

        private static void SerializeAddressBook(AddressBook addressBook, Stream outputStream)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Document,
                NewLineHandling = NewLineHandling.None
            };

            XmlWriter xmlWriter = XmlWriter.Create(outputStream, settings);

            XmlSerializer serializer = new XmlSerializer(typeof(AddressBookEntity));

            AddressBookEntity addressBookEntity = EntityConverter.ToEntity(addressBook);
            serializer.Serialize(xmlWriter, addressBookEntity);
        }
    }
}