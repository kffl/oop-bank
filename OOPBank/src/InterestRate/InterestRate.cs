using System;
using System.Collections.Generic;

namespace OOPBank.InterestRate
{
    public abstract class InterestRate : IInterestMechanism
    {
        protected readonly LocalAccount account;
        public readonly double depositInterestConstant = 0.01;
        protected readonly List<Operation> incomingOperations;
        public readonly double loanInterestConstant = 0.015;
        protected readonly List<Operation> outgoingOperations;


        protected InterestRate(LocalAccount account, List<Operation> incomingOperations,
            List<Operation> outgoingOperations)
        {
            this.account = account;
            this.incomingOperations = incomingOperations;
            this.outgoingOperations = outgoingOperations;
        }


        public abstract double calculateInterest(Action<InterestRate> setInterestRateState);
    }
}