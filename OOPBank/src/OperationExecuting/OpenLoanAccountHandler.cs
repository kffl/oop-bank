﻿using System;
using System.Collections.Generic;
using System.Text;
using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    class OpenLoanAccountHandler : OperationHandler
    {
        public OpenLoanAccountHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }
        public override void handle(Operation operation)
        {
            if (operation is OpenLoanAccount specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(OpenLoanAccount operation)
        {
            var newAccount = new LoanAccount(
                operation.customer,
                operation.bank.generateAccountNumber(),
                operation.Money ?? new Money(),
                operation.startingLoan ?? new Money()
            );
            operation.bank.addAccount(newAccount);
            newAccount.OtherOperations.Add(operation);
        }
    }
}
