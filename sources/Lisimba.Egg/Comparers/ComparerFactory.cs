// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using System.Collections;
using DustInTheWind.Lisimba.Egg.Enums;

namespace DustInTheWind.Lisimba.Egg.Comparers
{
    public static class ComparerFactory
    {
        public static IComparer GetComparer(ContactsSortingType sortField)
        {
            switch (sortField)
            {
                default:
                case ContactsSortingType.Birthday:
                    return new MultipleComparer(new IComparer[]
                    {
                        new ContactByBirthdayComparer(),
                        new ContactByNicknameComparer(),
                        new ContactByFirstNameComparer(),
                        new ContactByLastNameComparer(),
                        new ContactByMiddleNameComparer()
                    });

                case ContactsSortingType.BirthDate:
                    return new MultipleComparer(new IComparer[]
                    {
                        new ContactsByBirthdateComparer(),
                        new ContactByNicknameComparer(),
                        new ContactByFirstNameComparer(),
                        new ContactByLastNameComparer(),
                        new ContactByMiddleNameComparer()
                    });

                case ContactsSortingType.FirstName:
                    return new MultipleComparer(new IComparer[]
                    {
                        new ContactByFirstNameComparer(),
                        new ContactByLastNameComparer(),
                        new ContactByMiddleNameComparer(),
                        new ContactByNicknameComparer()
                    });

                case ContactsSortingType.LastName:
                    return new MultipleComparer(new IComparer[]
                    {
                        new ContactByLastNameComparer(),
                        new ContactByFirstNameComparer(),
                        new ContactByMiddleNameComparer(),
                        new ContactByNicknameComparer()
                    });

                case ContactsSortingType.Nickname:
                    return new MultipleComparer(new IComparer[]
                    {
                        new ContactByNicknameComparer(),
                        new ContactByFirstNameComparer(),
                        new ContactByLastNameComparer(),
                        new ContactByMiddleNameComparer()
                    });
            }
        }
    }
}
