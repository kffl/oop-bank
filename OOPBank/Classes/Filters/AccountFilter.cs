using System;

namespace OOPBank.Classes.Filters
{
    public class AccountFilter : Filter
    {
        public Account Account { get; }
        public AccountType AccountFilterType { get; }

        public enum AccountType
        {
            incoming,
            outgoing,
            both
        }

        public AccountFilter(int operationsLimit, OperationType type, Account account, AccountType accountFilterType) : base(operationsLimit, type)
        {
            Account = account;
            AccountFilterType = accountFilterType;
        }

        public new void showDetails()
        {
            Console.WriteLine("AccountFilter: showing " + OperationsLimit + " operations of type: " + Type + " with account filter type: " + AccountFilterType + " with account number set to: " + Account.accountNumber);
        }
    }
}
