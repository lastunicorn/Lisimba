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

using System.Text;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public abstract class ImporterBase<TParent, TValue> : IImporter
    {
        protected abstract string Name { get; }

        public TValue SourceValue { get; set; }
        public TValue DestinationValue { get; set; }
        public TParent DestinationParent { get; set; }
        public ImportType ImportType { get; set; }
        public TValue MergedValue { get; set; }

        object IImporter.SourceValue
        {
            get { return SourceValue; }
            set { SourceValue = (TValue)value; }
        }

        object IImporter.DestinationValue
        {
            get { return DestinationValue; }
            set { DestinationValue = (TValue)value; }
        }

        object IImporter.DestinationParent
        {
            get { return DestinationParent; }
            set { DestinationParent = (TParent)value; }
        }

        object IImporter.MergedValue
        {
            get { return MergedValue; }
            set { MergedValue = (TValue)value; }
        }

        public void Execute(StringBuilder sb, bool simulate)
        {
            switch (ImportType)
            {
                case ImportType.Ignore:
                    break;

                case ImportType.AddAsNew:
                    AddAsNew(sb, simulate);
                    break;

                case ImportType.Merge:
                    Merge(sb, simulate);
                    break;

                case ImportType.Replace:
                    Replace(sb, simulate);
                    break;

                default:
                    string message = string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", DestinationValue, SourceValue, ImportType);
                    throw new LisimbaException(message);
            }
        }

        protected virtual void AddAsNew(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Adding {0}: {1}", Name, SourceValue));

            if (!simulate)
                AddAsNew();
        }

        protected virtual void Merge(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Merging {0} '{1}' and '{2}'.", Name, DestinationValue, SourceValue));

            if (!simulate)
                Merge();
        }

        protected virtual void Replace(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Replacing {0} '{1}' with '{2}'.", Name, DestinationValue, SourceValue));

            if (!simulate)
                Replace();
        }

        protected abstract void AddAsNew();
        protected abstract void Merge();
        protected abstract void Replace();
    }
}