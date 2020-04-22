using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class LocalAccount : Account
    {
        protected Customer owner;
        protected Money balance { get; set; }


		public LocalAccount(Customer owner, string number, Money startingBalance) : base(number)
		{
			this.owner = owner;
			balance = new Money(startingBalance.dollars, startingBalance.cents);
		}

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

        public void rollbackOutgoingOperaion(Operation operation)
        {
            balance = balance + operation.Money;
        }

		public Money getBalance()
		{
			return balance;
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