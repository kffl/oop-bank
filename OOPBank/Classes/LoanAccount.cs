using System;

namespace OOPBank
{
    public class LoanAccount : LocalAccount
    {
        public Money loanAmount { get; set; }

        public LoanAccount(Customer owner, string number, Money startingBalance, Money loanAmount) : base(owner, number,
            startingBalance)
        {
            this.loanAmount = new Money(loanAmount.dollars, loanAmount.cents);
        }

        public bool tooMuchTransfer(Money money)
        {
            return loanAmount - money < 0;
        }

        public void bookInstallmentOperation(Operation operation)
        {
            outgoingOperations.Add(operation);
            balance -= operation.money;
            loanAmount -= operation.money;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Loan account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.asDouble);
            Console.WriteLine("Loan amount: " + loanAmount.asDouble);
            Console.WriteLine("###############################");
        }
    }
}