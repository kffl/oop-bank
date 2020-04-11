using System.Collections.Generic;

namespace OOPBank
{
    public class Account
    {
        protected List<Operation> Operations;
        protected Customer Owner;
        protected string AccountNumber { get; }
        protected Money Balance { get; }
    }
}