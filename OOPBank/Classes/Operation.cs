using System;

namespace OOPBank
{
    public class Operation
    {
        protected DateTime dateOfExecution;
        protected Account fromAccount;
        protected Account toAccount;
        public Money money;
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
            this.money = money;
            this.fromAccount = fromAccount;
            this.toAccount = toAccount;
            ID = ++lastId;
            dateOfExecution = new DateTime();
            status = OperationStatus.Disposed;
        }

        public Operation(Account fromAccount, Money money)
        {
            this.money = money;
            this.fromAccount = fromAccount;
            toAccount = null;
            ID = ++lastId;
            dateOfExecution = new DateTime();
            status = OperationStatus.Disposed;
        }

        public void displayOperationDetails()
        {
            Console.WriteLine("### Operation details ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("From account: {0}", fromAccount.accountNumber);
            if (toAccount != null)
                Console.WriteLine("To account: {0}", toAccount.accountNumber);
            Console.WriteLine("Amount: {0}", money.asDouble);
            Console.WriteLine("#########################");
        }

        public void setOperationStatus(OperationStatus status)
        {
            this.status = status;
        }
    }
}