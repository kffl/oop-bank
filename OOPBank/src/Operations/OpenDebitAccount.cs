using System;

namespace OOPBank.Operations
{
    internal class OpenDebitAccount : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public readonly Money startingDebit;

        public OpenDebitAccount(Customer customer, Bank bank, Money startingBalance = null,
            Money startingDebit = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.startingDebit = startingDebit;
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Open Deposit Account ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("Deposit: {0}", startingDebit.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
