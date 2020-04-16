using System.Collections.Generic;
using System;

namespace OOPBank
{
    public class LocalAccount : Account
    {
        protected List<Operation> incomingOperations = new List<Operation>();
        protected List<Operation> outgoingOperations = new List<Operation>();
        protected Customer owner;
        protected Money balance { get; set; }

        public LocalAccount(Customer owner, string number, long startingBalance) : base(number)
        {
            this.owner = owner;
            balance = new Money(startingBalance);
        }

        public bool hasSufficientBalance(Money money)
        {
            return (balance - money).Amount >= 0;
        }

        public void bookOutgoingOperation(Operation operation)
        {
            outgoingOperations.Add(operation);
            balance = balance - operation.money;
        }

        public void bookIncomingOperation(Operation operation)
        {
            incomingOperations.Add(operation);
            balance = balance + operation.money;
        }

        public void rollbackOutgoingOperaion(Operation operation)
        {
            balance = balance + operation.money;
        }

        public Money getBalance()
        {
            return balance;
        }

        public void displayAccountDetails()
        {
            Console.WriteLine("###  Account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.Amount);
            Console.WriteLine("#########################");
        }

        public void displayHistory()
        {
            Console.WriteLine("###  Account history  ###");
            Console.WriteLine("####Incoming history ####");
            foreach (var operation in incomingOperations)
            {
                operation.displayOperationDetails();
            }
            Console.WriteLine("####Outgoing history ####");
            foreach (var operation in outgoingOperations)
            {
                operation.displayOperationDetails();
            }
            Console.WriteLine("#########################");
        }
    }
}