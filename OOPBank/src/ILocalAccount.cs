
namespace OOPBank
{
    public interface ILocalAccount
    {
        Money balance { get; set; }
        void withdrawMoney(Money amount);
    }
}
