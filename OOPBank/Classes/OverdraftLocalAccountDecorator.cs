using System;
using System.Collections.Generic;
using System.Text;

namespace OOPBank.Classes
{
    public class OverdraftLocalAccountDecorator : LocalAccountDecorator
    {
        public OverdraftLocalAccountDecorator(ILocalAccount component) : base(component)
        {
        }

        public override void withdrawMoney(Money amount)
        {
            balance -= amount;
        }
    }
}
