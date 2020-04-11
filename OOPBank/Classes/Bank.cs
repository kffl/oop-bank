using System.Collections.Generic;

namespace OOPBank
{
    public class Bank
    {
        private string Name { get; }
        private List<Customer> Customers;

        public Bank(string Name)
        {
            this.Name = Name;
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer newCustomer)
        {
            Customers.Add(newCustomer);
        }
    }
}