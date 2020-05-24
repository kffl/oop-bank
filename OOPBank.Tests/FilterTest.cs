using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using OOPBank.Filters;
using static OOPBank.Filters.Filter;

namespace OOPBank.Tests
{
    [TestFixture]
    class FilterTest
    {
        public Mock<Account> Account1;
        public Mock<Account> Account2;
        public List<Operation> Operations1;
        public List<Operation> Operations2;
        [SetUp]
        public void Setup()
        {
            Account1 = new Mock<Account>();
            Account1.Setup(t => t.AccountNumber).Returns("1");

            Account2 = new Mock<Account>();
            Account2.Setup(t => t.AccountNumber).Returns("2");

            var mockOperations1 = new List<Operation>();
            var mockOperations2 = new List<Operation>();
            Mock<Operation> mock;

            for (var i = 1; i < 31; i++)
            {
                mock = new Mock<Operation>();
                mock.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 04, i));
                mock.Setup(t => t.FromAccount).Returns(Account1.Object);
                mock.Setup(t => t.ToAccount).Returns(Account2.Object);
                mock.Setup(t => t.Money).Returns(new Money(i));
                mock.Setup(t => t.acceptFilter(It.IsAny<IFilterVisitor>())).Returns(mock.Object);
                mockOperations1.Add(mock.Object);

                mock = new Mock<Operation>();
                mock.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 03, i));
                mock.Setup(t => t.FromAccount).Returns(Account2.Object);
                mock.Setup(t => t.ToAccount).Returns(Account1.Object);
                mock.Setup(t => t.acceptFilter(It.IsAny<IFilterVisitor>())).Returns((IFilterableElement)null);
                mock.Setup(t => t.Money).Returns(new Money(i * 2));
                mockOperations2.Add(mock.Object);
            }

            mock = new Mock<Operation>();
            mock.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 03, 15));
            mock.Setup(t => t.FromAccount).Returns(Account1.Object);
            mock.Setup(t => t.ToAccount).Returns(Account2.Object);
            mock.Setup(t => t.Money).Returns(new Money(11, 25));
            mockOperations2.Add(mock.Object);

            Operations1 = mockOperations1;
            Operations2 = mockOperations2;
            Account1.Setup(t => t.IncomingOperations).Returns(Operations1);
            Account1.Setup(t => t.OutgoingOperations).Returns(Operations2);

            Account2.Setup(t => t.IncomingOperations).Returns(Operations2);
            Account2.Setup(t => t.OutgoingOperations).Returns(Operations1);
        }

        [Test]
        public void AmountFilterTest()
        {
            var amountFilter = new AmountFilter(10, OperationType.All, new Money(10), new Money(20));

            Assert.Null(amountFilter.VisitOperation(Operations1[1]));
            Assert.NotNull(amountFilter.VisitOperation(Operations1[19]));
            Assert.NotNull(amountFilter.VisitOperation(Operations1[10]));
            Assert.Null(amountFilter.VisitOperation(Operations1[24]));
        }

        [Test]
        public void DateFilterTest()
        {
            var dateFilter = new DateFilter(10, OperationType.All, new DateTime(2020, 3, 25), new DateTime(2020, 4, 4));

            Assert.NotNull(dateFilter.VisitOperation(Operations1[1]));
            Assert.Null(dateFilter.VisitOperation(Operations1[19]));
            Assert.Null(dateFilter.VisitOperation(Operations1[10]));
            Assert.NotNull(dateFilter.VisitOperation(Operations1[3]));
        }

        [Test]
        public void AccountFilterTest()
        {
            var accountFilterOutgoing = new AccountFilter(10, OperationType.Outgoing, Account1.Object);

            Assert.Null(accountFilterOutgoing.VisitOperation(Operations1[10]));
            Assert.NotNull(accountFilterOutgoing.VisitOperation(Operations2[10]));

            var accountFilterAllowAll = new AccountFilter(10, OperationType.All, Account1.Object);

            Assert.NotNull(accountFilterAllowAll.VisitOperation(Operations1[10]));
            Assert.NotNull(accountFilterAllowAll.VisitOperation(Operations2[10]));

            var accountFilterIncoming = new AccountFilter(100, OperationType.Incoming, Account2.Object);

            Assert.Null(accountFilterOutgoing.VisitOperation(Operations1[10]));
            Assert.NotNull(accountFilterOutgoing.VisitOperation(Operations2[10]));
        }
    }
}
