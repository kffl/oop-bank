using System.Collections.Generic;

namespace OOPBank
{
    public class Account
    {
        protected List<Operation> incomingOperations;
        protected List<Operation> outgoingOperations;
        protected Customer owner;
        public string accountNumber { get; }
        protected Money balance { get; set; }

        public Account(Customer owner, string number)
        {
            this.owner = owner;
            this.accountNumber = number;
        }

        public bool hasSufficientBalance(Money money)
        {
            return (balance - money).Amount >= 0;
        }

        public void bookOutgoingOperation(Operation operation)
        {
            outgoingOperations.Add(operation);
            balance = balance-operation.money;
        }

        public void bookIncomingOperation(Operation operation)
        {
            incomingOperations.Add(operation);
            balance = balance+operation.money;
        }

        public Money getBalance()
        {
            return balance;
        }
    }
}