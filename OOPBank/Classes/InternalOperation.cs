namespace OOPBank
{
    public class InternalOperation : Operation
    {
        public InternalOperation(Account fromAccount, Account toAccount, Money money) : base(fromAccount, toAccount, money)
        {
        }
    }
}