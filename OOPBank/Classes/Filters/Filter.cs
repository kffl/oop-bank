using System;

namespace OOPBank.Classes.Filters
{
    public class Filter : IFilterVisitor
    {
        public enum OperationType
        {
            Incoming,

            Outgoing,

            All
        }

        public Filter()
        {
        }

        protected Filter(int operationsLimit, OperationType type)
        {
            OperationsLimit = operationsLimit;
            Type = type;
        }

        public virtual int OperationsLimit { get; }

        public virtual OperationType Type { get; }

        public virtual void showDetails()
        {
            Console.WriteLine("Filter: showing " + OperationsLimit + " operations of type: " + Type);
        }
        public Operation VisitOperation(Operation operation)
        {
            return operation;
        }
    }
}
