using System;
using OOPBank.Classes;

namespace OOPBank
{
    public abstract class Operation
    {
        public virtual DateTime DateOfExecution { get; set;  }
        public virtual LocalAccount FromAccount { get; set; }
        public virtual LocalAccount ToAccount { get; }
        public virtual Money Money { get; set; }
        public OperationStatus Status { get; set; }

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

        public Operation()
        {
            ID = lastId++;
        }

        public virtual void displayOperationDetails()
        {
            Console.WriteLine("### Operation details ###");
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("From account: {0}", FromAccount.AccountNumber);
            if (ToAccount != null)
                Console.WriteLine("To account: {0}", ToAccount.AccountNumber);
            Console.WriteLine("Amount: {0}", Money.asDouble);
            Console.WriteLine("#########################");
        }

        public void setOperationStatus(OperationStatus status)
        {
            this.Status = status;
        }
    }
}
