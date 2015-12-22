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

namespace Lisimba.Cmd.Common
{
    /// <summary>
    /// It is a wrapper over the System.Console class.
    /// Offers functionality to write colored texts.
    /// </summary>
    static class ConsoleHelper
    {
        private const ConsoleColor DefaultColor = ConsoleColor.White;
        private const ConsoleColor SuccessColor = ConsoleColor.Green;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;

        public static void WriteSuccess(string text)
        {
            Write(text, SuccessColor);
        }

        public static void WriteLineSuccess(string text)
        {
            WriteLine(text, SuccessColor);
        }

        public static void WriteError(string text)
        {
            Write(text, ErrorColor);
        }

        public static void WriteLineError(string text)
        {
            WriteLine(text, ErrorColor);
        }
        public static void WriteEmphasize(string text)
        {
            Write(text, DefaultColor);
        }

        public static void WriteLineEmphasize(string text)
        {
            WriteLine(text, DefaultColor);
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
    }
}