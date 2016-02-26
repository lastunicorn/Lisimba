// Lisimba
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

namespace DustInTheWind.ConsoleCommon.ConsoleCommandHandling
{
    /// <summary>
    /// Splitts a console command into components based on spaces.
    /// To include spaces in a component of the command, the whole component must be enclosed in double quotes.
    /// </summary>
    public class ConsoleCommandSplitter
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

        public ConsoleCommandSplitter(string commandText)
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