using System;

namespace OOPBank.Classes
{
    public class DebitAccount : LocalAccount
    {
        private readonly Money debitLimit;

        public DebitAccount(Customer owner, string number, Money startingBalance, Money debitLimitation) : base(
            owner, number, startingBalance)
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
            Console.WriteLine("Number: " + AccountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Debt limitation: " + debitLimit.asDouble);
            Console.WriteLine("###############################");
        }
    }
}
