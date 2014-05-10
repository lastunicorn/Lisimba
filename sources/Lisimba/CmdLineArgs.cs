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

namespace DustInTheWind.Lisimba
{
    class CmdLineArgs
    {
        private List<string> keys = new List<string>();
        private List<string> values = new List<string>();

        private bool acceptEmptyKeys = true;
        public bool AcceptEmptyKeys
        {
            get { return acceptEmptyKeys; }
            set { acceptEmptyKeys = value; }
        }


        public CmdLineArgs(string[] args)
        {
            Parse(args);
        }

        public void Add(string key, string value)
        {
            keys.Add(key);
            values.Add(value);
        }

        public int Count
        {
            get
            {
                return keys.Count;
            }
        }

        public string this[string key]
        {
            get
            {
                return values[keys.IndexOf(key)];
            }
        }

        public string this[int index]
        {
            get
            {
                return values[index];
            }
        }

        public void Parse(string[] args)
        {
            int pos = -1;
            string key;
            string value;

            foreach (string s in args)
            {
                if (s.Length == 0)
                    continue;

                if (s[0] == '-')
                {
                    if (s.Length == 1)
                        throw new ApplicationException("Invalid arguments.");

                    pos = s.IndexOf(":");
                    if (pos == -1)
                    {
                        key = s.Substring(1);
                        value = string.Empty;
                    }
                    else
                    {
                        key = s.Substring(1, pos - 1);
                        value = s.Substring(pos + 1);
                    }
                }
                else
                {
                    if (acceptEmptyKeys)
                    {
                        key = string.Empty;
                        value = s;
                    }
                    else
                    {
                        throw new ApplicationException("Invalid arguments.");
                    }
                }

                Add(key, value);
            }
        }
    }
}
