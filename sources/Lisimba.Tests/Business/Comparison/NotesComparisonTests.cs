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

using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.Lisimba.Business.Comparison;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Business.Comparison
{
    [TestFixture]
    public class NotesComparisonTests
    {
        [Test]
        public void both_notes_are_empty()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact();

            NotesComparison notesComparison = new NotesComparison(contactLeft, contactRight);

            Assert.That(notesComparison.Equality, Is.EqualTo(ItemEquality.BothEmpty));
        }

        [Test]
        public void left_contains_value_right_is_empty()
        {
            Contact contactLeft = new Contact { Notes = "note" };
            Contact contactRight = new Contact();

            NotesComparison notesComparison = new NotesComparison(contactLeft, contactRight);

            Assert.That(notesComparison.Equality, Is.EqualTo(ItemEquality.LeftExists));
        }

        [Test]
        public void left_is_empty_right_contains_value()
        {
            Contact contactLeft = new Contact();
            Contact contactRight = new Contact { Notes = "note" };
            
            NotesComparison notesComparison = new NotesComparison(contactLeft, contactRight);

            Assert.That(notesComparison.Equality, Is.EqualTo(ItemEquality.RightExists));
        }

        [Test]
        public void both_exists_but_different_values()
        {
            Contact contactLeft = new Contact { Notes = "note1" };
            Contact contactRight = new Contact { Notes = "note2" };

            NotesComparison notesComparison = new NotesComparison(contactLeft, contactRight);

            Assert.That(notesComparison.Equality, Is.EqualTo(ItemEquality.Different));
        }

        [Test]
        public void both_exists_and_have_same_value()
        {
            Contact contactLeft = new Contact { Notes = "note" };
            Contact contactRight = new Contact { Notes = "note" };

            NotesComparison notesComparison = new NotesComparison(contactLeft, contactRight);

            Assert.That(notesComparison.Equality, Is.EqualTo(ItemEquality.Equal));
        }
    }
}
