using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class DebitAccount : LocalAccount
    {
        protected Money debitLimit { get; set; }

        public DebitAccount(Customer owner, string number, long startingBalance, long debitLimitation) : base(owner, number, startingBalance)
        {
            debitLimit = new Money(debitLimitation);
        }

        public override bool hasSufficientBalance(Money money)
        {
            return (debitLimit + balance - money).Amount >= 0;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Debit account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.Amount);
            Console.WriteLine("Debt limitation: " + debitLimit.Amount);
            Console.WriteLine("###############################");
        }
    }
}