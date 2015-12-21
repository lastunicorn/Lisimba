using System;
using System.Collections.Generic;

namespace Lisimba.Cmd.CommandSystem
{
    class CommandParser
    {
        private readonly string commandText;
        private int index;
        private List<string> items;
        private int quotaCount;
        private int wordStartIndex;
        private int wordEndIndex;

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

                string item = ReadNextWord();
                items.Add(item);
            }
        }

        private string ReadNextWord()
        {
            IdentifyNextWordPosition();

            int wordLength = wordEndIndex - wordStartIndex + 1;
            return commandText.Substring(wordStartIndex, wordLength);
        }

        private void IdentifyNextWordPosition()
        {
            wordStartIndex = index;
            wordEndIndex = -1;

            quotaCount = 0;

            do
            {
                SkipUntilSpace();
            }
            while (quotaCount % 2 == 1 && !IsEndOfString());

            wordEndIndex = index - 1;

            bool isWordEnclosedInQuotas = quotaCount == 2 && commandText[wordStartIndex] == '"' && commandText[wordEndIndex] == '"';
            
            if (isWordEnclosedInQuotas)
            {
                wordStartIndex++;
                wordEndIndex--;
            }
        }

        private void SkipUntilSpace()
        {
            if (IsEndOfString())
                return;

            do
            {
                if (commandText[index] == '"')
                    quotaCount++;

                index++;

                if (IsEndOfString())
                    break;
            }
            while (commandText[index] != ' ');
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

        private bool IsEndOfString()
        {
            return index >= commandText.Length;
        }
    }
}