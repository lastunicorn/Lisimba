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
using System.Runtime.Serialization;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class MergeConflictException : Exception
    {
        private const string DefaultMessage = "The merge cannot be performed automatically. Conflicts exists.";

        private readonly IImporter importer;

        public MergeConflictException(IImporter importer)
            : base(DefaultMessage)
        {
            if (importer == null) throw new ArgumentNullException("importer");
            this.importer = importer;
        }

        public MergeConflictException(IImporter importer, Exception innerException)
            : base(DefaultMessage, innerException)
        {
            if (importer == null) throw new ArgumentNullException("importer");
            this.importer = importer;
        }

        protected MergeConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}