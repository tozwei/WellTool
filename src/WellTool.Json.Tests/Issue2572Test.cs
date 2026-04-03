using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2572 测试 - 测试枚举序列化
    /// </summary>
    public class Issue2572Test
    {
        [Fact]
        public void TestEnumSerialize()
        {
            var bean = new EnumBean { Status = TestStatus.Active };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("Active", jsonStr);
        }

        [Fact]
        public void TestEnumDeserialize()
        {
            var jsonStr = "{\"Status\":\"Active\"}";
            var bean = JSONUtil.ToBean<EnumBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(TestStatus.Active, bean.Status);
        }

        [Fact]
        public void TestEnumAsNumber()
        {
            var jsonStr = "{\"Status\":1}";
            var bean = JSONUtil.ToBean<EnumBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public enum TestStatus
        {
            Active = 1,
            Inactive = 2,
            Deleted = 3
        }

        public class EnumBean
        {
            public TestStatus Status { get; set; }
        }
    }
}
