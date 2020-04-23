using System;
using System.Collections.Generic;

namespace OOPBank
{
    public class Bank : IBank
    {
        private static long lastAccountNumber;
        private readonly List<LocalAccount> accounts = new List<LocalAccount>();
        private readonly List<Customer> customers = new List<Customer>();
        private readonly InterBankPaymentAgency IBPA;
        private readonly List<ExternalOperation> pendingExternalOperations = new List<ExternalOperation>();
        public string accountPrefix { get; }

        public Bank(string name, string accountPrefix)
        {
            this.name = name;
            this.accountPrefix = accountPrefix;
            //the bank has to register itself to IBPA
            IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.registerBank(this);
        }


        public string name { get; }


        public void simulateNewDay()
        {
            foreach (var account in accounts) account.handleNewDay();
            IBPA.processQueuedPayments();
        }

        public void addCustomer(Customer newCustomer)
        {
            customers.Add(newCustomer);
        }

        private string generateAccountNumber()
        {
            return accountPrefix + (++lastAccountNumber).ToString("D8");
        }

        public DebitAccount openDebitAccount(Customer customer, Money startingBalance = null,
            Money startingDebit = null)
        {
            var newAccount = new DebitAccount(
                customer,
                generateAccountNumber(),
                startingBalance ?? new Money(),
                startingDebit ?? new Money()
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public LoanAccount openLoanAccount(Customer customer, Money startingBalance = null, Money startingLoan = null)
        {
            var newAccount = new LoanAccount(
                customer,
                generateAccountNumber(),
                startingBalance ?? new Money(),
                startingLoan ?? new Money()
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public DepositAccount openDepositAccount(Customer customer, Money depositAmount, Money startingBalance = null,
            int durationDays = 30)
        {
            var newAccount = new DepositAccount(
                customer,
                generateAccountNumber(),
                startingBalance ?? new Money(),
                depositAmount,
                durationDays
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public LocalAccount openAccount(Customer customer, Money startingBalance = null)
        {
            var newAccount = new LocalAccount(
                customer,
                generateAccountNumber(),
                startingBalance ?? new Money()
            );
            accounts.Add(newAccount);
            return newAccount;
        }

        public void takeLoan(Customer customer, LoanAccount fromAccount, Money amount)
        {
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");
            if (amount <= 0) throw new Exception("Amount has to be greater than 0.");
            fromAccount.loanAmount += amount;
        }

        public void chargeInstallment(Customer customer, LoanAccount fromAccount, Money amount)
        {
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");
            if (amount <= 0) throw new Exception("Amount has to be greater than 0.");
            if (!fromAccount.hasSufficientBalance(amount)) throw new Exception("Insufficient account balance.");

            if (fromAccount.tooMuchTransfer(amount)) throw new Exception("Tried to transfer too much money.");

            var newOperation = new InternalOperation(fromAccount, amount);
            newOperation.setOperationStatus(Operation.OperationStatus.Completed);
            fromAccount.bookInstallmentOperation(newOperation);
        }

        public void makeTransfer(Customer customer, LocalAccount fromAccount, string toAccountNumber, Money amount)
        {
            //check if account belongs to this bank
            if (!accounts.Contains(fromAccount)) throw new Exception("This account does not belong to our bank.");
            if (amount <= 0) throw new Exception("Amount has to be greater than 0.");
            if (fromAccount.accountNumber == toAccountNumber) throw new Exception("Transfer has to be between different accounts.");
            if (!fromAccount.hasSufficientBalance(amount)) throw new Exception("Insufficient account balance.");

            if (toAccountNumber.StartsWith(accountPrefix))
            {
                //it's an internal transfer
                var recipientsAccount = accounts.Find(a => a.accountNumber == toAccountNumber);
                if (recipientsAccount == null) throw new Exception("Recipient's account not found");

                var newOperation = new InternalOperation(fromAccount, recipientsAccount, amount);
                newOperation.setOperationStatus(Operation.OperationStatus.Completed);
                fromAccount.bookOutgoingOperation(newOperation);
                recipientsAccount.bookIncomingOperation(newOperation);
            }
            else
            {
                //it's an external transfer
                var newOperation = new ExternalOperation(fromAccount, new Account(toAccountNumber), amount);
                newOperation.setOperationStatus(Operation.OperationStatus.PendingCompletion);
                fromAccount.bookOutgoingOperation(newOperation);
                var transactionID =
                    IBPA.performInterBankTransfer(fromAccount.accountNumber, this, toAccountNumber, amount);
                newOperation.IBPAID = transactionID;
                pendingExternalOperations.Add(newOperation);
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