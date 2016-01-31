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
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.Services
{
    internal class AvailableOperations
    {
        private readonly Dictionary<Type, IOperation> operations;

        public T GetOperation<T>()
            where T : IOperation
        {
            Type type = typeof(T);
            IOperation operation = operations[type];
            return (T)operation;
        }

        public AvailableOperations()
        {
            operations = new Dictionary<Type, IOperation>();
        }

        public void Add(IOperation operation)
        {
            if (operation == null) throw new ArgumentNullException("operation");

            Type operationType = operation.GetType();
            operations.Add(operationType, operation);
        }
    }
}