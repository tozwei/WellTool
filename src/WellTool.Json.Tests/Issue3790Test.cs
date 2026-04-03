using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3790 测试
    /// </summary>
    public class Issue3790Test
    {
        [Fact]
        public void TestSetterOnly()
        {
            var bean = new SetterBean();
            bean.Name = "test";
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("Name", jsonStr);
        }

        public class SetterBean
        {
            public string Name { get; set; }
        }
    }
}
