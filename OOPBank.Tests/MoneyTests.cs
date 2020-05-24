using NUnit.Framework;

namespace OOPBank.Tests
{
    [TestFixture]
    [DefaultFloatingPointTolerance(0.0000000001)]
    public class MoneyTests
    {
        [TestCase(0.32, ExpectedResult = 0.32)]
        [TestCase(0.01, ExpectedResult = 0.01)]
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(-0.01, ExpectedResult = -0.01)]
        [TestCase(-0.05, ExpectedResult = -0.05)]
        [TestCase(-10.99, ExpectedResult = -10.99)]
        [TestCase(0.555, ExpectedResult = 0.55)]
        [TestCase(0.9999, ExpectedResult = 0.99)]
        [TestCase(-0.555, ExpectedResult = -0.56)]
        [TestCase(-0.999, ExpectedResult = -1)]
        public double DoubleConversionDouble(double amount)
        {
            return new Money(amount).asDouble;
        }

        [TestCase(0, 32, ExpectedResult = 0.32)]
        [TestCase(0, 01, ExpectedResult = 0.01)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, -1, ExpectedResult = -0.01)]
        [TestCase(-10, 99, ExpectedResult = -10.99)]
        public double DoubleConversionDollarsCents(int dollars, int cents)
        {
            return new Money(dollars, cents).asDouble;
        }

        [TestCase(-0.01)]
        [TestCase(-0.99)]
        [TestCase(-10.01)]
        [TestCase(-10.01)]
        [TestCase(-100)]
        public void NegativeValues(double amount)
        {
            Assert.True(new Money(amount).isNegative);
            Assert.False(new Money(-amount).isNegative);
        }


        [TestCase(0.32, 0.67, ExpectedResult = 0.99)]
        [TestCase(0.66, 0.5, ExpectedResult = 1.16)]
        [TestCase(1253.99, 563.99, ExpectedResult = 1817.98)]
        [TestCase(1253.99, -563.99, ExpectedResult = 690)]
        [TestCase(-1253.99, 563.99, ExpectedResult = -690)]
        [TestCase(0, 0.01, ExpectedResult = 0.01)]
        [TestCase(-0.01, 0.01, ExpectedResult = 0)]
        public double AdditionDouble(double a, double b)
        {
            return (new Money(a) + new Money(b)).asDouble;
        }

        [TestCase(0, 32, 0, 67, ExpectedResult = 0.99)]
        [TestCase(0, 66, 0, 50, ExpectedResult = 1.16)]
        [TestCase(1253, 99, 563, 99, ExpectedResult = 1817.98)]
        [TestCase(1253, 99, -563, 99, ExpectedResult = 690)]
        [TestCase(-1253, 99, 563, 99, ExpectedResult = -690)]
        [TestCase(0, 0, 0, 1, ExpectedResult = 0.01)]
        [TestCase(0, -1, 0, 1, ExpectedResult = 0)]
        public double AdditionDollarsCents(int aDollars, int aCents, int bDollars, int bCents)
        {
            return (new Money(aDollars, aCents) + new Money(bDollars, bCents)).asDouble;
        }


        [TestCase(0.32, 0.67, ExpectedResult = -0.35)]
        [TestCase(0.66, 0.5, ExpectedResult = 0.16)]
        [TestCase(1253.99, 563.99, ExpectedResult = 690)]
        [TestCase(1253.99, -563.99, ExpectedResult = 1817.98)]
        [TestCase(-1253.99, 563.99, ExpectedResult = -1817.98)]
        [TestCase(0, 0.01, ExpectedResult = -0.01)]
        [TestCase(-0.01, 0.01, ExpectedResult = -0.02)]
        public double SubtractionDouble(double a, double b)
        {
            return (new Money(a) - new Money(b)).asDouble;
        }

        [TestCase(0, 32, 0, 67, ExpectedResult = -0.35)]
        [TestCase(0, 66, 0, 50, ExpectedResult = 0.16)]
        [TestCase(1253, 99, 563, 99, ExpectedResult = 690)]
        [TestCase(1253, 99, -563, 99, ExpectedResult = 1817.98)]
        [TestCase(-1253, 99, 563, 99, ExpectedResult = -1817.98)]
        [TestCase(0, 0, 0, 1, ExpectedResult = -0.01)]
        [TestCase(0, -1, 0, 1, ExpectedResult = -0.02)]
        public double SubtractionDollarsCents(int aDollars, int aCents, int bDollars, int bCents)
        {
            return (new Money(aDollars, aCents) - new Money(bDollars, bCents)).asDouble;
        }

        [TestCase(0.32, 0.67, ExpectedResult = 0.21)]
        [TestCase(542.99, 0.5, ExpectedResult = 271.49)]
        [TestCase(-121.68, 2.54, ExpectedResult = -309.07)]
        [TestCase(121.68, -2.54, ExpectedResult = -309.07)]
        [TestCase(121.68, 0, ExpectedResult = 0)]
        public double Multiplication(double money, double factor)
        {
            return (new Money(money) * factor).asDouble;
        }

        [TestCase(10, 0, 9, 99)]
        [TestCase(0, 0, 0, -1)]
        [TestCase(10, 23, -2, 4)]
        [TestCase(10, 11, -11, 11)]
        public void ComparisonALargerThanB(int aDollars, int aCents, int bDollars, int bCents)
        {
            var a = new Money(aDollars, aCents);
            var b = new Money(bDollars, bCents);
            Assert.True(a > b);
            Assert.False(a <= b);
            Assert.True(b < a);
            Assert.False(b >= a);
        }
    }
}