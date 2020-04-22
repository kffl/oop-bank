using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using OOPBank.Classes;
using OOPBank.Classes.Filters;

namespace OOPBank.Tests
{
    [TestFixture]
    class ReportServiceTest
    {
        public List<Operation> Operations1;
        public List<Operation> Operations2;
        public Mock<Account> Account1;
        public Mock<Account> Account2;
        public Mock<Account> Account3;

        [SetUp]
        public void Setup()
        {
            SetupAccounts();
            SetupOperations();
        }

        private void SetupOperations()
        {
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
                mockOperations1.Add(mock.Object);

                mock = new Mock<Operation>();
                mock.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 03, i));
                mock.Setup(t => t.FromAccount).Returns(Account2.Object);
                mock.Setup(t => t.ToAccount).Returns(Account1.Object);
                mock.Setup(t => t.Money).Returns(new Money(i*2));
                mockOperations2.Add(mock.Object);
            }

            mock = new Mock<Operation>();
            mock.Setup(t => t.DateOfExecution).Returns(new DateTime(2020, 03, 15));
            mock.Setup(t => t.FromAccount).Returns(Account3.Object);
            mock.Setup(t => t.ToAccount).Returns(Account2.Object);
            mock.Setup(t => t.Money).Returns(new Money(11,25));
            mockOperations2.Add(mock.Object);

            Operations1 = mockOperations1;
            Operations2 = mockOperations2;

            Account1.Setup(t => t.IncomingOperations).Returns(Operations1);
            Account1.Setup(t => t.OutgoingOperations).Returns(Operations2);

            Account2.Setup(t => t.IncomingOperations).Returns(Operations2);
            Account2.Setup(t => t.OutgoingOperations).Returns(Operations1);

            Account3.Setup(t => t.IncomingOperations).Returns(new List<Operation>());
            Account3.Setup(t => t.OutgoingOperations).Returns(new List<Operation>());
        }


        private void SetupAccounts()
        {
            Account1 = new Mock<Account>();
            Account1.Setup(t => t.accountNumber).Returns("1");

            Account2 = new Mock<Account>();
            Account2.Setup(t => t.accountNumber).Returns("2");

            Account3 = new Mock<Account>();
            Account3.Setup(t => t.accountNumber).Returns("3");
        }

        [Test]
        public void TestGenerateReportFilter()
        {
            var filterMock = new Mock<Filter>();
            filterMock.Setup(f => f.OperationsLimit).Returns(5);
            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.Incoming);

            var report = ReportService.generateReport(Account1.Object, filterMock.Object);

            var expectedOperations = new List<Operation>() { Operations1[29], Operations1[28], Operations1[27], Operations1[26], Operations1[25] };
            Assert.AreEqual(expectedOperations, report.Operations);
        }

        [Test]
        public void TestGenerateReportAmountFilter()
        {
            var filterMock = new Mock<AmountFilter>();
            filterMock.Setup(f => f.OperationsLimit).Returns(3);
            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.Incoming);
            filterMock.Setup(f => f.AmountFrom).Returns(new Money(5));
            filterMock.Setup(f => f.AmountTo).Returns(new Money(25));

            var report = ReportService.generateReport(Account2.Object, filterMock.Object);

            var expectedOperations = new List<Operation>() { Operations2[30], Operations2[11], Operations2[10] };
            Assert.AreEqual( expectedOperations, report.Operations);
        }

        [Test]
        public void TestGenerateReportAccountFilter()
        {
            var filterMock = new Mock<AccountFilter>();
            filterMock.Setup(f => f.OperationsLimit).Returns(3);
            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.Incoming);
            filterMock.Setup(f => f.Account).Returns(Account1.Object);

            var report = ReportService.generateReport(Account2.Object, filterMock.Object);
            var expectedOperations = new List<Operation>();
            Assert.AreEqual(expectedOperations, report.Operations);


            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.Outgoing);
            report = ReportService.generateReport(Account2.Object, filterMock.Object);
            Assert.AreEqual(expectedOperations, report.Operations);


            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.Incoming);
            filterMock.Setup(f => f.Account).Returns(Account3.Object);
            report = ReportService.generateReport(Account2.Object, filterMock.Object);
            expectedOperations = new List<Operation>() { Operations2[30] };
            Assert.AreEqual(expectedOperations, report.Operations);
        }

        [Test]
        public void TestGenerateReportDateFilter()
        {
            var filterMock = new Mock<DateFilter>();
            filterMock.Setup(f => f.OperationsLimit).Returns(10);
            filterMock.Setup(f => f.Type).Returns(Filter.OperationType.All);
            filterMock.Setup(f => f.DateFrom).Returns(new DateTime(2020, 3, 25));
            filterMock.Setup(f => f.DateTo).Returns(new DateTime(2020, 4, 4));

            var report = ReportService.generateReport(Account2.Object, filterMock.Object);
           
            var expectedOperations = new List<Operation>()
            {
                Operations1[3], Operations1[2], Operations1[1], Operations1[0],
                Operations2[29], Operations2[28], Operations2[27], Operations2[26], Operations2[25],
                Operations2[24]
            };
            Assert.AreEqual(expectedOperations, report.Operations);

        }
    }
}
