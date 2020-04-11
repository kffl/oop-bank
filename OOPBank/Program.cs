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

            var JohnsAccount = Bank1.openAccount(JohnDoe, 1000);
            var AndresSmithsAccount = SuperBank.openAccount(AndrewSmith, 2000);
            JohnsAccount.displayAccountDetails();
            AndresSmithsAccount.displayAccountDetails();

            Bank1.makeTransfer(JohnDoe, JohnsAccount, "SB00000002", new Money(100));

            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.processQueuedPayments();
            AndresSmithsAccount.displayAccountDetails();
            AndresSmithsAccount.displayHistory();
        }

        public static int AddInts(int a, int b)
        {
            return a + b;
        }
    }
}
