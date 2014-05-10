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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Exceptions;
using ICSharpCode.SharpZipLib.Zip;

namespace DustInTheWind.Lisimba.Egg.Gating
{
    public class ZipXmlGate
    {
        private readonly List<Exception> warnings;

        public IEnumerable<Exception> Warnings
        {
            get { return warnings; }
        }

        public ZipXmlGate()
        {
            warnings = new List<Exception>();
        }

        public AddressBook Load(string fileName)
        {
            warnings.Clear();

            AddressBook book;

            Version eggVersion = Assembly.GetExecutingAssembly().GetName().Version;

            //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //book = (Book)formatter.Deserialize(stream);
            //stream.Close();

            //return book;

            // Create serializer
            XmlSerializer serializer = new XmlSerializer(typeof(AddressBook));

            // Create unzipper.
            using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(fileName)))
            {
                ZipEntry zipEntry;

                // Search for the "file.xml" file
                do
                {
                    zipEntry = zipInputStream.GetNextEntry();

                    if (zipEntry == null)
                        throw new EggException("Incorrect file. The archive does not contains the \"file.xml\" file.");
                }
                while (!zipEntry.Name.Equals("file.xml"));

                // Unzip the "file.xml" file into memory.
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[10240];

                    while (true)
                    {
                        int size = zipInputStream.Read(buffer, 0, buffer.Length);

                        if (size > 0)
                            ms.Write(buffer, 0, size);
                        else
                            break;
                    }

                    ms.Position = 0;

                    // Read lsb version
                    Version lsbVersion = ReadLsbVersion(ms);

                    // Compare versions
                    if (lsbVersion == null)
                    {
                        string warningText = string.Format("The version of the file \"{0}\" could not be determined.", fileName);
                        EggIncorrectVersionException warning = new EggIncorrectVersionException(warningText);
                        warnings.Add(warning);
                    }
                    else if (lsbVersion.ToString(2) != eggVersion.ToString(2))
                    {
                        string warningText = string.Format("The file \"{0}\" is created with another version of the Egg.\n\nCurrent Egg version = {1}\nFile was created by Egg version = {2}", fileName, eggVersion.ToString(2), lsbVersion.ToString(2));
                        EggIncorrectVersionException warning = new EggIncorrectVersionException(warningText);
                        warnings.Add(warning);
                    }

                    ms.Position = 0;

                    // Deserialize the unzipped "file.xml" file
                    using (XmlTextReader xr = new XmlTextReader(ms))
                    {
                        book = ((AddressBook)serializer.Deserialize(xr));
                    }
                }
            }

            book.Version = eggVersion.ToString();
            book.FileName = fileName;

            return book;
        }

        private Version ReadLsbVersion(Stream stream)
        {
            Version ver = null;
            XmlTextReader xmlTextReader = new XmlTextReader(stream);
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
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

        public void Save(AddressBook addressBook, string fileName)
        {
            //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            //formatter.Serialize(stream, this);
            //stream.Close();

            //return true;

            try
            {
                // Create serializer
                XmlSerializer serializer = new XmlSerializer(typeof(AddressBook));

                // Create settings object
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.NewLineChars = "\r\n";
                settings.Encoding = Encoding.UTF8;
                settings.ConformanceLevel = ConformanceLevel.Document;
                settings.NewLineHandling = NewLineHandling.None;

                // Create memory stream to write the xml file to.
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create writer
                    using (XmlWriter xw = XmlWriter.Create(ms, settings))
                    {
                        // Serialize to xml
                        serializer.Serialize(xw, addressBook);

                        ms.Position = 0;

                        // Zip the xml file

                        using (ZipOutputStream zs = new ZipOutputStream(File.OpenWrite(fileName)))
                        {
                            zs.SetLevel(9);

                            ZipEntry zentry = new ZipEntry("file.xml");
                            zentry.CompressionMethod = CompressionMethod.Deflated;

                            zs.PutNextEntry(zentry);

                            int size;
                            byte[] buffer = new byte[10240];
                            do
                            {
                                size = ms.Read(buffer, 0, buffer.Length);
                                zs.Write(buffer, 0, size);
                            }
                            while (size > 0);

                            zs.Finish();
                        }
                    }
                }

                addressBook.FileName = fileName;
            }
            catch (Exception ex)
            {
                throw new EggException("Error saving. [" + ex.Message + "]", ex);
            }
        }
    }
}
