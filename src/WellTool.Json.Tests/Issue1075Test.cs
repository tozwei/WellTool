using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue1075 测试 - 测试忽略大小写功能
    /// </summary>
    public class Issue1075Test
    {
        [Fact]
        public void TestToBean()
        {
            var jsonStr = "{\"f1\":\"f1\",\"f2\":\"f2\",\"fac\":\"fac\"}";
            
            // 在不忽略大小写的情况下，f2、fac都不匹配
            var o2 = JSONUtil.ToBean<ObjA>(jsonStr);
            Assert.NotNull(o2);
            Assert.Equal("f1", o2.F1);
        }

        [Fact]
        public void TestToBeanIgnoreCase()
        {
            var jsonStr = "{\"f1\":\"f1\",\"f2\":\"f2\",\"fac\":\"fac\"}";
            
            // 在忽略大小写的情况下，f2、fac都匹配
            var config = JSONConfig.Create().SetIgnoreCase(true);
            var json = JSONUtil.ParseObj(jsonStr, config);
            var o2 = json.ToBean<ObjA>();
            
            Assert.NotNull(o2);
            Assert.Equal("fac", o2.FAC);
            Assert.Equal("f2", o2.F2);
        }

        #region 辅助类

        private class ObjA
        {
            public string F1 { get; set; }
            public string F2 { get; set; }
            public string FAC { get; set; }
        }

        #endregion
    }
}
