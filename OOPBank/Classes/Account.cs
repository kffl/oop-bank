using System.Collections.Generic;
using OOPBank.Classes;

namespace OOPBank
{
    public class Account
    {
        public virtual string accountNumber { get; }
        public virtual List<Operation> IncomingOperations { get; } = new List<Operation>();
        public virtual List<Operation> OutgoingOperations { get; } = new List<Operation>();

        public Account()
        {
        }
        public Account(string number)
        {
            accountNumber = number;
        }
    }
}