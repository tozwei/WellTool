using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Number工具单元测试
    /// </summary>
    public class NumberUtilTest
    {
        [Fact]
        public void AddTest()
        {
            Assert.Equal(7.37, NumberUtil.Add(3.15f, 4.22), 2);
        }

        [Fact]
        public void AddTest2()
        {
            Assert.Equal(7.37, NumberUtil.Add(3.15f, 4.22), 0.01);
        }

        [Fact]
        public void AddTest3()
        {
            Assert.Equal(14.74, NumberUtil.Add(3.15f, 4.22, 3.15f, 4.22), 0);
        }

        [Fact]
        public void IsIntegerTest()
        {
            Assert.True(NumberUtil.IsInteger("-12"));
            Assert.True(NumberUtil.IsInteger("256"));
            Assert.True(NumberUtil.IsInteger("0"));
            Assert.False(NumberUtil.IsInteger("23.4"));
            Assert.False(NumberUtil.IsInteger(null));
            Assert.False(NumberUtil.IsInteger(""));
        }

        [Fact]
        public void IsLongTest()
        {
            Assert.True(NumberUtil.IsLong("-12"));
            Assert.True(NumberUtil.IsLong("256"));
            Assert.True(NumberUtil.IsLong("0"));
            Assert.False(NumberUtil.IsLong("23.4"));
            Assert.False(NumberUtil.IsLong(null));
        }

        [Fact]
        public void IsNumberTest()
        {
            Assert.True(NumberUtil.IsNumber("28.55"));
            Assert.True(NumberUtil.IsNumber("0"));
            Assert.True(NumberUtil.IsNumber("+100.10"));
            Assert.True(NumberUtil.IsNumber("-22.022"));
        }

        [Fact]
        public void DivTest()
        {
            Assert.Equal(0, NumberUtil.Div(0, 1), 0);
            Assert.Equal(2, NumberUtil.Div(6, 3), 0);
        }

        [Fact]
        public void MulTest()
        {
            Assert.Equal(6, NumberUtil.Mul(2, 3));
            Assert.Equal(12, NumberUtil.Mul(2, 3, 2));
        }

        [Fact]
        public void SubTest()
        {
            Assert.Equal(1, NumberUtil.Sub(3, 2));
        }

        [Fact]
        public void RoundTest()
        {
            Assert.Equal(2, NumberUtil.Round(2.5, 0));
            Assert.Equal(2, NumberUtil.Round(2.4, 0));
            Assert.Equal(2.4, NumberUtil.Round(2.45, 1), 1);
        }

        [Fact]
        public void MaxTest()
        {
            Assert.Equal(5, NumberUtil.Max(1, 3, 5, 2));
        }

        [Fact]
        public void MinTest()
        {
            Assert.Equal(1, NumberUtil.Min(1, 3, 5, 2));
        }

        [Fact]
        public void IsBetweenTest()
        {
            Assert.True(NumberUtil.IsBetween(3, 1, 5));
            Assert.True(NumberUtil.IsBetween(1, 1, 5)); // 边界
            Assert.True(NumberUtil.IsBetween(5, 1, 5)); // 边界
            Assert.False(NumberUtil.IsBetween(0, 1, 5));
            Assert.False(NumberUtil.IsBetween(6, 1, 5));
        }

        [Fact]
        public void IsPrimeTest()
        {
            Assert.True(NumberUtil.IsPrime(2));
            Assert.True(NumberUtil.IsPrime(3));
            Assert.True(NumberUtil.IsPrime(5));
            Assert.True(NumberUtil.IsPrime(7));
            Assert.False(NumberUtil.IsPrime(4));
            Assert.False(NumberUtil.IsPrime(9));
        }

        [Fact]
        public void FactorialTest()
        {
            Assert.Equal(1, NumberUtil.Factorial(0));
            Assert.Equal(1, NumberUtil.Factorial(1));
            Assert.Equal(2, NumberUtil.Factorial(2));
            Assert.Equal(6, NumberUtil.Factorial(3));
        }

        [Fact]
        public void SqrtTest()
        {
            Assert.Equal(3, NumberUtil.Sqrt(9), 0);
            Assert.Equal(10, NumberUtil.Sqrt(100), 0);
        }

        [Fact]
        public void AbsTest()
        {
            Assert.Equal(5, NumberUtil.Abs(-5));
            Assert.Equal(5.5, NumberUtil.Abs(-5.5), 0);
        }

        [Fact]
        public void PowTest()
        {
            Assert.Equal(8, NumberUtil.Pow(2, 3));
            Assert.Equal(1, NumberUtil.Pow(5, 0));
            Assert.Equal(9, NumberUtil.Pow(3, 2));
        }
    }
}
