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
using DustInTheWind.Lisimba.Business;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    class ActiveObservers
    {
        private readonly ObserverFactory observerFactory;
        private List<IObserver> observers;

        public ActiveObservers(ObserverFactory observerFactory)
        {
            if (observerFactory == null) throw new ArgumentNullException("observerFactory");

            this.observerFactory = observerFactory;
        }

        public void Start()
        {
            if (observers == null)
                observers = CreateObservers();

            foreach (IObserver observer in observers)
                observer.Start();
        }

        private List<IObserver> CreateObservers()
        {
            return observerFactory.CreateObservers();
        }

        public void Stop()
        {
            if (observers != null)
            {
                foreach (IObserver observer in observers)
                    observer.Stop();
            }
        }
    }
}