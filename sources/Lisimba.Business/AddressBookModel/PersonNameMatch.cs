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

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class PersonNameMatch
    {
        public FuzzyMatch Match { get; private set; }
        public PersonName PersonName1 { get; private set; }
        public PersonName PersonName2 { get; private set; }

        public PersonNameMatch(PersonName personName1, PersonName personName2)
        {
            PersonName1 = personName1;
            PersonName2 = personName2;

            CalculateMatch();
        }

        private void CalculateMatch()
        {
            Match = PersonName1 == null || PersonName2 == null
                ? FuzzyMatch.NotEqual
                : Compare();
        }

        private FuzzyMatch Compare()
        {
            if (PersonName1.IsEmpty() || PersonName2.IsEmpty())
                return FuzzyMatch.NotEqual;

            FuzzyMatch firstNameMatch = MatchNames(PersonName1.FirstName, PersonName2.FirstName);

            if (firstNameMatch == FuzzyMatch.NotEqual)
                return FuzzyMatch.NotEqual;

            FuzzyMatch middleNameMatch = MatchNames(PersonName1.MiddleName, PersonName2.MiddleName);

            if (middleNameMatch == FuzzyMatch.NotEqual)
                return FuzzyMatch.NotEqual;

            FuzzyMatch lastNameMatch = MatchNames(PersonName1.LastName, PersonName2.LastName);

            if (lastNameMatch == FuzzyMatch.NotEqual)
                return FuzzyMatch.NotEqual;

            FuzzyMatch nicknameMatch = MatchNames(PersonName1.Nickname, PersonName2.Nickname);

            if (nicknameMatch == FuzzyMatch.NotEqual)
                return FuzzyMatch.NotEqual;

            return firstNameMatch == FuzzyMatch.AlmostEqual ||
                   middleNameMatch == FuzzyMatch.AlmostEqual ||
                   lastNameMatch == FuzzyMatch.AlmostEqual ||
                   nicknameMatch == FuzzyMatch.AlmostEqual
                ? FuzzyMatch.AlmostEqual
                : FuzzyMatch.Equal;
        }

        private FuzzyMatch MatchNames(string name1, string name2)
        {
            if (name1.Length == 0 && name2.Length == 0)
                return FuzzyMatch.Equal;

            if (name1.Length == 0 || name2.Length == 0)
                return FuzzyMatch.AlmostEqual;

            return name1.Equals(name2)
                ? FuzzyMatch.Equal
                : FuzzyMatch.NotEqual;
        }
    }
}