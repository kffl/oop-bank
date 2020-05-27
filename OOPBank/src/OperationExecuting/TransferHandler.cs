using System;
using OOPBank.Operations;

namespace OOPBank.OperationExecuting
{
    public class TransferHandler : OperationHandler
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
            if (localAccount == null || !operation.Bank.getAccounts().Contains(localAccount))
                throw new Exception("This account does not belong to our bank.");
            else
            {
                if (operation.Money <= 0) throw new Exception("Amount has to be greater than 0.");
                if (localAccount.AccountNumber == operation.ToAccountNumber)
                    throw new Exception("Transfer has to be between different accounts.");
                if (!localAccount.hasSufficientBalance(operation.Money)) throw new Exception("Insufficient account balance.");

                if (operation.ToAccountNumber.StartsWith(operation.Bank.accountPrefix))
                {
                    //it's an internal transfer
                    var recipientsAccount = operation.Bank.getAccounts().Find(a => a.AccountNumber == operation.ToAccountNumber);
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
                    operation.Bank.IBPA.performInterBankTransfer(operation);
                }
            }

        }
    }
}
