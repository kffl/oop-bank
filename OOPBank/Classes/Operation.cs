using System;

namespace OOPBank
{
    public class Operation
    {
        public DateTime DateOfExecution { get; }
        public Account FromAccount;
        public Account ToAccount;
        public Money Money;
        protected long ID;
        private static long lastId = 0;

        public enum OperationStatus
        {
            Disposed,
            Rejected,
            Accepted,
            Cancelled,
            Queued,
            PendingCompletion,
            Completed
        }

        protected OperationStatus status;

        public Operation(Account fromAccount, Account toAccount, Money money)
        {
            this.Money = money;
            this.FromAccount = fromAccount;
            this.ToAccount = toAccount;
            ID = ++lastId;
            DateOfExecution = new DateTime();
            status = OperationStatus.Disposed;
        }

        public Operation(Account fromAccount, Money money)
        {
            this.Money = money;
            this.FromAccount = fromAccount;
            ToAccount = null;
            ID = ++lastId;
            DateOfExecution = new DateTime();
            status = OperationStatus.Disposed;
        }

        public void displayOperationDetails()
        {
            Console.WriteLine("### Operation details ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("From account: {0}", FromAccount.accountNumber);
            if (ToAccount != null)
                Console.WriteLine("To account: {0}", ToAccount.accountNumber);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("#########################");
        }

        public void setOperationStatus(OperationStatus status)
        {
            this.status = status;
        }
    }
}