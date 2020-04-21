using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class Bank
    {
        private static long lastAccountNumber;
        private readonly List<LocalAccount> accounts = new List<LocalAccount>();
        private readonly List<Customer> customers = new List<Customer>();
        private readonly List<ExternalOperation> pendingExternalOperations = new List<ExternalOperation>();
        public string accountPrefix;

        public Bank(string name, string accountPrefix)
        {
            this.name = name;
            this.accountPrefix = accountPrefix;
            //the bank has to register itself to IBPA
            var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.registerBank(this);
        }

        public string name { get; }

        public void addCustomer(Customer newCustomer)
        {
            customers.Add(newCustomer);
        }

        private string generateAccountNumber()
        {
            return accountPrefix + (++lastAccountNumber).ToString("D8");
        }

        public LocalAccount openDebitAccount(Customer customer, long startingBalance = 0, long startingDebit = 0)
        {
            var newAccount = new DebitAccount(
                customer,
                generateAccountNumber(),
                startingBalance,
                startingDebit
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public LocalAccount openLoanAccount(Customer customer, long startingBalance = 0, long startingLoan = 0)
        {
            var newAccount = new LoanAccount(
                customer,
                generateAccountNumber(),
                startingBalance,
                startingLoan
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public LocalAccount openDepositAccount(Customer customer, long startingBalance = 0, int durationDays = 30)
        {
            var newAccount = new DepositAccount(
                customer,
                generateAccountNumber(),
                startingBalance,
                new TimeSpan(durationDays, 0, 0, 0)
            );
            accounts.Add(newAccount);
            return newAccount;
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

        public void takeLoan(Customer customer, LocalAccount fromAccount, Money amount)
        {
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");
            if (!(fromAccount is LoanAccount loanAccount)) throw new Exception("Wrong type of account.");
            loanAccount.loanAmount += amount;
        }

        public void chargeInstallment(Customer customer, LocalAccount fromAccount, Money amount)
        {
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");
            if (!(fromAccount is LoanAccount loanAccount)) throw new Exception("Wrong type of account.");
            if (loanAccount.tooMuchTransfer(amount))
                throw new Exception("Tried to transfer to much money.");

            if (!loanAccount.hasSufficientBalance(amount)) throw new Exception("Insufficient account balance.");

            var newOperation = new InternalOperation(loanAccount, amount);
            newOperation.setOperationStatus(Operation.OperationStatus.Completed);
            loanAccount.bookInstalmentOperation(newOperation);
        }

        public void makeTransfer(Customer customer, LocalAccount fromAccount, string toAccountNumber, Money amount)
        {
            //check if account belongs to this bank
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");

            if (toAccountNumber.StartsWith(accountPrefix))
            {
                //it's an internal transfer
                var recipientsAccount = accounts.Find(a => a.accountNumber == toAccountNumber);
                if (recipientsAccount != null)
                {
                    if (recipientsAccount is DepositAccount recipientsDepositAccount
                        && recipientsDepositAccount.hasFirstIncoming)
                        throw new Exception("You can transfer money only one time.");

                    if (fromAccount.hasSufficientBalance(amount))
                    {
                        var newOperation = new InternalOperation(fromAccount, recipientsAccount, amount);
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
                    var newOperation = new ExternalOperation(fromAccount, new Account(toAccountNumber), amount);
                    newOperation.setOperationStatus(Operation.OperationStatus.PendingCompletion);
                    fromAccount.bookOutgoingOperation(newOperation);
                    var IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
                    var transactionID =
                        IBPA.performInterBankTransfer(fromAccount.accountNumber, this, toAccountNumber, amount);
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
            if (toAccount == null) return false; //payment failed, notify IBPA so that it bounces back to sender

            var newOperation = new ExternalOperation(new Account(fromAccountNumber), toAccount, amount);
            toAccount.bookIncomingOperation(newOperation);
            return true;
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