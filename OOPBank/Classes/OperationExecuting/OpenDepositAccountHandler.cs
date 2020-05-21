using System;
using System.Collections.Generic;
using System.Text;
using OOPBank.Classes.Operations;

namespace OOPBank.Classes.OperationExecuting
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
