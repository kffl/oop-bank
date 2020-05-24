using System;

namespace OOPBank.Operations
{
    internal class OpenLoanAccount : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public readonly Money startingLoan;

        public OpenLoanAccount(Customer customer, Bank bank, Money startingBalance = null,
            Money startingLoan = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.startingLoan = startingLoan;
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Open Loan Account ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("Loan: {0}", startingLoan.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
