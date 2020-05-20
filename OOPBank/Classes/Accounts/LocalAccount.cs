using System;
using OOPBank.InterestRate;

namespace OOPBank.Classes
{
    public class LocalAccount : Account, ILocalAccount
    {
        protected InterestRate.InterestRate interestRate;
        public readonly Customer owner;


        public LocalAccount(Customer owner, string number, Money startingBalance) : base(number)
        {
            if (startingBalance < 0) throw new Exception("Starting balance must not be lower than 0.");
            this.owner = owner;
            balance = new Money(startingBalance.dollars, startingBalance.cents);
            interestRate = new InterestRatePoor(this, IncomingOperations, OutgoingOperations);
        }

        public Money balance { get; set; }
        public double InterestRate => interestRate.calculateInterest(state => { interestRate = state; });


        public virtual bool hasSufficientBalance(Money money)
        {
            return balance - money >= 0;
        }
        public virtual void decreaseBalance(Money money)
        {
            balance = balance - money;
        }

        public virtual void increaseBalance(Money money)
        {
            balance = balance + money;
        }

        public void rollbackOutgoingOperation(Operation operation)
        {
            balance = balance + operation.Money;
        }


        public virtual void handleNewDay()
        {
        }

        public void withdrawMoney(Money amount)
        {
            if (!hasSufficientBalance(amount)) throw new Exception("Insufficient account balance.");
            balance -= amount;
        }

        public virtual void displayAccountDetails()
        {
            Console.WriteLine("###  Account details  ###");
            Console.WriteLine("Number: " + AccountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("#########################");
        }
        public void displayHistory()
        {
            Console.WriteLine("###  Account history  ###");
            Console.WriteLine("####Incoming history ####" + IncomingOperations.Count);
            foreach (var operation in IncomingOperations) operation.displayOperationDetails();
            Console.WriteLine("####Outgoing history ####" + OutgoingOperations.Count);
            foreach (var operation in OutgoingOperations) operation.displayOperationDetails();
            Console.WriteLine("####Other history ####" + OutgoingOperations.Count);
            foreach (var operation in OtherOperations) operation.displayOperationDetails();
            Console.WriteLine("#########################");
        }
    }
}
