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

using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.CommandLine
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void parse_empty_string_results_no_items()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("");

            Assert.That(consoleCommandSplitter.Items, Is.Empty);
        }

        [Test]
        public void parse_one_char_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("a");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("a"));
        }

        [Test]
        public void parse_one_word_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("abc");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_space_at_the_begining_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter(" abc");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_two_spaces_at_the_begining_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("  abc");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_space_at_the_end_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("abc ");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_two_spaces_at_the_end_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("abc  ");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_two_word_with_one_space_between_them_results_two_items()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("abc xyz");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
            Assert.That(consoleCommandSplitter.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_between_them_results_two_items()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("abc  xyz");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
            Assert.That(consoleCommandSplitter.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_before_and_between_them_results_two_items()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("  abc  xyz");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
            Assert.That(consoleCommandSplitter.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_before_between_and_after_them_results_two_items()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("  abc  xyz  ");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(2));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
            Assert.That(consoleCommandSplitter.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_one_word_enclosed_in_quotas_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter("\"abc\"");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_enclosed_in_quotas_sorounded_by_spaces_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter(" \"abc\" ");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_two_words_enclosed_in_quotas_sorounded_by_spaces_results_one_item()
        {
            ConsoleCommandSplitter consoleCommandSplitter = new ConsoleCommandSplitter(" \"abc xyz\" ");

            Assert.That(consoleCommandSplitter.Items.Length, Is.EqualTo(1));
            Assert.That(consoleCommandSplitter.Items[0], Is.EqualTo("abc xyz"));
        }
    }
}
