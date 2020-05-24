using System;

namespace OOPBank.Filters
{
    public class AmountFilter : Filter
    {
        public AmountFilter()
        {
        }

        public AmountFilter(int operationsLimit, OperationType type, Money amountFrom, Money amountTo) : base(
            operationsLimit, type)
        {
            AmountFrom = amountFrom;
            AmountTo = amountTo;
        }

        public virtual Money AmountFrom { get; }

        public virtual Money AmountTo { get; }

        public new void showDetails()
        {
            Console.WriteLine(
                "AccountFilter: showing " + OperationsLimit + " operations of type: " + Type + " with amount between " +
                AmountFrom.asDouble + " and " + AmountTo.asDouble);
        }

        public new Operation VisitOperation(Operation operation)
        {
            if (operation.Money >= AmountFrom && operation.Money <= AmountTo)
            {
                return operation;
            }
            return null;
        }
    }
}
