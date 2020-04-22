using System;

namespace OOPBank.Classes.Filters
{
    public class Filter
    {
        public int OperationsLimit { get; }
        public OperationType Type { get; }

        public enum OperationType
        {
            Incoming,
            Outgoing,
            All
        }

        protected Filter(int operationsLimit, OperationType type)
        {
            OperationsLimit = operationsLimit;
            Type = type;
        }

        public void showDetails()
        {
            Console.WriteLine("Filter: showing " + OperationsLimit + " operations of type: " + Type);
        }

    }
}
