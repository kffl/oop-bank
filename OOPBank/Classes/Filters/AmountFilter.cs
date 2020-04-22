using System;

namespace OOPBank.Classes.Filters
{
    public class AmountFilter : Filter
    {
        public Money AmountFrom { get; }
        public Money AmountTo { get; }

        public AmountFilter(int operationsLimit, OperationType type, Money amountFrom, Money amountTo) : base(operationsLimit, type)
        {
            AmountFrom = amountFrom;
            AmountTo = amountTo;
        }
    }
}
