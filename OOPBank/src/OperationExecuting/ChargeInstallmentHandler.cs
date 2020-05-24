using System;
using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    class ChargeInstallmentHandler : OperationHandler
    {
        public ChargeInstallmentHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }

        public override void handle(Operation operation)
        {
            if (operation is ChargeInstallment specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(ChargeInstallment operation)
        {
            if (!operation.Bank.getAccounts().Contains(operation.LoanAccount))
                throw new Exception("This account does not belong to our bank.");
            if (operation.Money <= 0) throw new Exception("Amount has to be greater than 0.");
            if (!operation.LoanAccount.hasSufficientBalance(operation.Money)) throw new Exception("Insufficient account balance.");
            if (operation.LoanAccount.tooMuchTransfer(operation.Money)) throw new Exception("Tried to transfer too much money.");

            operation.LoanAccount.chargeInstallment(operation.Money);
            operation.Status = Operation.OperationStatus.Completed;
            operation.DateOfExecution = new DateTime();
        }

    }
}
