using System.Collections.Generic;

namespace OOPBank.Classes.IBPA
{
    //all the stuff that IBPA needs to interact with a bank
    public interface IBankColleague
    {
        /// <summary>
        /// Each IBPA client/colleague has to have it's unique account prefix
        /// </summary>
        /// <value>Bank's account prefix</value>
        string accountPrefix { get; }
        public List<LocalAccount> getAccounts();
    }
}