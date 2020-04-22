﻿using System;

namespace OOPBank.Classes.Filters
{
    public class DateFilter : Filter
    {
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public DateFilter(int operationsLimit, OperationType type, DateTime dateFrom, DateTime dateTo) : base(operationsLimit, type)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
