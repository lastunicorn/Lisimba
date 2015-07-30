using System;
using System.Text.RegularExpressions;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    internal class NameParser
    {
        public bool Success { get; private set; }

        //private string firstName;
        //private string middleName;
        //private string lastName;
        //private string nickname;

        public PersonName Result { get; private set; }

        public NameParser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            Regex regex = new Regex(@"^(?<one>\w*)\s?(?<two>\w*)\s?(?<three>\w*)\s?(\((?<nickname>\w*)\))?$");

            Match match = regex.Match(name);

            if (!match.Success)
                return;

            string one = match.Groups["one"].Value;
            string two = match.Groups["two"].Value;
            string three = match.Groups["three"].Value;
            string nickname = match.Groups["nickname"].Value;

            string firstName = string.Empty;
            string middleName = string.Empty;
            string lastName = string.Empty;

            if (three.Length > 0)
            {
                firstName = one;
                middleName = two;
                lastName = three;
            }
            else if (two.Length > 0)
            {
                firstName = one;
                lastName = two;
            }
            else if (one.Length > 0)
            {
                firstName = one;
            }

            Result = new PersonName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Nickname = nickname
            };

            Success = true;
        }
    }
}