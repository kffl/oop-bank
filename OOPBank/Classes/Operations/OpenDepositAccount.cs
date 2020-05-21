using System;

namespace OOPBank.Classes.Operations
{
    internal class OpenDepositAccount : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        private readonly Money depositAmount;

        private readonly int durationDays;

        public OpenDepositAccount(Customer customer, Bank bank, Money depositAmount, Money startingBalance = null,
            int durationDays = 30)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
            this.depositAmount = depositAmount;
            this.durationDays = durationDays;
        }

        public override void Execute()
        {
            var newAccount = new DepositAccount(
                customer,
                bank.generateAccountNumber(),
                Money ?? new Money(),
                depositAmount,
                durationDays
            );
            bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(this);
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
