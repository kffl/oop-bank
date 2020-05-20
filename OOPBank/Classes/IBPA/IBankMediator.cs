namespace OOPBank.Classes.IBPA
{
    public interface IBankMediator
    {
        /// <summary>
        /// Asks inter-bank mediator to perform a specified payment
        /// </summary>
        /// <param name="fromAccountNumber">Sender's account number</param>
        /// <param name="fromBank">Bank from which the payment is sent</param>
        /// <param name="toAccountNumber">Recipient's account number</param>
        /// <param name="amount">Amount of money that is being sent via this payment</param>
        /// <returns>Unique payment ID assigned by the mediator</returns>
        public long performInterBankTransfer(string fromAccountNumber, IBankColleague fromBank, string toAccountNumber, Money amount);
        public void registerBank(IBankColleague bank);
    }
}