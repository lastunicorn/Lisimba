﻿// Lisimba
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
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.LisimbaGate.Entities;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.LisimbaGate
{
    internal class Saver
    {
        public void Save(AddressBook addressBook, Stream stream)
        {
            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(stream))
                {
                    zipStream.SetLevel(9);

                    AddressBookEntity addressBookEntity = EntityConverter.ToEntity(addressBook);

                    WriteMainFile(zipStream, addressBookEntity);
                    WritePictures(zipStream, addressBookEntity);

                    zipStream.Finish();
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error saving address book. Inner error: {0}", ex.Message);
                throw new LisimbaException(message, ex);
            }
        }

        private static void WriteMainFile(ZipOutputStream zipStream, AddressBookEntity addressBookEntity)
        {
            ZipEntry zipEntry = new ZipEntry(Configuration.MainFileName)
            {
                CompressionMethod = CompressionMethod.Deflated
            };

            zipStream.PutNextEntry(zipEntry);

            SerializeAddressBook(addressBookEntity, zipStream);
        }

        private static void WritePictures(ZipOutputStream zipStream, AddressBookEntity addressBookEntity)
        {
            Dictionary<string, byte[]> pictures = new Dictionary<string, byte[]>();

            foreach (ContactEntity contact in addressBookEntity.Contacts)
            {
                if (contact.Picture == null)
                    continue;

                if (!pictures.ContainsKey(contact.PictureHash))
                    pictures.Add(contact.PictureHash, contact.Picture);
            }

            foreach (KeyValuePair<string, byte[]> keyValuePair in pictures)
            {
                ZipEntry zipEntry = new ZipEntry(Configuration.DataDirectoryName + "/" + keyValuePair.Key)
                {
                    CompressionMethod = CompressionMethod.Deflated
                };

                zipStream.PutNextEntry(zipEntry);

                zipStream.Write(keyValuePair.Value, 0, keyValuePair.Value.Length);
            }
        }

        private static void SerializeAddressBook(AddressBookEntity addressBookEntity, Stream outputStream)
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

            serializer.Serialize(xmlWriter, addressBookEntity);
        }
    }
}