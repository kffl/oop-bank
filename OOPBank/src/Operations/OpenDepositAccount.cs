using System;

namespace OOPBank.Operations
{
    internal class OpenDepositAccount : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public readonly Money depositAmount;

        public readonly int durationDays;

        public OpenDepositAccount(Customer customer, Bank bank, Money depositAmount, Money startingBalance = null,
            int durationDays = 30)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.depositAmount = depositAmount;
            this.durationDays = durationDays;
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Open Deposit Account ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("Deposit: {0}", depositAmount.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
