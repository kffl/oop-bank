using System;
using NUnit.Framework;
using OOPBank.Classes;

namespace OOPBank.Tests
{
    [TestFixture]
    class DebitAccountTest
    {
        public Customer SomeCustomer;

        public DebitAccountTest()
        {
            SomeCustomer = new Customer("", "");
        }

        [TestCase(0, -100)]
        public void ConstructorExceptionTest(long dollars, long cents)
        {
            Assert.Throws(Is.TypeOf<Exception>(), () =>
                new DebitAccount(SomeCustomer, "A01", new Money(),
                    new Money(dollars, cents)));
        }

        [TestCase(0, 0)]
        public void DebtLimitTest(long dollars, long cents)
        {
            Assert.Throws(Is.TypeOf<Exception>()
                .And.Message.EqualTo("Debt limitation has to be greater than 0."), () => new DebitAccount(SomeCustomer,
                "A01", new Money(),
                new Money(dollars, cents)));
        }

        [TestCase(1, 1)]
        public void AccountTest(long dollars, long cents)
        {
            var DebitAccount = new DebitAccount(SomeCustomer, "A01", new Money(),
                new Money(dollars, cents));
            Assert.IsTrue(DebitAccount.hasSufficientBalance(new Money(dollars, cents)));
            Assert.IsFalse(DebitAccount.hasSufficientBalance(new Money(dollars, cents + 1)));
        }
    }
}