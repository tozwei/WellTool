using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONStrFormatter 测试
    /// </summary>
    public class JSONStrFormatterTest
    {
        [Fact]
        public void TestFormat()
        {
            var jsonStr = "{\"id\":1,\"name\":\"test\",\"items\":[1,2,3]}";
            var formatted = JSONStrFormatter.Format(jsonStr);
            Assert.NotNull(formatted);
            Assert.Contains("\n", formatted);
            Assert.Contains("  ", formatted);
        }

        [Fact]
        public void TestFormatWithIndent()
        {
            var jsonStr = "{\"id\":1,\"name\":\"test\"}";
            var formatted = JSONStrFormatter.Format(jsonStr, JSONStrFormatter.DEFAULT_INDENT);
            Assert.NotNull(formatted);
            Assert.Contains("  ", formatted);
        }

        [Fact]
        public void TestFormatArray()
        {
            var jsonStr = "[1,2,3,4,5]";
            var formatted = JSONStrFormatter.Format(jsonStr);
            Assert.NotNull(formatted);
            Assert.Contains("\n", formatted);
        }

        [Fact]
        public void TestFormatNested()
        {
            var jsonStr = "{\"outer\":{\"inner\":{\"value\":123}}}";
            var formatted = JSONStrFormatter.Format(jsonStr);
            Assert.NotNull(formatted);
            Assert.Contains("  ", formatted);
            Assert.Contains("\"inner\"", formatted);
        }

        [Fact]
        public void TestFormatPretty()
        {
            var jsonStr = "{\"id\":1}";
            var formatted = JSONStrFormatter.Format(jsonStr, JSONStrFormatter.DEFAULT_INDENT);
            Assert.NotNull(formatted);
            Assert.Contains("\n", formatted);
        }
    }
}
