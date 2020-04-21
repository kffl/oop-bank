using System;

namespace OOPBank
{
    public class Money
    {
        public readonly long cents;
        public readonly long dollars;


        public Money(long dollars = 0, long cents = 0)
        {
            if (cents < 0 || cents > 99) throw new Exception("Cents value must be greater than 0 and lower than 99");
            this.dollars = dollars;
            this.cents = cents;
        }

        public Money(double amount)
        {
            var decimalPart = (long)Math.Floor(amount);
            dollars = decimalPart;
            cents = (long) (amount - decimalPart);
        }


        public double asDouble => dollars + cents * 0.01;


        public static Money operator +(Money a, Money b)
        {
            var dollarsSum = a.dollars + b.dollars;
            var centsSum = a.cents + b.cents;
            if (centsSum > 99)
            {
                centsSum -= 100;
                dollarsSum++;
            }

            return new Money(dollarsSum, centsSum);
        }

        public static Money operator -(Money a, Money b)
        {
            var dollarsResult = a.dollars - b.dollars;
            var centsResult = a.cents - b.cents;
            if (centsResult < 0)
            {
                dollarsResult--;
                centsResult += 100;
            }

            return new Money(dollarsResult, centsResult);
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