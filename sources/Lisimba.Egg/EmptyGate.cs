using System;
using System.Collections.Generic;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Exceptions;

namespace DustInTheWind.Lisimba.Egg
{
    public class EmptyGate : IGate
    {
        public IEnumerable<Exception> Warnings { get; private set; }

        public string Id
        {
            get { return "EmptyGate"; }
        }

        public string Name
        {
            get { return "Empty Gate"; }
        }

        public string Description
        {
            get { return "A Gate that knows nothing, does nothing. It just exists."; }
        }

        public EmptyGate()
        {
            Warnings = new List<Exception>();
        }

        public AddressBook Load(string fileName)
        {
            throw new EggException("EmptyGate cannot open anything.");
        }

        public void Save(AddressBook addressBook, string fileName)
        {
            throw new EggException("EmptyGate cannot save anything.");
        }
    }
}