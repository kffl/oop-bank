using System;

namespace OOPBank.Classes.Operations
{
    internal class OpenAccount : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public OpenAccount(Customer customer, Bank bank, Money startingBalance = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Open Account ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
