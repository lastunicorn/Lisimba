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

using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.GateModel;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Operations
{
    internal class ImportOperation : OperationBase<IGate>
    {
        public ImportOperation(UserInterface userInterface)
            : base(userInterface)
        {
        }

        public override string ShortDescription
        {
            get { return LocalizedResources.ImportOperationDescription; }
        }

        protected override void DoExecute(IGate gate)
        {
            userInterface.DisplayInfo("Import using " + gate.Name + ".");
        }
    }
}