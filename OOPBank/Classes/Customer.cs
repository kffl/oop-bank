namespace OOPBank
{
    public class Customer
    {
        public string firstName  { get; }
        public string fastName { get; }

        public Customer(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.fastName = lastName;
        }
    }
}