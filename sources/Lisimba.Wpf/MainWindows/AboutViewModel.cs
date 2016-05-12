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

using System.Reflection;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class AboutViewModel : ViewModelBase
    {
        private readonly Assembly mainAssembly;

        public string Title { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }

        public AboutViewModel()
        {
            mainAssembly = Assembly.GetEntryAssembly();

            Title = GetProductName();
            Name = GetProductName();
            Version = string.Format("Version. {0}", GetVersion());
            Author = GetAuthor();
        }

        private string GetProductName()
        {
            AssemblyProductAttribute assemblyProductAttribute = mainAssembly.GetCustomAttribute<AssemblyProductAttribute>();

            return assemblyProductAttribute == null
                ? string.Empty
                : assemblyProductAttribute.Product;
        }

        private string GetVersion()
        {
            return mainAssembly.GetName().Version.ToString();

            //AssemblyVersionAttribute assemblyVersionAttribute = mainAssembly.GetCustomAttribute<AssemblyVersionAttribute>();

            //return assemblyVersionAttribute == null
            //    ? string.Empty
            //    : assemblyVersionAttribute.Version;
        }

        private string GetAuthor()
        {
            AssemblyCompanyAttribute assemblyCompanyAttribute = mainAssembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            return assemblyCompanyAttribute == null
                ? string.Empty
                : assemblyCompanyAttribute.Company;
        }
    }
}
