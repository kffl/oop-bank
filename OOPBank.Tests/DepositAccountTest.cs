using System;
using Moq;
using NUnit.Framework;
using OOPBank.Classes;

namespace OOPBank.Tests
{
    [TestFixture]
    class DepositAccountTest
    {
        private readonly DepositAccount DepositAccount1;
        private readonly Customer SomeCustomer;
        private Mock<Account> Account1;
        private Mock<Account> Account2;
        private Mock<Operation> mockOp;

        public DepositAccountTest()
        {
            SomeCustomer = new Customer("", "");
            DepositAccount1 = new DepositAccount(SomeCustomer, "A01", new Money(100),
                new Money(100), 1);
        }

        [SetUp]
        public void Setup()
        {
            Account1 = new Mock<Account>();
            Account1.Setup(t => t.accountNumber).Returns("1");

            Account2 = new Mock<Account>();
            Account2.Setup(t => t.accountNumber).Returns("2");

            mockOp = new Mock<Operation>();
            mockOp.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 04, 01));
            mockOp.Setup(t => t.FromAccount).Returns(Account1.Object);
            mockOp.Setup(t => t.ToAccount).Returns(Account2.Object);
            mockOp.Setup(t => t.Money).Returns(new Money(100));
        }

        [Test]
        public void ConstructorExceptionTest()
        {
            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Deposit duration has to be longer than 0 days."), () =>
                new DepositAccount(SomeCustomer, "A01", new Money(),
                    new Money(0, 1), 0));
            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Deposit amount has to be greater than 0."), () => new DepositAccount(SomeCustomer,
                "A01", new Money(),
                new Money(), 1));
        }

        [Test]
        public void DepositOperationsTest()
        {
            DepositAccount1.bookIncomingOperation(mockOp.Object);
            Assert.AreEqual(200, DepositAccount1.getBalance().asDouble);
            DepositAccount1.bookOutgoingOperation(mockOp.Object);
            DepositAccount1.bookOutgoingOperation(mockOp.Object);
            Assert.AreEqual(0, DepositAccount1.getBalance().asDouble);
            DepositAccount1.bookOutgoingOperation(mockOp.Object);
            Assert.AreEqual(0, DepositAccount1.getBalance().asDouble);
        }

        [Test]
        public void AccountTest()
        {
            Assert.IsTrue(DepositAccount1.hasSufficientBalance(new Money(200)));
            Assert.IsFalse(DepositAccount1.hasSufficientBalance(new Money(200, 1)));
        }
    }
}