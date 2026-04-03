using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2131 测试 - 测试布尔值处理
    /// </summary>
    public class Issue2131Test
    {
        [Fact]
        public void TestBooleanTrue()
        {
            var jsonStr = "{\"enabled\":true}";
            var bean = JSONUtil.ToBean<BoolBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.True(bean.Enabled);
        }

        [Fact]
        public void TestBooleanFalse()
        {
            var jsonStr = "{\"enabled\":false}";
            var bean = JSONUtil.ToBean<BoolBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.False(bean.Enabled);
        }

        [Fact]
        public void TestBooleanFromNumber()
        {
            var jsonStr = "{\"enabled\":1}";
            var bean = JSONUtil.ToBean<BoolBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestSerializeBool()
        {
            var bean = new BoolBean { Enabled = true };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("true", jsonStr.ToLower());
        }

        public class BoolBean
        {
            public bool Enabled { get; set; }
        }
    }
}
