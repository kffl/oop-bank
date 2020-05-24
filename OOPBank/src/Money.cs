using System;

namespace OOPBank
{
    public class Money
    {
        public readonly long cents;

        public readonly long dollars;


        public Money(long dollars = 0, long cents = 0)
        {
            if (dollars == 0 && (cents < -99 || cents > 99))
                throw new Exception(
                    "Cents value must be greater than -99 and lower than 99, when dollars are equal to 0.");
            if (dollars != 0 && (cents < 0 || cents > 99))
                throw new Exception(
                    "Cents value must be greater than 0 and lower than 99, when dollars are not equal to 0.");
            this.dollars = dollars;
            this.cents = cents;
        }

        public Money(double amount)
        {
            var decimalPart = (long)(amount < 0 ? Math.Ceiling(amount) : Math.Floor(amount));
            dollars = decimalPart;
            var fractionPart = amount - decimalPart;
            cents = (long)(Math.Round(
                                fractionPart, 2, amount < 0 ? MidpointRounding.AwayFromZero : MidpointRounding.ToZero) *
                            100);
        }


        public double asDouble => dollars + Math.Abs(cents) * (isNegative ? -0.01 : 0.01);

        public bool isNegative => dollars < 0 || cents < 0;


        public static Money operator +(Money a, Money b)
        {
            return new Money(a.asDouble + b.asDouble);
        }

        public static Money operator -(Money a, Money b)
        {
            return new Money(a.asDouble - b.asDouble);
        }

        public static Money operator *(Money a, double b)
        {
            return new Money(a.asDouble * b);
        }

        public static bool operator <(Money a, Money b)
        {
            return a.dollars < b.dollars || a.dollars == b.dollars && a.cents < b.cents;
        }

        public static bool operator >(Money a, Money b)
        {
            return a.dollars > b.dollars || a.dollars == b.dollars && a.cents > b.cents;
        }

        public static bool operator <=(Money a, Money b)
        {
            return a.dollars < b.dollars || a.dollars == b.dollars && a.cents <= b.cents;
        }

        public static bool operator >=(Money a, Money b)
        {
            return a.dollars > b.dollars || a.dollars == b.dollars && a.cents >= b.cents;
        }

        public static bool operator <(Money a, double b)
        {
            return a.asDouble < b;
        }

        public static bool operator >(Money a, double b)
        {
            return a.asDouble > b;
        }

        public static bool operator <=(Money a, double b)
        {
            return a.asDouble <= b;
        }

        public static bool operator >=(Money a, double b)
        {
            return a.asDouble >= b;
        }
    }
}
