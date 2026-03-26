using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Core.Tests
{
    public class CoreTests
    {
        [Fact]
        public void TestStringUtil()
        {
            var result = WellTool.Core.Lang.StringUtil.IsBlank("");
            Assert.True(result);
        }

        [Fact]
        public void TestDateUtil()
        {
            var now = WellTool.Core.Date.DateUtil.Now();
            Assert.True(now != default(DateTime));
        }

        [Fact]
        public void TestBeanUtil()
        {
            var testObj = new TestObject { Name = "Test", Age = 10 };
            var dict = WellTool.Core.Bean.BeanUtil.BeanToMap(testObj);
            Assert.NotNull(dict);
            Assert.Contains("Name", dict);
            Assert.Contains("Age", dict);
            Assert.Equal("Test", dict["Name"]);
            Assert.Equal(10, dict["Age"]);
        }

        [Fact]
        public void TestCollUtil()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 3, 4, 5 };
            var union = WellTool.Core.Collection.CollUtil.Union(list1, list2);
            Assert.Equal(5, union.Count);
        }

        [Fact]
        public void TestBase64()
        {
            var original = "Hello, World!";
            var encoded = WellTool.Core.Codec.Base64.Encode(original);
            var decoded = WellTool.Core.Codec.Base64.DecodeStr(encoded);
            Assert.Equal(original, decoded);
        }

        private class TestObject
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
        }
    }
}
