using System;
using System.Collections.Generic;
using System.Text;
using OOPBank.Classes.Operations;

namespace OOPBank.Classes.OperationExecuting
{
    class TransferHandler : OperationHandler
    {
        public TransferHandler(OperationHandler nextHandler) : base(nextHandler)
        {
        }

        public override void handle(Operation operation)
        {
            if (operation is Transfer specificOp)
            {
                execute(specificOp);
            }
            passToNext(operation);
        }

        private void execute(Transfer operation)
        {
            LocalAccount localAccount = (LocalAccount)operation.FromAccount;
            //check if account belongs to this bank
            if (localAccount == null || !operation.bank.getAccounts().Contains(localAccount))
                throw new Exception("This account does not belong to our bank.");
            else
            {
                if (operation.Money <= 0) throw new Exception("Amount has to be greater than 0.");
                if (localAccount.AccountNumber == operation.toAccountNumber)
                    throw new Exception("Transfer has to be between different accounts.");
                if (!localAccount.hasSufficientBalance(operation.Money)) throw new Exception("Insufficient account balance.");

                if (operation.toAccountNumber.StartsWith(operation.bank.accountPrefix))
                {
                    //it's an internal transfer
                    var recipientsAccount = operation.bank.getAccounts().Find(a => a.AccountNumber == operation.toAccountNumber);
                    if (recipientsAccount == null) throw new Exception("Recipient's account not found");

                    localAccount.decreaseBalance(operation.Money);
                    recipientsAccount.increaseBalance(operation.Money);
                    recipientsAccount.IncomingOperations.Add(operation);
                    operation.Status = Operation.OperationStatus.Completed;
                }
                else
                {
                    //it's an external transfer
                    operation.Status = Operation.OperationStatus.PendingCompletion;
                    localAccount.decreaseBalance(operation.Money);
                    localAccount.OutgoingOperations.Add(operation);
                    operation.bank.IBPA.performInterBankTransfer(operation);
                }
            }

        }
    }
}
