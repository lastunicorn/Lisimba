﻿// Lisimba
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
using System.Resources;
using System.Threading;
using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.CommandLine.Properties;

namespace DustInTheWind.Lisimba.CommandLine.Flows
{
    internal class LangFlow : IFlow
    {
        private readonly EnhancedConsole console;

        public LangFlow(EnhancedConsole console)
        {
            if (console == null) throw new ArgumentNullException("console");

            this.console = console;
        }

        public void Execute(IList<string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                console.WriteLine();
                CultureInfo culture = CultureInfo.CurrentUICulture;
                console.WriteNormal(culture.EnglishName + " - " + culture.NativeName + " - " + culture.Name);
                console.WriteLine();
            }
            else
            {
                if (parameters[0] == "all")
                {
                    console.WriteLine();

                    ResourceManager rm = new ResourceManager(typeof(Resources));

                    IEnumerable<CultureInfo> availableCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .Where(x => x.Name.Length > 0)
                        .Where(x => rm.GetResourceSet(x, true, false) != null);

                    foreach (CultureInfo culture in availableCultures)
                        console.WriteLineNormal(culture.EnglishName + " - " + culture.NativeName + " - " + culture.Name);

                    console.WriteLine();
                }
                else
                {
                    string cultureName = parameters[0];
                    CultureInfo culture = CultureInfo.GetCultureInfo(cultureName);

                    Thread.CurrentThread.CurrentUICulture = culture;
                    Thread.CurrentThread.CurrentCulture = culture;

                    console.WriteLineSuccess(string.Format(Resources.LanguageChangeSuccess, culture.Name, culture.EnglishName, culture.NativeName));
                }
            }
        }
    }
}