using System;

namespace OOPBank.Classes.Operations
{
    internal class ChargeInstallment : Operation
    {
        public Bank Bank { get; }

        private readonly Customer customer;

        public LoanAccount LoanAccount { get; }

        public ChargeInstallment(Customer customer, Bank bank, LoanAccount loanAccount, Money amount)
        {
            Bank = bank;
            this.customer = customer;
            LoanAccount = loanAccount;
            Money = amount;
            Status = OperationStatus.Disposed;
            loanAccount.OtherOperations.Add(this);
        }

        public override void displayOperationDetails()
        {
            Console.WriteLine("### Operation: Charge Installment ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("#########################");
        }
    }
}
