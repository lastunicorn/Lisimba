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
using System.Globalization;
using System.Linq;

namespace DustInTheWind.ConsoleCommon
{
    class YesNoCancel
    {
        private static readonly List<YesNoCancelItem> Items = new List<YesNoCancelItem>
        {
            new YesNoCancelItem("en", ConsoleKey.Y, ConsoleKey.N, ConsoleKey.C, "yes", "no", "cancel"),
            new YesNoCancelItem("ro", ConsoleKey.D, ConsoleKey.N, ConsoleKey.A, "da", "nu", "anulare")
        };

        private readonly YesNoCancelItem item;

        public YesNoCancel()
        {
            string cultureName = CultureInfo.CurrentUICulture.Name;

            item = Items.FirstOrDefault(x => x.Culture == cultureName) ?? Items[0];
        }

        public bool? Interpret(ConsoleKey key)
        {
            if (key == item.YesKey)
                return true;

            if (key == item.NoKey)
                return false;

            return null;
        }

        public string FormatQuestion(string question)
        {
            return string.Format("{0} {1} ", question, item);
        }
    }
}