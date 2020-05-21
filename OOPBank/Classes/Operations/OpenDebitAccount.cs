using System;

namespace OOPBank.Classes.Operations
{
    internal class OpenDebitAccount : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        private readonly Money startingDebit;

        public OpenDebitAccount(Customer customer, Bank bank, Money startingBalance = null,
            Money startingDebit = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.startingDebit = startingDebit;
        }

        public override void Execute()
        {
            if (startingDebit <= 0) throw new Exception("Debt limitation has to be greater than 0.");
            var newAccount = new DebitAccount(
                customer,
                bank.generateAccountNumber(),
                Money ?? new Money(),
                startingDebit ?? new Money()
            );
            bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(this);
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
