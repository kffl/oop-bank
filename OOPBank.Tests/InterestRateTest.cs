using System;
using Moq;
using NUnit.Framework;
using OOPBank;

namespace OOPBank.Tests
{
    internal class InterestRateTest
    {
        private LocalAccount account;
        private Mock<Operation> operation;

        [SetUp]
        public void Setup()
        {
            account = new LocalAccount(new Customer("", ""), "A01", new Money(99999));
            operation = new Mock<Operation>();
            operation.SetupGet(o => o.Money).Returns(new Money(1));
        }


        [Test, Order(1)]
        public void PoorToRichTest()
        {
            var rateBeforeOperation = account.InterestRate;
            account.IncomingOperations.Add(operation.Object);
            Assert.LessOrEqual(rateBeforeOperation, account.InterestRate);
        }

        [Test, Order(2)]
        public void RichToPoorTest()
        {
            var rateBeforeOperation = account.InterestRate;
            account.OutgoingOperations.Add(operation.Object);
            Assert.GreaterOrEqual(rateBeforeOperation, account.InterestRate);
        }
    }
}