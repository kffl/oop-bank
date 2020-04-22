using System.Collections.Generic;
using OOPBank.Classes.Filters;

namespace OOPBank.Classes
{
    public class Report
    {
        public readonly List<Operation> Operations;
        public readonly Account Account;

        public Report(Account account, List<Operation> operations)
        {
            Account = account;
            Operations = operations;
        }

    }
}
