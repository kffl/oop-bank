using System;

namespace OOPBank.Classes
{
    public class LoanAccount : LocalAccount
    {
        public Money loanAmount;

        private Money lostMoney = new Money();


        public LoanAccount(Customer owner, string number, Money startingBalance, Money loanAmount) : base(
            owner, number,
            startingBalance + loanAmount)
        {
            this.loanAmount = new Money(loanAmount.dollars, loanAmount.cents);
        }

        public bool tooMuchTransfer(Money money)
        {
            return loanAmount - money < 0;
        }

        public override void handleNewDay()
        {
            var capitalization = loanAmount * (InterestRate + interestRate.loanInterestConstant);
            lostMoney += capitalization;
            loanAmount += capitalization;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Loan account details  ###");
            Console.WriteLine("Number: " + AccountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Lost money: " + lostMoney.asDouble);
            Console.WriteLine("Loan amount: " + loanAmount.asDouble);
            Console.WriteLine("###############################");
        }
    }
}
