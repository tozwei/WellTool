using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3795 测试
    /// </summary>
    public class Issue3795Test
    {
        [Fact]
        public void TestGetterOnly()
        {
            var bean = new GetterBean();
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("Value", jsonStr);
        }

        public class GetterBean
        {
            public string Value => "computed";
        }
    }
}
