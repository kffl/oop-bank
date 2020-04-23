﻿using System;

namespace OOPBank.Classes.Filters
{
    public class DateFilter : Filter
    {
        public virtual DateTime DateFrom { get; }
        public virtual DateTime DateTo { get; }

        public DateFilter()
        {
        }

        public DateFilter(int operationsLimit, OperationType type, DateTime dateFrom, DateTime dateTo) : base(operationsLimit, type)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public new void showDetails()
        {
            Console.WriteLine("AccountFilter: showing " + OperationsLimit + " operations of type: " + Type + " with dates between " + DateFrom + " and " + DateTo);
        }
    }
}