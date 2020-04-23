namespace OOPBank
{
    //all the stuff that IBPA needs to interact with a bank
    public interface IBank
    {
        string accountPrefix { get; }
        void handleConfirmation(long transactionID);
        bool handleIncomingPayment(string fromAccountNumber, string toAccountNumber, Money amount);
        void handlePaymentFailure(long transactionID);
    }
}