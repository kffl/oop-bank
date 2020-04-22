using System;
using System.Collections.Generic;
using OOPBank.Classes.Filters;

namespace OOPBank.Classes
{
    public class Report
    {
        public readonly List<Operation> Operations;
        public readonly Filter Filter;
        public readonly Account Account;

        public Report(Account account, List<Operation> operations, Filter filter)
        {
            Account = account;
            Operations = operations;
            Filter = filter;
        }

        public void displayReport()
        {
            Console.WriteLine("### Report for Account number: " + Account.accountNumber + " ###");
            Console.WriteLine("Used filter:");
            Filter.showDetails();
            Console.WriteLine("Operations:");
            foreach (var operation in Operations) operation.displayOperationDetails();
            Console.WriteLine("#########################");
        }
    }
}
