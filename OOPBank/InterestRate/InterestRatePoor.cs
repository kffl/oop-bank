using System;
using System.Collections.Generic;

namespace OOPBank.InterestRate
{
    public class InterestRatePoor : InterestRate
    {
        private const double BalanceConstant = 2;

        public InterestRatePoor(LocalAccount account, List<Operation> incomingOperations,
            List<Operation> outgoingOperations) : base(account, incomingOperations, outgoingOperations)
        {
        }

        public override double calculateInterest(Action<InterestRate> setInterestRateState)
        {
            var amount = (BalanceConstant + Math.Log2(incomingOperations.Count) + Math.Log2(outgoingOperations.Count)) *
                         0.01;
            if (amount < 0.005) amount = 0.005;
            else if (amount > 0.2) amount = 0.2;
            if (account.getBalance() >= 100000)
                setInterestRateState(new InterestRateRich(account, incomingOperations, outgoingOperations));
            return amount;
        }
    }
}