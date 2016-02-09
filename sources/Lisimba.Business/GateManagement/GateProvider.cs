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
using System.IO;
using System.Linq;
using System.Reflection;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.GateModel;

namespace DustInTheWind.Lisimba.Business.GateManagement
{
    public class GateProvider
    {
        public const string GatesDirectory = "Gates";

        public IEnumerable<IGate> GetAllGates()
        {
            string gateDirectory = GetGateDirectory();

            if (gateDirectory == null || !Directory.Exists(gateDirectory))
                yield break;

            IEnumerable<Assembly> assemblies = GetAllAssembliesFrom(gateDirectory);

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<IGate> gates = GetAllGatesFrom(assembly);

                foreach (IGate gate in gates)
                    yield return gate;
            }
        }

        private static string GetGateDirectory()
        {
            string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            return applicationDirectory == null
                ? null
                : Path.Combine(applicationDirectory, GatesDirectory);
        }

        private static IEnumerable<Assembly> GetAllAssembliesFrom(string gateDirectory)
        {
            string[] assemblyPaths = Directory.GetFiles(gateDirectory, "*.dll");

            return assemblyPaths
                .Select(Assembly.LoadFrom);
        }

        private static IEnumerable<IGate> GetAllGatesFrom(Assembly assembly)
        {
            IEnumerable<Type> gateTypes = assembly.GetExportedTypes()
                .Where(x => x.IsClass && typeof(IGate).IsAssignableFrom(x));

            return gateTypes
                .Select(x => (IGate)Activator.CreateInstanceFrom(assembly.Location, x.FullName).Unwrap());
        }
    }
}