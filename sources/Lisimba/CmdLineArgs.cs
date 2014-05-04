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
