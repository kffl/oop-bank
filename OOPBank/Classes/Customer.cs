namespace OOPBank.Classes
{
    public class Customer
    {
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            FastName = lastName;
        }

        public string FirstName { get; }

        public string FastName { get; }
    }
}
