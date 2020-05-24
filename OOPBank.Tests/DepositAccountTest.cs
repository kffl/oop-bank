using System;
using Moq;
using NUnit.Framework;

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
            DepositAccount1.increaseBalance(new Money(100));
            Assert.AreEqual(200, DepositAccount1.balance.asDouble);
            Assert.AreEqual(100, DepositAccount1.depositAmount.asDouble);
            DepositAccount1.decreaseBalance(new Money(200));
            Assert.AreEqual(0, DepositAccount1.balance.asDouble);
            Assert.AreEqual(100, DepositAccount1.depositAmount.asDouble);
            DepositAccount1.decreaseBalance(new Money(100));
            Assert.AreEqual(0, DepositAccount1.balance.asDouble);
            Assert.AreEqual(0, DepositAccount1.depositAmount.asDouble);
        }

        [Test]
        public void AccountTest()
        {
            Assert.IsTrue(DepositAccount1.hasSufficientBalance(new Money(200)));
            Assert.IsFalse(DepositAccount1.hasSufficientBalance(new Money(200, 1)));
        }
    }
}