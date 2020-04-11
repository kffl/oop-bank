using System.Collections.Generic;
using System;

namespace OOPBank
{
    public class Bank
    {
        private string Name { get; }
        private List<Customer> customers;
        private List<Account> accounts;
        public string accountPrefix;
        private static long lastAccountNumber = 0;

        public Bank(string Name, string accountPrefix)
        {
            this.Name = Name;
            this.accountPrefix = accountPrefix;
            customers = new List<Customer>();
            //the bank has to register itself to IBPA
            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.registerBank(this);
        }

        public void addCustomer(Customer newCustomer)
        {
            customers.Add(newCustomer);
        }

        private string generateAccountNumber()
        {
            return accountPrefix + ((++lastAccountNumber).ToString("D8"));
        }

        public Account openAccount(Customer customer)
        {
            var newAccount = new Account(
                customer,
                generateAccountNumber()
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public void makeTransfer(Customer customer, Account fromAccount, string toAccountNumber, Money amount)
        {
            //check if account belongs to this bank
            if (!accounts.Contains(fromAccount))
            {
                throw new Exception("This account does not belong to our bank.");
            }

            if (toAccountNumber.StartsWith(accountPrefix))
            {
                //it's an internal transfer
                var recipientsAccount = accounts.Find(a => a.accountNumber == toAccountNumber);
                if (recipientsAccount != null)
                {
                    if (fromAccount.hasSufficientBalance(amount))
                    {
                        Operation newOperation = new Operation(fromAccount, recipientsAccount, amount);
                        newOperation.setOperationStatus(Operation.OperationStatus.Completed);
                        fromAccount.bookOutgoingOperation(newOperation);
                        recipientsAccount.bookIncomingOperation(newOperation);
                    }
                    else
                    {
                        throw new Exception("Insufficient account balance.");
                    }
                }
                else
                {
                    throw new Exception("Recipients account not found");
                }
            }
            else
            {
                //it's an external transfer
            }
            
        }
    }
}