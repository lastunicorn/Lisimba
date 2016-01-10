using DustInTheWind.Lisimba.Cmd.Common;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Cmd
{
    [TestFixture]
    public class CommandParserTests
    {
        [Test]
        public void parse_empty_string_results_no_items()
        {
            CommandParser commandParser = new CommandParser("");

            Assert.That(commandParser.Items, Is.Empty);
        }

        [Test]
        public void parse_one_char_results_one_item()
        {
            CommandParser commandParser = new CommandParser("a");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("a"));
        }

        [Test]
        public void parse_one_word_results_one_item()
        {
            CommandParser commandParser = new CommandParser("abc");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_space_at_the_begining_results_one_item()
        {
            CommandParser commandParser = new CommandParser(" abc");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_two_spaces_at_the_begining_results_one_item()
        {
            CommandParser commandParser = new CommandParser("  abc");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_space_at_the_end_results_one_item()
        {
            CommandParser commandParser = new CommandParser("abc ");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_with_two_spaces_at_the_end_results_one_item()
        {
            CommandParser commandParser = new CommandParser("abc  ");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_two_word_with_one_space_between_them_results_two_items()
        {
            CommandParser commandParser = new CommandParser("abc xyz");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
            Assert.That(commandParser.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_between_them_results_two_items()
        {
            CommandParser commandParser = new CommandParser("abc  xyz");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
            Assert.That(commandParser.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_before_and_between_them_results_two_items()
        {
            CommandParser commandParser = new CommandParser("  abc  xyz");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
            Assert.That(commandParser.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_two_word_with_two_spaces_before_between_and_after_them_results_two_items()
        {
            CommandParser commandParser = new CommandParser("  abc  xyz  ");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
            Assert.That(commandParser.Items[1], Is.EqualTo("xyz"));
        }

        [Test]
        public void parse_one_word_enclosed_in_quotas_results_one_item()
        {
            CommandParser commandParser = new CommandParser("\"abc\"");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_one_word_enclosed_in_quotas_sorounded_by_spaces_results_one_item()
        {
            CommandParser commandParser = new CommandParser(" \"abc\" ");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc"));
        }

        [Test]
        public void parse_two_words_enclosed_in_quotas_sorounded_by_spaces_results_one_item()
        {
            CommandParser commandParser = new CommandParser(" \"abc xyz\" ");

            Assert.That(commandParser.Items.Length, Is.EqualTo(1));
            Assert.That(commandParser.Items[0], Is.EqualTo("abc xyz"));
        }
    }
}
