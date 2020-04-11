using NUnit.Framework;
using OOPBank;

namespace OOPBank.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(Program.AddInts(5, 5), 10);
        }
    }
}