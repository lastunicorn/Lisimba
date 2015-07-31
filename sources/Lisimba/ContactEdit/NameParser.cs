using System;
using System.Text.RegularExpressions;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    internal class NameParser
    {
        private string one;
        private string two;
        private string three;
        private string nickname;

        public bool Success { get; private set; }

        private string firstName;
        private string middleName;
        private string lastName;

        public PersonName Result { get; private set; }

        public NameParser(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            Parse(name);
        }

        private void Parse(string name)
        {
            Success = ExtractParts(name);

            if (!Success)
                return;

            InterpretParts();

            Success = firstName.Length != 0 || middleName.Length != 0 || lastName.Length != 0 || nickname.Length != 0;

            if (!Success)
                return;

            CreateResult();
        }

        private bool ExtractParts(string name)
        {
            Match match = GetMatch(name);

            if (match == null || !match.Success)
                return false;

            one = match.Groups["one"].Value;
            two = match.Groups["two"].Value;
            three = match.Groups["three"].Value;
            nickname = match.Groups["nickname"].Value;

            return true;
        }

        private static Match GetMatch(string name)
        {
            // first middle last (nick)
            //Regex regex = new Regex(@"^(?<one>\w*) ?(?<two>\w*) ?(?<three>\w*) ?(\((?<nickname>\w*)\))?$");

            // nick (first middle last)
            Regex regex1 = new Regex(@"^(?<nickname>\w*) ?[(](?:(?<one>\w*) ?(?<two>\w*) ?(?<three>\w*))?[)]$");

            Match match = regex1.Match(name);

            if (match.Success)
                return match;

            // first middle last
            Regex regex2 = new Regex(@"^(?<one>\w*) ?(?<two>\w*) ?(?<three>\w*)$");

            match = regex2.Match(name);

            if (match.Success)
                return match;
            
            return null;
        }

        private bool InterpretParts()
        {
            if (three.Length > 0)
            {
                firstName = one;
                middleName = two;
                lastName = three;

                return true;
            }

            if (two.Length > 0)
            {
                firstName = one;
                middleName = string.Empty;
                lastName = two;

                return true;
            }

            if (one.Length > 0)
            {
                firstName = one;
                middleName = string.Empty;
                lastName = string.Empty;

                return true;
            }

            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            return false;
        }

        private void CreateResult()
        {
            Result = new PersonName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Nickname = nickname
            };
        }
    }
}