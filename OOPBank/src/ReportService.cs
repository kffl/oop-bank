using System.Collections.Generic;
using System.Linq;
using OOPBank.Filters;

namespace OOPBank
{
    public static class ReportService
    {
        public static Report generateReport(Account account, Filter filter)
        {
            var allOperations = new List<Operation>();
            var operations = new List<Operation>();
            allOperations.AddRange(account.IncomingOperations);
            allOperations.AddRange(account.OutgoingOperations);

            foreach (var operation in allOperations)
            {
                var op = operation.acceptFilter(filter);
                if (op is Operation cop)
                {
                    operations.Add(cop);
                }
            }

            operations = operations.OrderByDescending(o => o.DateOfExecution).ToList(); //latest first

            if (operations.Count > filter.OperationsLimit)
                operations = operations.Take(filter.OperationsLimit).ToList(); //limit number of operations

            return new Report(account, operations, filter);
        }
    }
}
