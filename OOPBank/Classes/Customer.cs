namespace OOPBank
{
    public class Customer
    {
        private string FirstName  { get; }
        private string LastName { get; }

        public Customer(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
    }
}