using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace DustInTheWind.Lisimba.Utils
{
    /// <summary>
    /// Class used to write log information to a file.
    /// </summary>
    public class Log : IDisposable
    {
        private bool enabled = false;

        public static Log Instance = new Log(Log.DefaultFileName, true);

        private StreamWriter sw;


        //System.Threading.ManualResetEvent semaphore = new System.Threading.ManualResetEvent(true);
        //private object lockObject = new object();

        //private bool logLock = false;
        //public bool LogLock
        //{
        //    get { return logLock; }
        //    set
        //    {
        //        lock (lockObject)
        //        {
        //            logLock = value;
        //            if (value)
        //            {
        //                semaphore.Reset();
        //            }
        //        }
        //    }
        //}


        #region FileName
        private string fileName = string.Empty;
        /// <summary>
        /// The name of the file into witch will be generated the logs.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }
        #endregion

        #region NewLine
        private string newLine = "\r\n";
        /// <summary>
        /// The string containing the new line characters. Default value is "\r\n";
        /// </summary>
        public string NewLine
        {
            get { return newLine; }
            set { newLine = value; }
        }
        #endregion

        #region WriteTimeStamp
        private bool writeTimeStamp = true;
        /// <summary>
        /// If true, a timestamp is written at the beginning of every line.
        /// </summary>
        public bool WriteTimeStamp
        {
            get { return writeTimeStamp; }
            set { writeTimeStamp = value; }
        }
        #endregion

        #region TimeStampFormat
        private string timeStampFormat = "[yyyy/MM/dd HH:mm:ss]";
        /// <summary>
        /// The format of the timestamp written at the beginning of every line.
        /// </summary>
        public string TimeStampFormat
        {
            get { return timeStampFormat; }
            set { timeStampFormat = value; }
        }
        #endregion

        #region IndentString
        private string indentString = "    ";
        /// <summary>
        /// The string used to indent the lines. (where indent is needed)
        /// </summary>
        public string IndentString
        {
            get { return indentString; }
            set { indentString = value; }
        }
        #endregion

        #region LastException
        private Exception lastException = null;
        /// <summary>
        /// No function of this class raises any exception. So, the last internal exception is exposed here.
        /// </summary>
        public Exception LastException
        {
            get { return lastException; }
        }
        #endregion

        #region DefaultLocation
        private static string defaultLocation;
        /// <summary>
        /// The location where the log file will be created if the user does not explicitly specifies the file.
        /// </summary>
        public static string DefaultLocation
        {
            get { return defaultLocation; }
        }
        #endregion

        #region DefaultFileName
        /// <summary>
        /// The name of the log file if the user does not explicitly specifies it.
        /// </summary>
        public static string DefaultFileName
        {
            get { return DefaultLocation + "\\logfile.log"; }
        }
        #endregion

        private enum ErrorLevel
        {
            Message,
            Warning,
            Error
        }

        #region Constructors

        static Log()
        {
            // Set the default location

            Assembly a = null;

            a = Assembly.GetEntryAssembly();
            if (a == null)
                a = Assembly.GetCallingAssembly();
            if (a == null)
                a = Assembly.GetExecutingAssembly();

            defaultLocation = Path.GetDirectoryName(a.Location);
        }

        public Log()
        {
        }

        public Log(bool enabled)
        {
            this.enabled = enabled;
        }

        public Log(string fileName)
        {
            this.fileName = fileName;
        }

        public Log(string fileName, bool enabled)
        {
            this.enabled = enabled;

            this.fileName = fileName;
        }

        #endregion

        #region Open/Close file

        /// <summary>
        /// Opens the file specified in constructor.
        /// </summary>
        /// <returns>True if the file could be oppened.</returns>
        public bool OpenFile()
        {
            if (!this.enabled || this.disposed)
                return false;

            try
            {
                if (fileName.Length != 0)
                {
                    string dir = Path.GetDirectoryName(fileName);
                    if (dir.Length > 0 && !Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    // Cloase the previouslly opened file.
                    CloseFile();

                    // Open the file.
                    sw = new StreamWriter(fileName, true);
                    sw.AutoFlush = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Opens a new file to write into.
        /// </summary>
        /// <param name="fileName">The file name to be oppened.</param>
        /// <returns>True if the file could be oppened.</returns>
        public bool OpenFile(string fileName)
        {
            if (!this.enabled || this.disposed)
                return false;

            this.fileName = fileName;
            return OpenFile();
        }

        /// <summary>
        /// Closes the current opened file.
        /// </summary>
        public void CloseFile()
        {
            if (sw != null && !this.disposed)
            {
                try
                {
                    sw.Close();
                    //sw.Dispose();
                    sw = null;
                }
                catch { }
            }
        }

        #endregion

        #region Indentation

        private int indentIndex = 0;

        /// <summary>
        /// Change the indentation.
        /// </summary>
        /// <param name="value">If true, increase the indentation. If false, decrease the indentation</param>
        public void Indent(bool value)
        {
            if (value)
                indentIndex++;
            else
                indentIndex--;
        }

        /// <summary>
        /// Reset the indentation.
        /// </summary>
        public void ResetIndent()
        {
            indentIndex = 0;
        }

        #endregion

        #region Write

        /// <summary>
        /// Write the specified text to the file. No additional text.
        /// </summary>
        /// <param name="text">The text to be written to the file.</param>
        /// <returns>True if success.</returns>
        public bool Write(string text)
        {
            return WriteLog(text, false, ErrorLevel.Message, false, false);
        }

        /// <summary>
        /// Write the specified text to the file. The time stamp is added if requested by caller.
        /// </summary>
        /// <param name="text">The text to be written to the file.</param>
        /// <param name="writeTimeStamp">If true, a time stamp is added.</param>
        /// <returns>True if success.</returns>
        public bool Write(string text, bool writeNewLine)
        {
            return WriteLog(text, false, ErrorLevel.Message, writeNewLine, false);
        }

        public bool WriteNewLine()
        {
            return WriteLog(string.Empty, false, ErrorLevel.Message, true, false);
        }

        /// <summary>
        /// Write the specified text to the file. A time stamp and the newLine string are added.
        /// </summary>
        /// <param name="text">The text to be written to the file.</param>
        /// <returns>True if success.</returns>
        public bool WriteLine(string text)
        {
            return WriteLog(text, true, ErrorLevel.Message, true, true);
        }

        /// <summary>
        /// Write the specified text to the file. The time stamp is added if requested by caller. The newLine string is added automatically.
        /// </summary>
        /// <param name="text">The text to be written to the file.</param>
        /// <param name="writeTimeStamp">If true, a time stamp is added.</param>
        /// <returns>True if success.</returns>
        public bool WriteLine(string text, bool writeNewLine)
        {
            return WriteLog(text, true, ErrorLevel.Message, writeNewLine, true);
        }

        public bool WriteStart(string text)
        {
            bool b = WriteLog("[->] " + text, true, ErrorLevel.Message, true, true);
            if (b) indentIndex++;
            return b;
        }

        public bool WriteEnd(string text)
        {
            indentIndex--;
            bool b = WriteLog("[<-] " + text, true, ErrorLevel.Message, true, true);
            if (!b) indentIndex++;
            return b;
        }

        /// <summary>
        /// Write a warning text to the file. A time stamp and the newLine string are added automatically.
        /// </summary>
        /// <param name="text">The warning text to be written to the file.</param>
        /// <returns>True if success.</returns>
        public bool WriteWarning(string text)
        {
            return WriteLog(text, true, ErrorLevel.Warning, true, true);
        }

        /// <summary>
        /// Write a error text to the file. A time stamp and the newLine string are added automatically.
        /// </summary>
        /// <param name="text">The error text to be written to the file.</param>
        /// <returns>True if success.</returns>
        public bool WriteError(string text)
        {
            return WriteLog(text, true, ErrorLevel.Error, true, true);
        }
        public bool WriteError(string text, Exception ex)
        {
            Exception e = ex;
            while (e != null)
            {
                text += (text.Length == 0 ? string.Empty : "\r\n") +
                    e.GetType().Name + ": " + e.Message + "\r\nStackTrace: " + e.StackTrace;
                e = e.InnerException;
            }
            return WriteLog(text, true, ErrorLevel.Error, true, true);
        }
        public bool WriteError(Exception ex)
        {
            return WriteError(string.Empty, ex);
        }

        /// <summary>
        /// Private function used to write text to the log file.
        /// </summary>
        /// <param name="text">The text to be written to the file.</param>
        /// <param name="writeTimeStamp">If true, a time stamp is added.</param>
        /// <param name="errorLevel">The error level.</param>
        /// <param name="writeNewLine">If true, the newLine string is added at the end of the text.</param>
        /// <returns></returns>
        private bool WriteLog(string text, bool writeTimeStamp, ErrorLevel errorLevel, bool writeNewLine, bool indent)
        {
            lock (this)
            {
                try
                {
                    if (!this.enabled || this.disposed)
                        return false;

                    string temp = string.Empty;

                    // time stamp
                    if (this.writeTimeStamp && writeTimeStamp)
                    {
                        temp = DateTime.Now.ToString(this.timeStampFormat) + " - ";
                    }

                    // identation
                    if (indent)
                    {
                        for (int i = 0; i < indentIndex; i++)
                        {
                            temp += this.indentString;
                        }
                    }

                    // error level
                    switch (errorLevel)
                    {
                        case ErrorLevel.Message:
                            break;
                        case ErrorLevel.Warning:
                            //temp += (temp.Length > 0 ? " - " : "") + "[ Warning ]";
                            temp += "[ Warning ] ";
                            break;
                        case ErrorLevel.Error:
                            //temp += (temp.Length > 0 ? " - " : "") + "[  Error  ]";
                            temp += "[  Error  ] ";
                            break;
                    }

                    // the text
                    temp += text;

                    // new line
                    if (writeNewLine) temp += this.newLine;


                    sw.Write(temp);
                    return true;
                }
                catch (Exception ex)
                {
                    this.lastException = ex;
                    return false;
                }
            }
        }

        #endregion

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                //this.logLock = false;

                // If disposing equals true, dispose all managed resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.CloseFile();
                    //if (sw != null)
                    //{
                    //    sw.Close();
                    //    //sw.Dispose();
                    //    sw = null;
                    //}
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // ...

                disposed = true;
            }
        }

        ~Log()
        {
            Dispose(false);
        }

        #endregion
    }
}
