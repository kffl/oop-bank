using System;
using System.Collections.Generic;
using OOPBank.Classes;
using OOPBank.Classes.IBPA;
using OOPBank.Classes.OperationExecuting;
using OOPBank.Classes.Operations;

namespace OOPBank
{
    public class Program
    {
        private static void Main(string[] args)
        {
            static void DisplayAccountsDetails(List<LocalAccount> accounts)
            {
                foreach (var account in accounts) account.displayAccountDetails();
                Console.Write("\n\n");
            }

            var OperationExecutor = new OperationExecutor();
            var Bank1 = new Bank("Bank1", "B1");
            var SuperBank = new Bank("SuperBank", "SB");
            var JohnDoe = new Customer("John", "Doe");
            var AndrewSmith = new Customer("Andrew", "Smith");
            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            Bank1.addCustomer(JohnDoe);
            SuperBank.addCustomer(AndrewSmith);

            OperationExecutor.execute(new OpenDebitAccount(JohnDoe, Bank1, new Money(1000, 99), new Money(111, 43)));
            OperationExecutor.execute(new OpenDepositAccount(JohnDoe, Bank1, new Money(1000, 12), new Money(234, 53)));
            OperationExecutor.execute(new OpenLoanAccount(JohnDoe, Bank1, new Money(1000, 12), new Money(234, 53)));
            new OpenAccount(AndrewSmith, SuperBank, new Money(2000, 1));


            var JohnsAccountsList = Bank1.getCustomerAccounts(JohnDoe);
            DisplayAccountsDetails(JohnsAccountsList);
            Bank1.simulateNewDay();
            DisplayAccountsDetails(JohnsAccountsList);
            IBPA.processQueuedPayments();

            OperationExecutor.execute(new Transfer(JohnDoe, Bank1, JohnsAccountsList[0], "SB00000004", new Money(1000, 64)));
            OperationExecutor.execute(new Transfer(JohnDoe, Bank1, JohnsAccountsList[1], "SB00000004", new Money(1000, 64)));
            OperationExecutor.execute(new Transfer(JohnDoe, Bank1, JohnsAccountsList[2], "SB00000004", new Money(1200, 64)));

            OperationExecutor.execute(new TakeLoan(JohnDoe, Bank1, JohnsAccountsList[2] as LoanAccount, new Money(400, 99)));
            DisplayAccountsDetails(JohnsAccountsList);
            OperationExecutor.execute(new ChargeInstallment(JohnDoe, Bank1, JohnsAccountsList[2] as LoanAccount, new Money(400, 99)));

            DisplayAccountsDetails(JohnsAccountsList);
            IBPA.processQueuedPayments();
            Bank1.simulateNewDay();
            DisplayAccountsDetails(JohnsAccountsList);

            JohnsAccountsList[2].displayHistory();
            Console.Write("\n");
            //AndrewSmithsAccount.displayHistory();
        }
    }
}
