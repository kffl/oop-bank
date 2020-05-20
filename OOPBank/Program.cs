using System;
using System.Collections.Generic;
using OOPBank.Classes;
using OOPBank.Classes.IBPA;

namespace OOPBank
{
    public class Program
    {
        private static void Main(string[] args)
        {
            static void displayAccountsDetails(List<LocalAccount> accounts)
            {
                foreach (var account in accounts) account.displayAccountDetails();
                Console.Write("\n\n");
            }

            var Bank1 = new Bank("Bank1", "B1");
            var SuperBank = new Bank("SuperBank", "SB");
            var JohnDoe = new Customer("John", "Doe");
            var AndrewSmith = new Customer("Andrew", "Smith");
            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            Bank1.addCustomer(JohnDoe);
            SuperBank.addCustomer(AndrewSmith);

            var JohnsDebitAccount = Bank1.openDebitAccount(JohnDoe, new Money(5000, 99), new Money(111, 43));
            var JohnsLoanAccount = Bank1.openLoanAccount(JohnDoe, new Money(99000, 12), new Money(234, 53));
            var JohnsDepositAccount = Bank1.openDepositAccount(JohnDoe, new Money(1000, 6), new Money(1230, 64), 1);
            var AndrewSmithsAccount = SuperBank.openAccount(AndrewSmith, new Money(2000, 1));
            var accounts = new List<LocalAccount>
            {
                JohnsDebitAccount,
                JohnsLoanAccount,
                JohnsDepositAccount,
                AndrewSmithsAccount
            };

            displayAccountsDetails(accounts);
            Bank1.simulateNewDay();
            IBPA.processQueuedPayments();
            displayAccountsDetails(accounts);

            Bank1.makeTransfer(JohnDoe, JohnsDebitAccount, "SB00000004", new Money(1050, 64));
            Bank1.makeTransfer(JohnDoe, JohnsDebitAccount, JohnsLoanAccount.accountNumber, new Money(1300));
            Bank1.chargeInstallment(JohnDoe, JohnsLoanAccount, new Money(100, 73));
            Bank1.takeLoan(JohnDoe, JohnsLoanAccount, new Money(400, 99));

            displayAccountsDetails(accounts);
            IBPA.processQueuedPayments();
            Bank1.simulateNewDay();
            displayAccountsDetails(accounts);

            JohnsDebitAccount.displayHistory();
            Console.Write("\n");
            AndrewSmithsAccount.displayHistory();
        }
    }
}