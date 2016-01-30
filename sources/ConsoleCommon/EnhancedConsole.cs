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
using DustInTheWind.ConsoleCommon.Templating;

namespace DustInTheWind.ConsoleCommon
{
    public class EnhancedConsole
    {
        public ConsoleColor NormalColor { get; set; }
        public ConsoleColor SuccessColor { get; set; }
        public ConsoleColor WarningColor { get; set; }
        public ConsoleColor ErrorColor { get; set; }
        public ConsoleColor EmphasizeColor { get; set; }

        public EnhancedConsole()
        {
            NormalColor = ConsoleColor.Gray;
            SuccessColor = ConsoleColor.Green;
            WarningColor = ConsoleColor.Yellow;
            ErrorColor = ConsoleColor.Red;
            EmphasizeColor = ConsoleColor.White;
        }

        public void WriteNormal(string text)
        {
            Write(text, NormalColor);
        }

        public void WriteLineNormal(string text)
        {
            WriteLine(text, NormalColor);
        }

        public void WriteSuccess(string text)
        {
            Write(text, SuccessColor);
        }

        public void WriteLineSuccess(string text)
        {
            WriteLine(text, SuccessColor);
        }

        public void WriteWarning(string text)
        {
            Write(text, WarningColor);
        }

        public void WriteLineWarning(string text)
        {
            WriteLine(text, WarningColor);
        }

        public void WriteError(string text)
        {
            Write(text, ErrorColor);
        }

        public void WriteLineError(string text)
        {
            WriteLine(text, ErrorColor);
        }
        public void WriteEmphasize(string text)
        {
            Write(text, EmphasizeColor);
        }

        public void WriteLineEmphasize(string text)
        {
            WriteLine(text, EmphasizeColor);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(false);
        }

        private static void Write(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        private static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public void DisplayTemplate(ConsoleTemplate consoleTemplate)
        {
            TextType textType = TextType.Normal;

            foreach (TemplateElement templateElement in consoleTemplate)
            {
                switch (templateElement.Type)
                {
                    case TemplateElementType.Text:
                        DisplayText(templateElement.Value, textType);
                        break;

                    case TemplateElementType.Action:
                        if (templateElement.Value == "emp")
                            textType = TextType.Emphasies;
                        break;

                    case TemplateElementType.EndAction:
                        if (templateElement.Value == "emp")
                            textType = TextType.Normal;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void DisplayText(string text, TextType textType)
        {
            switch (textType)
            {
                case TextType.Normal:
                    WriteNormal(text);
                    break;

                case TextType.Success:
                    WriteSuccess(text);
                    break;

                case TextType.Warning:
                    WriteWarning(text);
                    break;

                case TextType.Error:
                    WriteError(text);
                    break;

                case TextType.Emphasies:
                    WriteEmphasize(text);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool? AskYesNoCancelQuestion(string question)
        {
            YesNoCancel ync = new YesNoCancel();

            string text = ync.FormatQuestion(question);
            WriteNormal(text);

            ConsoleKeyInfo key = Console.ReadKey(false);
            Console.WriteLine();

            return ync.Interpret(key.Key);
        }
    }
}