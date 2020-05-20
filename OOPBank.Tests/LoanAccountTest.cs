using System;
using Moq;
using NUnit.Framework;
using OOPBank.Classes;

namespace OOPBank.Tests
{
    [TestFixture]
    class LoanAccountTest
    {
        private Mock<Account> Account1;
        private Mock<Account> Account2;
        private readonly LoanAccount LoanAccount1;
        private Mock<Operation> mockOp;
        private readonly Customer SomeCustomer;

        public LoanAccountTest()
        {
            SomeCustomer = new Customer("", "");
            LoanAccount1 = new LoanAccount(SomeCustomer, "A01", new Money(100), new Money(100));
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
                    .And.Message.EqualTo("Loan amount has to be greater than 0."),
                () => new LoanAccount(SomeCustomer, "A01", new Money(100), new Money()));
        }

        [Test]
        public void LoanOperationsTest()
        {
            LoanAccount1.bookInstallmentOperation(mockOp.Object);
            Assert.AreEqual(0, LoanAccount1.balance.asDouble);
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