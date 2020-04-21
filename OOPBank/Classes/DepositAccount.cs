using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class DepositAccount : LocalAccount
    {
        public DateTime date { get; set; }
        public bool hasFirstIncoming { get; set; }
        private bool isCalculated { get; set; }
        private TimeSpan duration { get; }

        public DepositAccount(Customer owner, string number, Money startingBalance, TimeSpan duration) : base(owner, number, startingBalance)
        {
            this.duration = duration;
            date = DateTime.Today.Add(duration);
            isCalculated = false;
            hasFirstIncoming = false;
        }

        public override void bookIncomingOperation(Operation operation)
        {
            incomingOperations.Add(operation);
            balance = balance + operation.money;
            hasFirstIncoming = true;
        }

        public override void bookOutgoingOperation(Operation operation)
        {
            outgoingOperations.Add(operation);
            if (DateTime.Today >= date && isCalculated)
            {
                balance = balance; //Add interest rate
            }
            isCalculated = true;
            balance = balance - operation.money;
        }

        public override void displayAccountDetails()
        {
            if (DateTime.Today >= date && isCalculated)
            {
                balance = balance; //Add interest rate
                isCalculated = true;
            }
            Console.WriteLine("###  Deposit account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Expected end date: " + date.ToString("yyyyMMdd"));
            Console.WriteLine("###############################");
        }
    }
}