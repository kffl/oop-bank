namespace OOPBank
{
    public class ExternalOperation : Operation
    {
        public long IBPAID { get; set; }
        public ExternalOperation(Account fromAccount, Account toAccount, Money money) : base(fromAccount, toAccount, money)
        {
        }
    }
}