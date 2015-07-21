// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    [ConfigurationCollection(typeof(RecentFilesConfigElement), AddItemName = "file")]
    public class RecentFilesConfigElementCollection : ConfigurationElementCollection
    {
        public RecentFilesConfigElement this[int index]
        {
            get { return BaseGet(index) as RecentFilesConfigElement; }
        }

        public void AddNewRecentFile(string fileName)
        {
            int i = 0;
            while (i < Count)
            {
                if (this[i].FileName.Equals(fileName))
                {
                    BaseRemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            RecentFilesConfigElement element = CreateNewElement() as RecentFilesConfigElement;
            element.FileName = fileName;

            BaseAdd(0, element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RecentFilesConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RecentFilesConfigElement)element).FileName;
        }
    }
}
