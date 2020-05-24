using System;
using System.Collections.Generic;
using OOPBank.Filters;

namespace OOPBank
{
    public class Report
    {
        public readonly Account Account;

        public readonly Filter Filter;

        public readonly List<Operation> Operations;

        public Report(Account account, List<Operation> operations, Filter filter)
        {
            Account = account;
            Operations = operations;
            Filter = filter;
        }

        public void displayReport()
        {
            Console.WriteLine("### Report for Account number: " + Account.AccountNumber + " ###");
            Console.WriteLine("Used filter:");
            Filter.showDetails();
            Console.WriteLine("Operations:");
            foreach (var operation in Operations) operation.displayOperationDetails();
            Console.WriteLine("#########################");
        }
    }
}
