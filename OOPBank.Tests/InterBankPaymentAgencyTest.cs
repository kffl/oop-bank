using System;
using Moq;
using OOPBank.Classes;
using NUnit.Framework;

namespace OOPBank.Tests
{
    [TestFixture]
    public class InterBankPaymentAgencyTest
    {
        private InterBankPaymentAgency IBPA;
        private Mock<IBank> Bank1;
        private Mock<IBank> Bank2;

        [SetUp]
        public void SetUp()
        {
            Bank1 = new Mock<IBank>();
            Bank1.SetupGet(t => t.accountPrefix).Returns("B1");
            Bank1.Setup(t => t.handleConfirmation(1));
            Bank2 = new Mock<IBank>();
            Bank2.SetupGet(t => t.accountPrefix).Returns("B2");
            Bank2.Setup(t => t.handleIncomingPayment("B10000000001", "B20000000001", It.IsAny<Money>())).Returns(true);
            Bank2.Setup(t => t.handleIncomingPayment("B10000000001", "B20000000002", It.IsAny<Money>())).Returns(false);

            IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.registerBank(Bank1.Object);
            IBPA.registerBank(Bank2.Object);
        }

        [Test]
        public void InterBankPaymentAgencySingletonTest()
        {
            var IBPA1 = InterBankPaymentAgency.getInterBankPaymentAgency();
            var IBPA2 = InterBankPaymentAgency.getInterBankPaymentAgency();

            Assert.AreSame(IBPA1, IBPA2);
        }

        [Test]
        public void PerformInterBankTransferSuccessfulTest()
        {
            var amount = new Money(100);
            IBPA.performInterBankTransfer("B10000000001", Bank1.Object, "B20000000001", amount);
            IBPA.processQueuedPayments();
            Bank1.Verify(t => t.handleConfirmation(It.IsAny<long>()), Times.Once());
        }

        [Test]
        public void PerformInterBankTransferInvalidAccountTest()
        {
            var amount = new Money(50);
            IBPA.performInterBankTransfer("B10000000001", Bank1.Object, "B20000000002", amount);
            IBPA.processQueuedPayments();
            Bank1.Verify(t => t.handlePaymentFailure(It.IsAny<long>()), Times.Once());
        }

        [Test]
        public void PerformInterBankTransferBankNotFoundTest()
        {
            var amount = new Money(200);
            Assert.Throws<Exception>(() => { IBPA.performInterBankTransfer("B10000000001", Bank1.Object, "XY0000000001", amount); }, "Recipients bank not found");
        }

        [Test]
        public void PerformInterBankTransferUniqueIdsTest()
        {
            var amount = new Money(800);
            var id1 = IBPA.performInterBankTransfer("B10000000001", Bank1.Object, "B20000000002", amount);
            var id2 = IBPA.performInterBankTransfer("B10000000001", Bank1.Object, "B20000000002", amount);
            Assert.AreNotEqual(id1, id2);
        }
    }
}