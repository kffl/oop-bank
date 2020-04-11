using System;

namespace OOPBank
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var Bank1 = new Bank("Bank1", "B1");
            var SuperBank = new Bank("SuperBank", "SB");
            var JohnDoe = new Customer("John", "Doe");
            Bank1.addCustomer(JohnDoe);
            Bank1.openAccount(JohnDoe);
        }

        public static int AddInts(int a, int b)
        {
            return a + b;
        }
    }
}
