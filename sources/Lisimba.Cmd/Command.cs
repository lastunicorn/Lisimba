using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisimba.Cmd
{
    class Command
    {
        private readonly List<string> parameters;

        public string Name { get; private set; }

        public bool HasParameters
        {
            get { return parameters.Any(); }
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index > parameters.Count)
                    return null;

                if (index == 0)
                    return Name;

                return parameters[index - 1];
            }
        }

        public Command(string commandText)
        {
            if (commandText == null)
            {
                Name = string.Empty;
                parameters = new List<string>();
            }
            else
            {
                string[] items = commandText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Name = items[0];

                parameters = items
                    .Skip(1)
                    .ToList();
            }
        }
    }
}