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

using System.Collections.Generic;
using System.Text;
using DustInTheWind.Lisimba.Business.Importing.Importers;

namespace DustInTheWind.Lisimba.Business.Importing
{
    public abstract class ItemImportBase<TParent, TValue> : IItemImport
    {
        protected abstract string Name { get; }

        public List<IItemImport> ItemImports { get; set; }

        public TValue SourceValue { get; set; }
        public TValue DestinationValue { get; set; }
        public TParent DestinationParent { get; set; }
        public ImportType ImportType { get; set; }
        public TValue MergedValue { get; set; }

        object IItemImport.SourceValue
        {
            get { return SourceValue; }
            set { SourceValue = (TValue)value; }
        }

        object IItemImport.DestinationValue
        {
            get { return DestinationValue; }
            set { DestinationValue = (TValue)value; }
        }

        object IItemImport.DestinationParent
        {
            get { return DestinationParent; }
            set { DestinationParent = (TParent)value; }
        }

        object IItemImport.MergedValue
        {
            get { return MergedValue; }
            set { MergedValue = (TValue)value; }
        }

        //protected ItemImportBase()
        //{
        //}

        //protected ItemImportBase(IItemComparison<TParent, TValue> itemComparison)
        //{
        //    if (itemComparison == null) throw new ArgumentNullException("itemComparison");

        //    SourceValue = itemComparison.ValueRight;
        //    DestinationValue = itemComparison.ValueLeft;
        //    DestinationParent = itemComparison.ParentLeft;

        //    switch (itemComparison.Equality)
        //    {
        //        case ItemEquality.BothEmpty:
        //        case ItemEquality.LeftExists:
        //        case ItemEquality.Equal:
        //            ImportType = ImportType.Ignore;
        //            break;

        //        case ItemEquality.RightExists:
        //            ImportType = ImportType.AddAsNew;
        //            break;

        //        case ItemEquality.Different:
        //            ImportType = ImportType.Replace;
        //            break;

        //        case ItemEquality.Similar:
        //            ImportType = ImportType.Merge;
        //            break;

        //        default:
        //            throw new LisimbaException("Invalid comparison item.");
        //    }
        //}

        public void Execute(StringBuilder sb, bool simulate)
        {
            switch (ImportType)
            {
                case ImportType.Ignore:
                    break;

                case ImportType.AddAsNew:
                    ExecuteAddAsNew(sb, simulate);
                    break;

                case ImportType.Merge:
                    ExecuteMerge(sb, simulate);
                    break;

                case ImportType.Replace:
                    ExecuteReplace(sb, simulate);
                    break;

                default:
                    string message = string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", DestinationValue, SourceValue, ImportType);
                    throw new LisimbaException(message);
            }
        }

        private void ExecuteAddAsNew(StringBuilder sb, bool simulate)
        {
            if (!simulate)
                AddAsNew();

            sb.AppendLine(string.Format("Added {0}: {1}", Name, SourceValue));
        }

        private void ExecuteMerge(StringBuilder sb, bool simulate)
        {
            sb.AppendLine(string.Format("Merging {0} '{1}' and '{2}'.", Name, DestinationValue, SourceValue));

            if (!simulate)
                Merge();

            if (ItemImports != null)
            {
                foreach (IItemImport importRule in ItemImports)
                {
                    try
                    {
                        importRule.Execute(sb, simulate);
                    }
                    catch (MergeConflictException)
                    {
                        sb.AppendLine(string.Format("Merge conflict. dest: '{0}'; source: '{1}'; import type: {2}.", importRule.DestinationValue, importRule.SourceValue, importRule.ImportType));
                    }
                    catch
                    {
                        sb.AppendLine(string.Format("Invalid import rule for dest: '{0}'; source: '{1}'; import type: {2}.", importRule.DestinationValue, importRule.SourceValue, importRule.ImportType));
                        throw;
                    }
                }
            }
        }

        private void ExecuteReplace(StringBuilder sb, bool simulate)
        {
            if (!simulate)
                Replace();

            sb.AppendLine(string.Format("Replaced {0} '{1}' with '{2}'.", Name, DestinationValue, SourceValue));
        }

        protected abstract void AddAsNew();
        protected abstract void Merge();
        protected abstract void Replace();
    }
}