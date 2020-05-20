using OOPBank.Classes.Operations;

namespace OOPBank.Classes.IBPA
{
    public interface IBankMediator
    {
        public void registerBank(IBankColleague bank);
        public void performInterBankTransfer(Transfer transfer);
    }
}