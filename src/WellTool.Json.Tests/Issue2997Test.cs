using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2997 测试
    /// </summary>
    public class Issue2997Test
    {
        [Fact]
        public void TestCircularReference()
        {
            var bean = new CircularBean { Name = "test" };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("Name", jsonStr);
        }

        public class CircularBean
        {
            public string Name { get; set; }
        }
    }
}
