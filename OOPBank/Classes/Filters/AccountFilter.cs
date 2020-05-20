using System;

namespace OOPBank.Classes.Filters
{
    public class AccountFilter : Filter
    {
        public virtual Account Account { get; }

        public AccountFilter()
        {
        }

        public AccountFilter(int operationsLimit, OperationType type, Account account) : base(operationsLimit, type)
        {
            Account = account;
        }

        public new void showDetails()
        {
            Console.WriteLine("AccountFilter: showing " + OperationsLimit + " operations of type: " + Type + " with account number set to: " + Account.accountNumber);
        }

        public new Operation VisitOperation(Operation operation)
        {
            switch (Type)
            {
                case Filter.OperationType.Incoming:
                    if (operation.FromAccount == Account)
                    {
                        return operation;
                    }
                    break;
                case Filter.OperationType.Outgoing:
                    if (operation.ToAccount == Account)
                    {
                        return operation;
                    }
                    break;
                case Filter.OperationType.All:
                    return operation;
            }
            return null;
        }
    }
}
