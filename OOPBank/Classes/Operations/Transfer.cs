using System;

namespace OOPBank.Classes.Operations
{
    public class Transfer : Operation
    {
        public readonly Bank bank;

        public readonly Customer customer;

        public readonly string toAccountNumber;

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
    }
}
