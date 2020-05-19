using System.Collections.Generic;

namespace OOPBank
{
    public class Account
    {
        public Account(string number)
        {
            accountNumber = number;
        }

        public string accountNumber { get; }
        public List<Operation> IncomingOperations { get; } = new List<Operation>();
        public List<Operation> OutgoingOperations { get; } = new List<Operation>();
    }
}