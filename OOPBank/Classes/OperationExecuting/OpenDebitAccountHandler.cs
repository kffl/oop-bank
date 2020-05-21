using System;
using System.Collections.Generic;
using System.Text;
using OOPBank.Classes.Operations;

namespace OOPBank.Classes.OperationExecuting
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
            if (operation.startingDebit <= 0) throw new Exception("Debt limitation has to be greater than 0.");
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
