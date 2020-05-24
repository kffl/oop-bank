using System.Collections.Generic;
using OOPBank.IBPA;
using System.Linq;

namespace OOPBank
{
    public class Bank : IBankColleague
    {
        private static long _lastAccountNumber;

        private readonly List<LocalAccount> accounts = new List<LocalAccount>();

        private readonly List<Customer> customers = new List<Customer>();

        public readonly IBankMediator IBPA;

        public string accountPrefix { get; }

        public Bank(string name, string accountPrefix)
        {
            Name = name;
            this.accountPrefix = accountPrefix;
            //the bank has to register itself to IBPA
            IBPA = InterBankPaymentAgency.getInterBankPaymentAgency();
            IBPA.registerBank(this);
        }


        public string name { get; }


        public void simulateNewDay()
        {
            foreach (var account in accounts) account.handleNewDay();
        }

        public string Name { get; }

        public List<LocalAccount> getAccounts()
        {
            return accounts;
        }

        internal void addAccount(LocalAccount newAccount)
        {
            accounts.Add(newAccount);
        }

        public void addCustomer(Customer newCustomer)
        {
            customers.Add(newCustomer);
        }

        internal string generateAccountNumber()
        {
            return accountPrefix + (++_lastAccountNumber).ToString("D8");
        }

        public List<LocalAccount> getCustomerAccounts(Customer customer)
        {
            return accounts.Where(item => item.owner == customer).ToList();
        }
    }
}
