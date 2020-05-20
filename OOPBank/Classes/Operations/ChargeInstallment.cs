using System;

namespace OOPBank.Classes.Operations
{
    internal class ChargeInstallment : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        private readonly LoanAccount loanAccount;

        public ChargeInstallment(Customer customer, Bank bank, LoanAccount loanAccount, Money amount)
        {
            this.bank = bank;
            this.customer = customer;
            this.loanAccount = loanAccount;
            Money = amount;
            status = OperationStatus.Disposed;
            loanAccount.OtherOperations.Add(this);
        }
        public override void Execute()
        {
            if (!bank.getAccounts().Contains(loanAccount))
                throw new Exception("This account does not belong to our bank.");
            if (Money <= 0) throw new Exception("Amount has to be greater than 0.");
            if (!loanAccount.hasSufficientBalance(Money)) throw new Exception("Insufficient account balance.");
            if (loanAccount.tooMuchTransfer(Money)) throw new Exception("Tried to transfer too much money.");

            loanAccount.balance -= Money;
            loanAccount.loanAmount -= Money;
            status = OperationStatus.Completed;
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
