using System;
using OOPBank.InterestRate;

namespace OOPBank.Classes
{
    public class LocalAccount : Account, ILocalAccount
    {
        protected InterestRate.InterestRate interestRate;
        protected readonly Customer owner;


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

        public virtual void bookOutgoingOperation(Operation operation)
        {
            OutgoingOperations.Add(operation);
            balance = balance - operation.Money;
        }

        public virtual void bookIncomingOperation(Operation operation)
        {
            IncomingOperations.Add(operation);
            balance = balance + operation.Money;
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
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("#########################");
        }
        public void displayHistory()
        {
            Console.WriteLine("###  Account history  ###");
            Console.WriteLine("####Incoming history ####");
            foreach (var operation in IncomingOperations) operation.displayOperationDetails();
            Console.WriteLine("####Outgoing history ####");
            foreach (var operation in OutgoingOperations) operation.displayOperationDetails();
            Console.WriteLine("#########################");
        }
    }
}