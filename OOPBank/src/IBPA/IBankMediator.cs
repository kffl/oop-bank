using OOPBank.Operations;

namespace OOPBank.IBPA
{
    public interface IBankMediator
    {
        public void registerBank(IBankColleague bank);
        public void performInterBankTransfer(Transfer transfer);
    }
}