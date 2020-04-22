using System;
using System.Collections.Generic;

namespace OOPBank.Classes
{
    public class InterestRate
    {
        private readonly LocalAccount account;
        private readonly List<Operation> incomingOperations;
        private readonly List<Operation> outgoingOperations;
        public readonly double loanInterestConstant = 0.015;
        public readonly double depositInterestConstant = 0.01;


        public InterestRate(LocalAccount account, List<Operation> incomingOperations,
            List<Operation> outgoingOperations)
        {
            this.account = account;
            this.incomingOperations = incomingOperations;
            this.outgoingOperations = outgoingOperations;
        }

        public double Amount
        {
            get
            {
                var amount = (Math.Log10(account.getBalance().asDouble) + Math.Log2(incomingOperations.Count) +
                             Math.Log2(outgoingOperations.Count)) * 0.01;
                if (amount < 0.005) amount = 0.005;
                else if (amount > 0.2) amount = 0.2;
                return amount;
            }
        }
    }
}