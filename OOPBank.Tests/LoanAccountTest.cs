using System;
using Moq;
using NUnit.Framework;
using OOPBank.Classes;

namespace OOPBank.Tests
{
    [TestFixture]
    class LoanAccountTest
    {
        private readonly LoanAccount LoanAccount1;
        private readonly Customer SomeCustomer;

        public LoanAccountTest()
        {
            SomeCustomer = new Customer("", "");
            LoanAccount1 = new LoanAccount(SomeCustomer, "A01", new Money(100), new Money(100));
        }

        [Test]
        public void ConstructorExceptionTest()
        {
            Assert.Throws(Is.TypeOf<Exception>()
                    .And.Message.EqualTo("Loan amount has to be greater than 0."),
                () => new LoanAccount(SomeCustomer, "A01", new Money(100), new Money()));
        }

        [Test]
        public void LoanOperationsTest()
        {
            LoanAccount1.chargeInstallment(new Money(100));
            Assert.AreEqual(100, LoanAccount1.balance.asDouble);
            Assert.AreEqual(0, LoanAccount1.loanAmount.asDouble);
        }

        [Test]
        public void AccountTest()
        {
            Assert.IsTrue(LoanAccount1.tooMuchTransfer(new Money(100, 1)));
            Assert.IsFalse(LoanAccount1.tooMuchTransfer(new Money(100)));
        }
    }
}