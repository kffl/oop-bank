using System.Collections.Generic;
using OOPBank.Classes;

namespace OOPBank
{
    public class Account
    {
        public string accountNumber { get; }
        public List<Operation> IncomingOperations { get; } = new List<Operation>();
        public List<Operation> OutgoingOperations { get; } = new List<Operation>();

        public Account()
        {
        }
        public Account(string number)
        {
            accountNumber = number;
        }
    }
}