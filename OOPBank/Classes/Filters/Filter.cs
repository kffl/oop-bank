namespace OOPBank.Classes.Filters
{
    public abstract class Filter
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

    }
}
