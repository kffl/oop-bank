using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class DebitAccount : LocalAccount
    {
        protected Money debitLimit { get; set; }

        public DebitAccount(Customer owner, string number, Money startingBalance, Money debitLimitation) : base(owner, number, startingBalance)
        {
            debitLimit = new Money(debitLimitation.dollars, debitLimitation.cents);
        }

        public override bool hasSufficientBalance(Money money)
        {
            return debitLimit + balance - money >= 0;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Debit account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Debt limitation: " + debitLimit.asDouble);
            Console.WriteLine("###############################");
        }
    }
}