using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Exceptions;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg.Gating
{
    class EggXmlGate
    {
        #region Event IncorrectXmlVersion

        public event EventHandler<IncorrectXmlVersionEventArgs> IncorrectVersion;

        protected virtual void OnIncorrectVersion(IncorrectXmlVersionEventArgs e)
        {
            if (IncorrectVersion == null)
                return;

            IncorrectVersion(null, e);
        }

        #endregion

        public AddressBook Load(string fileName)
        {
            AddressBook book = null;

            Version eggVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Version lsbVersion = null;

            //System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            //book = (Book)formatter.Deserialize(stream);
            //stream.Close();

            //return book;


            // Create serializer
            XmlSerializer serializer = new XmlSerializer(typeof(AddressBook));

            // Create unzipper.
            using (ZipInputStream zs = new ZipInputStream(File.OpenRead(fileName)))
            {
                ZipEntry zipEntry;

                // Search for the "file.xml" file
                do
                {
                    zipEntry = zs.GetNextEntry();
                    if (zipEntry == null)
                        throw new EggException("Incorrect file. The archive does not contains the \"file.xml\" file.");
                }
                while (!zipEntry.Name.Equals("file.xml"));

                // Unzip the "file.xml" file into memory.
                using (MemoryStream ms = new MemoryStream())
                {
                    int size = 0;
                    byte[] buffer = new byte[10240];
                    while (true)
                    {
                        size = zs.Read(buffer, 0, buffer.Length);
                        if (size > 0)
                        {
                            ms.Write(buffer, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }


                    ms.Position = 0;

                    // Read lsb version
                    lsbVersion = ReadLsbVersion(ms);

                    // Compare versions
                    if (lsbVersion == null)
                    {
                        IncorrectXmlVersionEventArgs e = new IncorrectXmlVersionEventArgs(eggVersion, lsbVersion, fileName);
                        OnIncorrectVersion(e);

                        if (!e.ContinueParsing)
                            throw new EggIncorrectVersionException();
                    }
                    else
                    {
                        int verCmp = 0;
                        verCmp = eggVersion.Major.CompareTo(lsbVersion.Major);
                        if (verCmp != 0)
                        {
                            IncorrectXmlVersionEventArgs e = new IncorrectXmlVersionEventArgs(eggVersion, lsbVersion, fileName);
                            OnIncorrectVersion(e);

                            if (!e.ContinueParsing)
                                throw new EggIncorrectVersionException();
                        }
                        else
                        {
                            verCmp = eggVersion.Minor.CompareTo(lsbVersion.Minor);
                            if (verCmp != 0)
                            {
                                IncorrectXmlVersionEventArgs e = new IncorrectXmlVersionEventArgs(eggVersion, lsbVersion, fileName);
                                OnIncorrectVersion(e);

                                if (!e.ContinueParsing)
                                    throw new EggIncorrectVersionException();
                            }
                        }
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
