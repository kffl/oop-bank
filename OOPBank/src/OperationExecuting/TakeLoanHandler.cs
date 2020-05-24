using System;
using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    class TakeLoanHandler : OperationHandler
    {
        public TakeLoanHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }
        public override void handle(Operation operation)
        {
            if (operation is TakeLoan specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(TakeLoan operation)
        {
            if (!operation.bank.getAccounts().Contains(operation.loanAccount))
                throw new Exception("This account does not belong to our bank.");
            if (operation.Money <= 0) throw new Exception("Amount has to be greater than 0.");
            operation.loanAccount.loanAmount += operation.Money;
            operation.loanAccount.balance += operation.Money;
            operation.Status = Operation.OperationStatus.Completed;
        }
    }
}
