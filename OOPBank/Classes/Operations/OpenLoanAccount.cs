using System;

namespace OOPBank.Classes.Operations
{
    internal class OpenLoanAccount : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        private readonly Money startingLoan;

        public OpenLoanAccount(Customer customer, Bank bank, Money startingBalance = null,
            Money startingLoan = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.startingLoan = startingLoan;
        }

        public void execute()
        {
            if (startingLoan <= 0) throw new Exception("Loan amount has to be greater than 0.");
            var newAccount = new LoanAccount(
                customer,
                bank.generateAccountNumber(),
                Money ?? new Money(),
                startingLoan ?? new Money()
            );
            bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(this);
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
