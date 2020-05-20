using System;
using NUnit.Framework;
using OOPBank.Classes;

namespace OOPBank.Tests
{
    internal class OverdraftAccountTest
    {
        private ILocalAccount account;

        [SetUp]
        public void Setup()
        {
            account = new LocalAccount(new Customer("", ""), "A01", new Money(1000));
        }


        [Test]
        public void InsufficientBalanceWithdrawTest()
        {
            Assert.DoesNotThrow(() => { account.withdrawMoney(new Money(1000)); });
            Assert.Throws<Exception>(() => { account.withdrawMoney(new Money(1)); });
        }

        [TestCase(1000)]
        [TestCase(1001)]
        [TestCase(999999)]
        public void OverdraftWithdrawTest(int withdrawnAmount)
        {
            Assert.DoesNotThrow(() => { new OverdraftLocalAccountDecorator(account).withdrawMoney(new Money(withdrawnAmount)); });
        }
    }
}