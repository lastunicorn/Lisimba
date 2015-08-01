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
using System.Configuration;
using System.IO;

namespace DustInTheWind.Desmond.Logging
{
    /// <summary>
    /// Helper class that opens the log files.
    /// </summary>
    public class LogManager
    {
        private const string LOG_DIR = "";
        private const string LOG_FILE = "Desmond - [DATE].log";

        private static string ConfigLogDir
        {
            get
            {
                string logDir = ConfigurationSettings.AppSettings["logDir"];
                if (logDir == null)
                {
                    return LOG_DIR;
                }
                else
                {
                    return logDir;
                }
            }
        }

        private static string ConfigLogFile
        {
            get
            {
                string logFile = ConfigurationSettings.AppSettings["logFile"];
                if (logFile == null || logFile.Length == 0)
                {
                    return LOG_FILE;
                }
                else
                {
                    return logFile;
                }
            }
        }

        /// <summary>
        /// Returns the global log file name prepended with the log folder. Both read from config file.
        /// </summary>
        /// <returns>The global log file name.</returns>
        public static string GetLogFileFullName()
        {
            string logFile = ConfigLogFile.Replace("[DATE]", DateTime.Now.ToString("yyyy MM dd"));
            return GetLogFileFullName(logFile);
        }

        /// <summary>
        /// Prepends the specified log file name with the log folder read from config file.
        /// </summary>
        /// <param name="fileShortName">The log short file name.</param>
        /// <returns>The log file name.</returns>
        public static string GetLogFileFullName(string fileShortName)
        {
            string logDir = ConfigLogDir;
            string logFile = fileShortName;

            if (logFile == null || logFile.Length == 0)
            {
                logFile = ConfigLogFile.Replace("[DATE]", DateTime.Now.ToString("yyyy MM dd"));
            }

            return Path.Combine(logDir, logFile);
        }

        /// <summary>
        /// Opens the global log file.
        /// </summary>
        /// <returns>A reference to the global log file.</returns>
        public static Log OpenGlobalLogFile()
        {
            string logFullFileName = GetLogFileFullName();

            Log.Instance.OpenFile(logFullFileName);

            return Log.Instance;
        }

        /// <summary>
        /// Opens the global log file.
        /// </summary>
        /// <returns>A reference to the global log file.</returns>
        public static Log OpenGlobalLogFile(string fileName)
        {
            Log.Instance.OpenFile(fileName);

            return Log.Instance;
        }

        /// <summary>
        /// Opens a log file. The log file will be created in the log folder specified in config file.
        /// </summary>
        /// <param name="fileShortName">The name of the log file.</param>
        /// <returns>A reference to the log file opened.</returns>
        public static Log OpenLogFile(string fileShortName)
        {
            string logFullFileName = GetLogFileFullName(fileShortName);

            Log log = new Log(logFullFileName);
            log.OpenFile();

            return log;
        }

        public static Log OpenLogFile(string fileShortName, bool enabled)
        {
            string logFullFileName = GetLogFileFullName(fileShortName);

            Log log = new Log(logFullFileName, enabled);
            log.OpenFile();

            return log;
        }
    }
}