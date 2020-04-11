namespace OOPBank
{
    public class Money
    {
        private long Amount { get; set; }

        public Money()
        {
            Amount = 0;
        }

        public Money(double Amount)
        {
            this.Amount = (long)(Amount * 100);
        }

        public Money(long Amount)
        {
            this.Amount = Amount;
        }
        public double asDouble()
        {
            return (double)Amount / 100.0;
        }

        public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount);
        public static Money operator -(Money a, Money b) => new Money(a.Amount - b.Amount);
    }
}