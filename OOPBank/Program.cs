using System;

namespace OOPBank
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var Bank1 = new Bank("Bank1", "B1");
            var SuperBank = new Bank("SuperBank", "SB");
            var JohnDoe = new Customer("John", "Doe");
            var AndrewSmith = new Customer("Andrew", "Smith");
            Bank1.addCustomer(JohnDoe);
            SuperBank.addCustomer(AndrewSmith);

            var JohnsDebitAccount = Bank1.openDebitAccount(JohnDoe, 1000, 111);
            var JohnsLoanAccount = Bank1.openLoanAccount(JohnDoe, 1000,234);
            var JohnsDepositAccount = Bank1.openDebitAccount(JohnDoe, 1000, 123);
            var AndresSmithsAccount = SuperBank.openAccount(AndrewSmith, 2000);
            
            JohnsDebitAccount.displayAccountDetails();
            JohnsLoanAccount.displayAccountDetails();
            JohnsDepositAccount.displayAccountDetails();
            AndresSmithsAccount.displayAccountDetails();

            Bank1.makeTransfer(JohnDoe, JohnsDebitAccount, "SB00000004", new Money(99));
            Bank1.chargeInstallment(JohnDoe, JohnsLoanAccount, new Money(100));
            Bank1.takeLoan(JohnDoe, JohnsLoanAccount, new Money(400));

            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.processQueuedPayments();
            JohnsDebitAccount.displayAccountDetails();
            JohnsLoanAccount.displayAccountDetails();
            JohnsDepositAccount.displayAccountDetails();
            AndresSmithsAccount.displayAccountDetails();
            JohnsDebitAccount.displayHistory();
            AndresSmithsAccount.displayHistory();
        }

        public static int AddInts(int a, int b)
        {
            return a + b;
        }
    }
}
