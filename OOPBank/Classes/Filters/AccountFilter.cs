using System;

namespace OOPBank.Classes.Filters
{
    public class AccountFilter : Filter
    {
        public AccountFilter()
        {
        }

        public AccountFilter(int operationsLimit, OperationType type, Account account) : base(operationsLimit, type)
        {
            Account = account;
        }

        public virtual Account Account { get; }

        public new void showDetails()
        {
            Console.WriteLine(
                "AccountFilter: showing " + OperationsLimit + " operations of type: " + Type +
                " with account number set to: " + Account.AccountNumber);
        }
    }
}
