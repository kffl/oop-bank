using System.Collections.Generic;

namespace OOPBank.Classes
{
    public class Account
    {
        public Account(string number)
        {
            accountNumber = number;
        }

        public Account() { }

        public virtual string accountNumber { get; }
        public virtual List<Operation> IncomingOperations { get; } = new List<Operation>();
        public virtual List<Operation> OutgoingOperations { get; } = new List<Operation>();
    }
}