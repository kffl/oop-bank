
namespace OOPBank.Operations
{
    public class Transfer : Operation
    {
        public virtual Bank Bank { get; }

        public readonly Customer customer;

        public virtual string ToAccountNumber { get; }

        public Transfer()
        {
        }

        public Transfer(Customer customer, Bank bank, LocalAccount fromAccount, string toAccountNumber, Money amount)
        {
            this.Bank = bank;
            this.customer = customer;
            FromAccount = fromAccount;
            this.ToAccountNumber = toAccountNumber;
            Money = amount;
            FromAccount.OutgoingOperations.Add(this);
        }

        public long Ibpaid { get; set; }
    }
}
