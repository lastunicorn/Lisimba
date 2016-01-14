using DustInTheWind.ConsoleCommon;
using DustInTheWind.ConsoleCommon.CommandModel;
using NUnit.Framework;

namespace DustInTheWind.Lisimba.Tests.Cmd
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
