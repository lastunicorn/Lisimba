using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lisimba.Cmd.Common
{
    class Command : IEnumerable<string>
    {
        private readonly List<string> parameters;

        public string Name { get; private set; }

        public bool HasParameters
        {
            get { return parameters.Any(); }
        }

        public int ParameterCount
        {
            get { return parameters.Count; }
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
                CommandParser commandParser = new CommandParser(commandText);
                string[] items = commandParser.Items;

                Name = items.Length > 0 ? items[0] : string.Empty;

                parameters = items
                    .Skip(1)
                    .ToList();
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}