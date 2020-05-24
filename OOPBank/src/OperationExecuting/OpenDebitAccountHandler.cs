using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    class OpenDebitAccountHandler : OperationHandler
    {
        public OpenDebitAccountHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }

        public override void handle(Operation operation)
        {
            if (operation is OpenDebitAccount specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(OpenDebitAccount operation)
        {
            var newAccount = new DebitAccount(
                operation.customer,
                operation.bank.generateAccountNumber(),
                operation.Money ?? new Money(),
                operation.startingDebit ?? new Money()
            );
            operation.bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(operation);
        }

    }
}
