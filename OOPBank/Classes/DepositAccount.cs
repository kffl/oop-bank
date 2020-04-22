using System;

namespace OOPBank
{
    public class DepositAccount : LocalAccount
    {
        private readonly int daysToClose;
        private int daysPassed;
        private Money depositAmount;
        private int depositsWithdraws;
        private Money earnedMoney = new Money();


        public DepositAccount(Customer owner, string number, Money startingBalance, Money depositAmount, int duration) :
            base(owner, number, startingBalance)
        {
            if (duration <= 0) throw new Exception("Deposit duration has to be longer than 0 days.");
            daysToClose = duration;
            this.depositAmount = new Money(depositAmount.dollars, depositAmount.cents);
        }

        private bool isClosed => daysPassed >= daysToClose;


        public override bool hasSufficientBalance(Money money)
        {
            return depositAmount + balance - money >= 0;
        }

        public override void bookIncomingOperation(Operation operation)
        {
            IncomingOperations.Add(operation);
            balance += operation.Money;
        }

        public override void bookOutgoingOperation(Operation operation)
        {
            OutgoingOperations.Add(operation);
            if (balance - operation.Money >= 0)
            {
                balance -= operation.Money;
                return;
            }

            var remainingMoney = operation.Money - balance;
            balance = new Money();
            depositAmount -= remainingMoney;
            depositsWithdraws++;
        }

        public override void handleNewDay()
        {
            if (daysToClose == daysPassed)
            {
                earnedMoney = depositAmount *
                              ((InterestRate + interestRate.depositInterestConstant) /
                               (depositsWithdraws == 0 ? 1 : depositsWithdraws));
                balance += depositAmount + earnedMoney;
                depositAmount = new Money();
            }

            if (!isClosed) daysPassed++;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Deposit account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Deposit amount: " + depositAmount.asDouble);
            Console.WriteLine("Earned money: " + earnedMoney.asDouble);
            Console.WriteLine("Days to close: " + (daysToClose - daysPassed));
            Console.WriteLine("###############################");
        }
    }
}