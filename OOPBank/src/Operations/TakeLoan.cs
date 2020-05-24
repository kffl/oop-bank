using System;

namespace OOPBank.Operations
{
    internal class TakeLoan : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public readonly LoanAccount loanAccount;

        public TakeLoan(Customer customer, Bank bank, LoanAccount loanAccount, Money amount)
        {
            this.bank = bank;
            this.customer = customer;
            this.loanAccount = loanAccount;
            Money = amount;
            Status = OperationStatus.Disposed;
            loanAccount.OtherOperations.Add(this);
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Take Loan ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
