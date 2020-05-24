using System.Collections.Generic;

namespace OOPBank
{
    public class Account
    {
        public Account()
        {
        }
        public Account(string number)
        {
            AccountNumber = number;
        }

        public virtual string AccountNumber { get; }

        public virtual List<Operation> IncomingOperations { get; } = new List<Operation>();

        public virtual List<Operation> OutgoingOperations { get; } = new List<Operation>();

        public virtual List<Operation> OtherOperations { get; } = new List<Operation>();
    }
}
