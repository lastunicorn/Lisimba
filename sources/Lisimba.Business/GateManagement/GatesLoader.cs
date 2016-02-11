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
using DustInTheWind.Lisimba.Egg.GateModel;

namespace DustInTheWind.Lisimba.Business.GateManagement
{
    /// <summary>
    /// Loads all gates from all the assemblies found in the Gates directory.
    /// </summary>
    public static class GatesLoader
    {
        public static IEnumerable<IGate> LoadAllGates()
        {
            string applicationDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string gateDirectory = Path.Combine(applicationDirectory, "Gates");

            if (!Directory.Exists(gateDirectory))
                yield break;

            string[] assemblyPaths = Directory.GetFiles(gateDirectory, "*.dll");

            IEnumerable<Assembly> assemblies = assemblyPaths
                .Select(Assembly.LoadFrom);

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> gateTypes = assembly.GetExportedTypes()
                    .Where(x => x.IsClass && typeof(IGate).IsAssignableFrom(x));

                foreach (Type gateType in gateTypes)
                {
                    IGate gate = (IGate)Activator.CreateInstanceFrom(assembly.Location, gateType.FullName).Unwrap();

                    yield return gate;
                }
            }
        }
    }
}