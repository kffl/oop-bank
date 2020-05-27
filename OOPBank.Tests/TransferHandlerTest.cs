using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using OOPBank.OperationExecuting;
using OOPBank.Operations;

namespace OOPBank.Tests
{
    [TestFixture]
    public class TransferHandlerTest
    {

        [TestCase(5, 0, 1, "This account does not belong to our bank.")]
        [TestCase(-1, 1, 1, "Amount has to be greater than 0.")]
        [TestCase(15, 1, 1, "Insufficient account balance.")]
        [TestCase(5, 1, 0, "Transfer has to be between different accounts.")]
        [TestCase(5, 1, 2, "Recipient's account not found")]
        public void ExecuteExceptionsTest(long amountParam, int accountListIdx, int toAccountIdx, string errorMsg)
        {
            var transfer = new Mock<Transfer>();
            var bank = new Mock<Bank>();
            var accountList = GetAccountList(accountListIdx);

            bank.Setup(t => t.getAccounts()).Returns(accountList);
            bank.Setup(t => t.accountPrefix).Returns("B");
            transfer.Setup(t => t.Money).Returns(new Money(amountParam));
            transfer.SetupGet(t => t.Bank).Returns(bank.Object);
            transfer.Setup(t => t.FromAccount).Returns(accountList.ElementAtOrDefault(0));
            transfer.Setup(t => t.ToAccountNumber).Returns(GetAccountList(2)[toAccountIdx].AccountNumber.ToString);

            var transferHandler = new TransferHandler(null);

            Assert.Throws(Is.TypeOf<Exception>()
                    .And.Message.EqualTo(errorMsg),
                () => transferHandler.handle(transfer.Object));
        }

        private static List<LocalAccount> GetAccountList(int index)
        {
            switch (index)
            {
                case 0:
                    return new List<LocalAccount>();
                case 1:
                    return new List<LocalAccount>
                    {
                        new LocalAccount(null, "B1234", new Money(10)),
                        new LocalAccount(null, "B5667", new Money(10))
                    };
                case 2:
                    return new List<LocalAccount>
                    {
                        new LocalAccount(null, "B1234", new Money(10)),
                        new LocalAccount(null, "B5667", new Money(10)),
                        new LocalAccount(null, "B5151", new Money(10)),
                    };
                default:
                    return null;
            }
        }
    }
}