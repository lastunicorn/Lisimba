// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace DustInTheWind.ConsoleCommon.Templating
{
    public class ConsoleTemplate : IEnumerable<TemplateElement>
    {
        private readonly string template;
        private readonly IDictionary<string, object> parameters;

        public ConsoleTemplate(string template, IDictionary<string, object> parameters)
        {
            this.template = "<template>" + template + "</template>";
            this.parameters = parameters;
        }

        public string Template
        {
            get { return template; }
        }

        public IDictionary<string, object> Parameters
        {
            get { return parameters; }
        }

        public IEnumerator<TemplateElement> GetEnumerator()
        {
            using (StringReader sr = new StringReader(template))
            {
                using (XmlReader reader = new XmlTextReader(sr))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Whitespace)
                            yield return new TemplateElement
                            {
                                Type = TemplateElementType.Text,
                                Value = reader.Value
                            };

                        if (reader.NodeType == XmlNodeType.Text)
                        {
                            string text = parameters != null && parameters.Any()
                                ? ApplyParameters(reader.Value, parameters)
                                : reader.Value;

                            yield return new TemplateElement
                            {
                                Type = TemplateElementType.Text,
                                Value = text
                            };
                        }

                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "emp")
                                yield return new TemplateElement
                                {
                                    Type = TemplateElementType.Action,
                                    Value = reader.Name
                                };
                        }

                        if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            if (reader.Name == "emp")
                                yield return new TemplateElement
                                {
                                    Type = TemplateElementType.EndAction,
                                    Value = reader.Name
                                };
                        }
                    }
                }
            }
        }

        private static string ApplyParameters(string template, IDictionary<string, object> parameters)
        {
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                template = template.Replace("{" + parameter.Key + "}", parameter.Value.ToString());
            }

            return template;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static ConsoleTemplate CreateFromString(string templateText, IDictionary<string, object> parameters)
        {
            return new ConsoleTemplate(templateText, parameters);
        }

        public static ConsoleTemplate CreateFromEmbeddedFile(string fileName, dynamic parameters)
        {
            Dictionary<string, object> parametersDictionary = ToDictionary(parameters);


            Assembly assembly = Assembly.GetCallingAssembly();
            string template = GetTemplate(fileName, assembly);
            return new ConsoleTemplate(template, parametersDictionary);
        }

        private static Dictionary<string, object> ToDictionary(dynamic parameters)
        {
            return ((object)parameters).GetType().GetProperties()
                .ToDictionary(x => x.Name, x => x.GetValue(parameters));
        }

        private static string GetTemplate(string templateFileName, Assembly assembly)
        {
            Stream manifestResourceStream = assembly.GetManifestResourceStream(templateFileName);

            if (manifestResourceStream == null)
                throw new ApplicationException("The template '" + templateFileName + "' cannot be found.");

            using (StreamReader textStreamReader = new StreamReader(manifestResourceStream))
            {
                return textStreamReader.ReadToEnd();
            }
        }
    }
}