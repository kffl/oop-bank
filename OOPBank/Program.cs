using System;

namespace OOPBank
{
    public class Program
    {
        static void Main(string[] args)
        {
            var Bank1 = new Bank("Bank1", "B1");
            var SuperBank = new Bank("SuperBank", "SB");
            var JohnDoe = new Customer("John", "Doe");
            var AndrewSmith = new Customer("Andrew", "Smith");
            Bank1.addCustomer(JohnDoe);
            SuperBank.addCustomer(AndrewSmith);

            var JohnsDebitAccount = Bank1.openDebitAccount(JohnDoe, new Money(1000, 99), new Money(111, 43));
            var JohnsLoanAccount = Bank1.openLoanAccount(JohnDoe, new Money(1000, 12),new Money(234, 53));
            var JohnsDepositAccount = Bank1.openDebitAccount(JohnDoe, new Money(1000, 6), new Money(123, 64));
            var AndrewSmithsAccount = SuperBank.openAccount(AndrewSmith, new Money(2000, 1));
            
            JohnsDebitAccount.displayAccountDetails();
            JohnsLoanAccount.displayAccountDetails();
            JohnsDepositAccount.displayAccountDetails();
            AndrewSmithsAccount.displayAccountDetails();

            Console.Write("\n\n");

            Bank1.makeTransfer(JohnDoe, JohnsDebitAccount, "SB00000004", new Money(99, 64));
            Bank1.chargeInstallment(JohnDoe, JohnsLoanAccount, new Money(100, 73));
            Bank1.takeLoan(JohnDoe, JohnsLoanAccount, new Money(400, 99));

            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.processQueuedPayments();
            JohnsDebitAccount.displayAccountDetails();
            JohnsLoanAccount.displayAccountDetails();
            JohnsDepositAccount.displayAccountDetails();
            AndrewSmithsAccount.displayAccountDetails();
            Console.Write("\n\n");
            JohnsDebitAccount.displayHistory();
            AndrewSmithsAccount.displayHistory();
        }

        public static int AddInts(int a, int b)
        {
            return a + b;
        }
    }
}
