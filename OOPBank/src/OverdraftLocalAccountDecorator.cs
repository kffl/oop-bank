
namespace OOPBank
{
    public class OverdraftLocalAccountDecorator : LocalAccountDecorator
    {
        public OverdraftLocalAccountDecorator(ILocalAccount component) : base(component)
        {
        }

        public override void withdrawMoney(Money amount)
        {
            balance -= amount;
        }
    }
}
