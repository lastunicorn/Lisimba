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

namespace DustInTheWind.ConsoleCommon
{
    class YesNoCancelItem
    {
        public string Culture { get; private set; }

        public ConsoleKey YesKey { get; private set; }
        public ConsoleKey NoKey { get; private set; }
        public ConsoleKey CancelKey { get; private set; }

        public string YesText { get; private set; }
        public string NoText { get; private set; }
        public string CancelText { get; private set; }

        public YesNoCancelItem(string culture, ConsoleKey yesKey, ConsoleKey noKey, ConsoleKey cancelKey, string yesText, string noText, string cancelText)
        {
            if (culture == null) throw new ArgumentNullException("culture");
            if (yesText == null) throw new ArgumentNullException("yesText");
            if (noText == null) throw new ArgumentNullException("noText");
            if (cancelText == null) throw new ArgumentNullException("cancelText");

            Culture = culture;
            YesKey = yesKey;
            NoKey = noKey;
            CancelKey = cancelKey;
            YesText = yesText;
            NoText = noText;
            CancelText = cancelText;
        }

        public override string ToString()
        {
            return string.Format("[{0}-{1} {2}-{3} {4}-{5}]", YesKey.ToString().ToLower(), YesText, NoKey.ToString().ToLower(), NoText, CancelKey.ToString().ToLower(), CancelText);
        }
    }
}