using System;
using System.Collections.Generic;

namespace Lisimba.Cmd
{
    class CommandParser
    {
        private readonly string commandText;
        private int index;
        private List<string> items;

        public string[] Items
        {
            get
            {
                if (items == null)
                    Split();

                return items.ToArray();
            }
        }

        public CommandParser(string commandText)
        {
            if (commandText == null) throw new ArgumentNullException("commandText");

            this.commandText = commandText;
        }

        private void Split()
        {
            items = new List<string>();

            index = -1;

            while (true)
            {
                SkipSpaces();

                if (IsEndOfString())
                    break;

                int startIndex = index;
                int endIndex = -1;

                if (commandText[index] == '"')
                {
                    SkipUntilChar('"');

                    if (IsEndOfWord())
                    {
                        startIndex++;
                        endIndex = index - 1;
                    }
                    else
                    {
                        SkipUntilChar(' ');
                        endIndex = index - 1;
                    }
                }
                else
                {
                    SkipUntilChar(' ');
                    endIndex = index - 1;
                }

                string item = commandText.Substring(startIndex, endIndex - startIndex + 1);
                items.Add(item);
            }
        }

        private bool IsEndOfWord()
        {
            if (index + 1 >= commandText.Length)
                return true;

            return commandText[index + 1] == ' ';
        }

        private void SkipSpaces()
        {
            do
            {
                index++;

                if (IsEndOfString())
                    break;
            }
            while (commandText[index] == ' ');
        }

        private void SkipUntilChar(char c)
        {
            do
            {
                index++;

                if (IsEndOfString())
                    break;
            }
            while (commandText[index] != c);
        }

        private bool IsEndOfString()
        {
            return index >= commandText.Length;
        }
    }
}