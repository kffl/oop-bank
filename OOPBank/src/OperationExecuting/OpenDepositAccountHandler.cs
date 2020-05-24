using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    class OpenDepositAccountHandler : OperationHandler
    {
        public OpenDepositAccountHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }

        public override void handle(Operation operation)
        {
            if (operation is OpenDepositAccount specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(OpenDepositAccount operation)
        {
            var newAccount = new DepositAccount(
                operation.customer,
                operation.bank.generateAccountNumber(),
                operation.Money ?? new Money(),
                operation.depositAmount,
                operation.durationDays
            );
            operation.bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(operation);
        }
    }
}
