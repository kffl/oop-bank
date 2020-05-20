using System;
using System.Collections.Generic;
using System.Text;

namespace OOPBank.Classes
{
    public interface ILocalAccount
    {
        Money balance { get; set; }
        void withdrawMoney(Money amount);
    }
}
