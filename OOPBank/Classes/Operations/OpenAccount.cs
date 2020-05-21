﻿using System;

namespace OOPBank.Classes.Operations
{
    internal class OpenAccount : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        public OpenAccount(Customer customer, Bank bank, Money startingBalance = null)
        {
            this.bank = bank;
            this.customer = customer;
            Money = startingBalance;
        }

        public override void Execute()
        {
            var newAccount = new LocalAccount(
                customer,
                bank.generateAccountNumber(),
                Money ?? new Money()
            );
            bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(this);
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
