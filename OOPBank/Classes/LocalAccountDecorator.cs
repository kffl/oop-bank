namespace OOPBank.Classes
{
    public abstract class LocalAccountDecorator : ILocalAccount
    {
        private readonly ILocalAccount decoratedComponent;


        public LocalAccountDecorator(ILocalAccount component)
        {
            decoratedComponent = component;
        }


        public Money balance
        {
            get => decoratedComponent.balance;
            set => decoratedComponent.balance = value;
        }

        public virtual void withdrawMoney(Money amount)
        {
            decoratedComponent.withdrawMoney(amount);
        }
    }
}