
namespace OOPBank.OperationExecuting
{
    public class OperationExecutor
    {
        private readonly OperationHandler _firstHandler;

        public OperationExecutor()
        {
            _firstHandler = initializeChain();
        }

        private OperationHandler initializeChain()
        {
            //init from end to beginning of chain
            var takeLoanHandler = new TakeLoanHandler(null);
            var openLoanAccountHandler = new OpenLoanAccountHandler(takeLoanHandler);
            var openDepositAccountHandler = new OpenDepositAccountHandler(openLoanAccountHandler);
            var openDebitAccountHandler = new OpenDebitAccountHandler(openDepositAccountHandler);
            var openAccountHandler = new OpenAccountHandler(openDebitAccountHandler);
            var chargeInstallmentHandler = new ChargeInstallmentHandler(openAccountHandler);
            var transferHandler = new TransferHandler(chargeInstallmentHandler);

            return transferHandler; //first handler
        }

        public void execute(Operation operation)
        {
            _firstHandler.handle(operation);
        }
    }
}
