using System.Collections.Generic;

namespace OOPBank
{
    public class Account
    {
        public string accountNumber { get; }

        public Account()
        {

        }
        public Account(string number)
        {
            this.accountNumber = number;
        }
    }
}