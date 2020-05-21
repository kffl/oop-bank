using OOPBank.Classes.Operations;

namespace OOPBank.Classes.OperationExecuting
{
    class OpenAccountHandler : OperationHandler
    {
        public OpenAccountHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }

        public override void handle(Operation operation)
        {
            if (operation is OpenAccount specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(OpenAccount operation)
        {
            var newAccount = new LocalAccount(
                operation.customer,
                operation.bank.generateAccountNumber(),
                operation.Money ?? new Money()
            );
            operation.bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(operation);
        }
    }
}
