using System.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using OOPBank.Classes.Filters;

namespace OOPBank.Classes
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
                operations = operations.Take(filter.OperationsLimit).ToList();  //limit number of operations

            return new Report(account, operations, filter);
        }

        private static List<Operation> filterAccount(List<Operation> operations, AccountFilter accountFilter)
        {
            switch (accountFilter.Type)
            {
                case Filter.OperationType.Incoming:
                    return operations.Where(operation => operation.FromAccount == accountFilter.Account).ToList();
                case Filter.OperationType.Outgoing:
                    return operations.Where(operation => operation.ToAccount == accountFilter.Account).ToList();
                case Filter.OperationType.All:
                    return operations;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private static List<Operation> filterAmount(List<Operation> operations, AmountFilter amountFilter)
        {
            return operations.Where(operation => operation.Money >= amountFilter.AmountFrom && operation.Money <= amountFilter.AmountTo).ToList();

        }

        private static List<Operation> filterDate(List<Operation> operations, DateFilter filter)
        {
            return operations.Where(operation => operation.DateOfExecution >= filter.DateFrom && operation.DateOfExecution <= filter.DateTo).ToList();
        }

        public static List<Operation> filterType(Account account, Filter filter)
        {
            var operations = new List<Operation>();

            switch (filter.Type)
            {
                case Filter.OperationType.Incoming:
                    operations.AddRange(account.IncomingOperations);
                    break;

                case Filter.OperationType.Outgoing:
                    operations.AddRange(account.OutgoingOperations);
                    break;

                case Filter.OperationType.All:
                    operations.AddRange(account.IncomingOperations);
                    operations.AddRange(account.OutgoingOperations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return operations;
        }
    }
}
