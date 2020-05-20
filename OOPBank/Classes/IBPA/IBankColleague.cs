namespace OOPBank.Classes.IBPA
{
    //all the stuff that IBPA needs to interact with a bank
    public interface IBankColleague
    {
        /// <summary>
        /// Each IBPA client/colleague has to have it's unique account prefix
        /// </summary>
        /// <value>Bank's account prefix</value>
        string accountPrefix { get; }
        /// <summary>
        /// Method used to notify the entity that asked inter bank mediator about payment success
        /// </summary>
        /// <param name="transactionID">ID of the payment that succeeded</param>
        void handleConfirmation(long transactionID);
        /// <summary>
        /// Asks the client bank to process an incoming external payment.
        /// </summary>
        /// <param name="fromAccountNumber">Sender's account number (with prefix)</param>
        /// <param name="toAccountNumber">Recipient's account number</param>
        /// <param name="amount">Transfer amount</param>
        /// <returns>True if recipient can accept such operation, false if the operation fails.</returns>
        bool handleIncomingPayment(string fromAccountNumber, string toAccountNumber, Money amount);
        /// <summary>
        /// Method used to notify the entity that asked inter bank mediator about payment failure
        /// </summary>
        /// <param name="transactionID">ID of the payment that failed</param>
        void handlePaymentFailure(long transactionID);
    }
}