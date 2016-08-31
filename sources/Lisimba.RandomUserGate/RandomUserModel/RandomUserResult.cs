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

namespace DustInTheWind.Lisimba.RandomUserGate.RandomUserModel
{
    internal class RandomUserResult
    {
        public string Gender { get; set; }
        public RandomUserName Name { get; set; }
        public RandomUserLocation Location { get; set; }
        public string Email { get; set; }
        public object Login { get; set; }
        public string Dob { get; set; }
        public string Registered { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public object Id { get; set; }
        public RandomUserPicture Picture { get; set; }
        public string Nat { get; set; }
    }
}