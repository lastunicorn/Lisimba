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

using DustInTheWind.ConsoleCommon.CommandModel;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.CommandLine
{
    [TestFixture]
    public class RealStringTests
    {
        [Test]
        public void test1()
        {
            CommandSplitter commandSplitter = new CommandSplitter(@"update name=""value""");

            Assert.That(commandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(commandSplitter.Items[0], Is.EqualTo("update"));
            Assert.That(commandSplitter.Items[1], Is.EqualTo(@"name=""value"""));
        }

        [Test]
        public void test2()
        {
            CommandSplitter commandSplitter = new CommandSplitter(@"update name=""val""ue""");

            Assert.That(commandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(commandSplitter.Items[0], Is.EqualTo("update"));
            Assert.That(commandSplitter.Items[1], Is.EqualTo(@"name=""val""ue"""));
        }

        [Test]
        public void test3()
        {
            CommandSplitter commandSplitter = new CommandSplitter(@"update name=""va""l""ue""");

            Assert.That(commandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(commandSplitter.Items[0], Is.EqualTo("update"));
            Assert.That(commandSplitter.Items[1], Is.EqualTo(@"name=""va""l""ue"""));
        }
    }
}
