using System;

namespace OOPBank.InterestRate
{
    internal interface IInterestMechanism
    {
        double calculateInterest(Action<InterestRate> setInterestRateState);
    }
}
