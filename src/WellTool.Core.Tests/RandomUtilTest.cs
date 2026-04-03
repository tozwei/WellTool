using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Random工具单元测试
    /// </summary>
    public class RandomUtilTest
    {
        [Fact]
        public void RandomIntTest()
        {
            var value = RandomUtil.RandomInt(1, 10);
            Assert.InRange(value, 1, 9);
        }

        [Fact]
        public void RandomIntMaxTest()
        {
            var value = RandomUtil.RandomInt(10);
            Assert.InRange(value, 0, 9);
        }

        [Fact]
        public void RandomLongTest()
        {
            var value = RandomUtil.RandomLong();
            Assert.True(value >= 0);
        }

        [Fact]
        public void RandomLongMaxTest()
        {
            var value = RandomUtil.RandomLong(100);
            Assert.InRange(value, 0, 99);
        }

        [Fact]
        public void RandomDoubleTest()
        {
            var value = RandomUtil.RandomDouble();
            Assert.InRange(value, 0.0, 1.0);
        }

        [Fact]
        public void RandomDoubleMaxTest()
        {
            var value = RandomUtil.RandomDouble(10.0);
            Assert.InRange(value, 0.0, 10.0);
        }

        [Fact]
        public void RandomBytesTest()
        {
            var bytes = RandomUtil.RandomBytes(16);
            Assert.Equal(16, bytes.Length);
        }

        [Fact]
        public void RandomStringTest()
        {
            var str = RandomUtil.RandomString(10);
            Assert.Equal(10, str.Length);
        }

        [Fact]
        public void RandomStringLowerTest()
        {
            var str = RandomUtil.RandomStringLower(10);
            Assert.Equal(10, str.Length);
            Assert.True(str.All(char.IsLower));
        }

        [Fact]
        public void RandomStringUpperTest()
        {
            var str = RandomUtil.RandomStringUpper(10);
            Assert.Equal(10, str.Length);
            Assert.True(str.All(char.IsUpper));
        }

        [Fact]
        public void RandomNumbersTest()
        {
            var numbers = RandomUtil.RandomNumbers(10);
            Assert.Equal(10, numbers.Length);
            Assert.True(numbers.All(char.IsDigit));
        }

        [Fact]
        public void RandomEleTest()
        {
            var list = new[] { "a", "b", "c", "d", "e" };
            var ele = RandomUtil.RandomEle(list);
            Assert.Contains(ele, list);
        }

        [Fact]
        public void RandomEleListTest()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var ele = RandomUtil.RandomEle(list);
            Assert.Contains(ele, list);
        }

        [Fact]
        public void RandomEleSetTest()
        {
            var set = new HashSet<int> { 1, 2, 3, 4, 5 };
            var ele = RandomUtil.RandomEleSet(set);
            Assert.Contains(ele, set);
        }

        [Fact]
        public void RandomIntSetTest()
        {
            var set = RandomUtil.RandomIntSet(5, 1, 10);
            Assert.Equal(5, set.Count);
            Assert.True(set.All(v => v >= 1 && v <= 10));
        }

        [Fact]
        public void RandomUUIDTest()
        {
            var uuid = RandomUtil.RandomUUID();
            Assert.Equal(32, uuid.Length);
        }
    }
}
