using System.Collections.Generic;
using System;

namespace OOPBank
{
    public class Bank
    {
        private string Name { get; }
        private List<Customer> customers;
        private List<LocalAccount> accounts = new List<LocalAccount>();
        public string accountPrefix;
        private static long lastAccountNumber = 0;
        private List<ExternalOperation> pendingExternalOperations = new List<ExternalOperation>();

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

        public LocalAccount openAccount(Customer customer, long startingBalance = 0)
        {
            var newAccount = new LocalAccount(
                customer,
                generateAccountNumber(),
                startingBalance
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public void makeTransfer(Customer customer, LocalAccount fromAccount, string toAccountNumber, Money amount)
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
                        InternalOperation newOperation = new InternalOperation(fromAccount, recipientsAccount, amount);
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
                    throw new Exception("Recipient's account not found");
                }
            }
            else
            {
                //it's an external transfer
                if (fromAccount.hasSufficientBalance(amount))
                    {
                        ExternalOperation newOperation = new ExternalOperation(fromAccount, new Account(toAccountNumber), amount);
                        newOperation.setOperationStatus(Operation.OperationStatus.PendingCompletion);
                        fromAccount.bookOutgoingOperation(newOperation);
                        var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
                        var transactionID = IBPA.performInterBankTransfer(fromAccount.accountNumber, this, toAccountNumber, amount);
                        newOperation.IBPAID = transactionID;
                        pendingExternalOperations.Add(newOperation);
                    }
            }
        }

        //handle incoming payment from IBPA
        //return true if it can be processed here, return false if it should bounceback
        public bool handleIncomingPayment(string fromAccountNumber, string toAccountNumber, Money amount)
        {
            var toAccount = accounts.Find(a => a.accountNumber == toAccountNumber);
            if (toAccount != null)
            {
                var newOperation = new ExternalOperation(new Account(fromAccountNumber), toAccount, amount);
                toAccount.bookIncomingOperation(newOperation);
                return true;
            }
            {
                //payment failed, notify IBPA so that it bounces back to sender
                return false;
            }
        }

        //handle comfirmation from IBPA regarding one of recently sent payments having been recieved
        public void handleConfirmation(long transactionID)
        {
            var operation = pendingExternalOperations.Find(o => o.IBPAID == transactionID);
            operation.setOperationStatus(Operation.OperationStatus.Completed);
            pendingExternalOperations.Remove(operation);
        }

        //handle info from IBPA regarding one of recently sent payments having been rejected by recipient's bank
        public void handlePaymentFailure(long transactionID)
        {
            var operation = pendingExternalOperations.Find(o => o.IBPAID == transactionID);
            operation.setOperationStatus(Operation.OperationStatus.Rejected);
            pendingExternalOperations.Remove(operation);
        }
    }
}