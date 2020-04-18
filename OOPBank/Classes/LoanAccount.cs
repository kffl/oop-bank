using System;

namespace OOPBank
{
    public class LoanAccount : LocalAccount
    {
        public Money loanAmount { get; set; }

        public LoanAccount()
        {
        }

        public LoanAccount(Customer owner, string number, long startingBalance, long loanAmount) : base(owner, number,
            startingBalance)
        {
            this.loanAmount = new Money(loanAmount);
        }

        public bool tooMuchTransfer(Money money)
        {
            return (loanAmount - money).Amount < 0;
        }

        public void bookInstalmentOperation(Operation operation)
        {
            outgoingOperations.Add(operation);
            balance -= operation.money;
            loanAmount -= operation.money;
        }

        public override void displayAccountDetails()
        {
            Console.WriteLine("###  Loan account details  ###");
            Console.WriteLine("Number: " + accountNumber);
            Console.WriteLine("Balance: " + balance.Amount);
            Console.WriteLine("Loan amount: " + loanAmount.Amount);
            Console.WriteLine("###############################");
        }
    }
}