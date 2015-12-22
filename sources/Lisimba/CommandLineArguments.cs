// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
    internal class CommandLineArguments
    {
        private readonly List<string> keys = new List<string>();
        private readonly List<string> values = new List<string>();

        public bool AcceptEmptyKeys { get; set; }

        public CommandLineArguments(string[] args)
        {
            AcceptEmptyKeys = true;
            Parse(args);
        }

        public void Add(string key, string value)
        {
            keys.Add(key);
            values.Add(value);
        }

        public int Count
        {
            get { return keys.Count; }
        }

        public string this[string key]
        {
            get { return values[keys.IndexOf(key)]; }
        }

        public string this[int index]
        {
            get { return values[index]; }
        }

        public void Parse(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.Length == 0)
                    continue;

                string key;
                string value;

                if (arg[0] == '-')
                {
                    if (arg.Length == 1)
                        throw new ApplicationException("Invalid arguments.");

                    int pos = arg.IndexOf(":");
                    if (pos == -1)
                    {
                        key = arg.Substring(1);
                        value = string.Empty;
                    }
                    else
                    {
                        key = arg.Substring(1, pos - 1);
                        value = arg.Substring(pos + 1);
                    }
                }
                else
                {
                    if (AcceptEmptyKeys)
                    {
                        key = string.Empty;
                        value = arg;
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