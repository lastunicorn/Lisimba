using Lisimba.Cmd;
using Lisimba.Cmd.CommandSystem;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Cmd
{
    [TestFixture]
    public class RealStringTests
    {
        [Test]
        public void test1()
        {
            CommandParser commandParser = new CommandParser(@"update name=""value""");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("update"));
            Assert.That(commandParser.Items[1], Is.EqualTo(@"name=""value"""));
        }

        [Test]
        public void test2()
        {
            CommandParser commandParser = new CommandParser(@"update name=""val""ue""");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("update"));
            Assert.That(commandParser.Items[1], Is.EqualTo(@"name=""val""ue"""));
        }

        [Test]
        public void test3()
        {
            CommandParser commandParser = new CommandParser(@"update name=""va""l""ue""");

            Assert.That(commandParser.Items.Length, Is.EqualTo(2));
            Assert.That(commandParser.Items[0], Is.EqualTo("update"));
            Assert.That(commandParser.Items[1], Is.EqualTo(@"name=""va""l""ue"""));
        }
    }
}
