﻿using System;

namespace OOPBank.Classes.Operations
{
    public class Transfer : Operation
    {
        private readonly Bank bank;

        private readonly Customer customer;

        internal readonly string toAccountNumber;

        public Transfer(Customer customer, Bank bank, LocalAccount fromAccount, string toAccountNumber, Money amount)
        {
            this.bank = bank;
            this.customer = customer;
            FromAccount = fromAccount;
            this.toAccountNumber = toAccountNumber;
            Money = amount;
            FromAccount.OutgoingOperations.Add(this);
        }

        public long Ibpaid { get; set; }

        public override void Execute()
        {
            //check if account belongs to this bank
            if (!bank.getAccounts().Contains(FromAccount))
                throw new Exception("This account does not belong to our bank.");
            if (Money <= 0) throw new Exception("Amount has to be greater than 0.");
            if (FromAccount.AccountNumber == toAccountNumber)
                throw new Exception("Transfer has to be between different accounts.");
            if (!FromAccount.hasSufficientBalance(Money)) throw new Exception("Insufficient account balance.");

            if (toAccountNumber.StartsWith(bank.accountPrefix))
            {
                //it's an internal transfer
                var recipientsAccount = bank.getAccounts().Find(a => a.AccountNumber == toAccountNumber);
                if (recipientsAccount == null) throw new Exception("Recipient's account not found");

                FromAccount.decreaseBalance(Money);
                recipientsAccount.increaseBalance(Money);
                recipientsAccount.IncomingOperations.Add(this);
                status = OperationStatus.Completed;
            }
            else
            {
                //it's an external transfer
                status = OperationStatus.PendingCompletion;
                FromAccount.decreaseBalance(Money);
                FromAccount.OutgoingOperations.Add(this);
                bank.IBPA.performInterBankTransfer(this);
            }
        }
    }
}
