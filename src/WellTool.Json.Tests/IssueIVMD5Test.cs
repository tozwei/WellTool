using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIVMD5 测试
    /// </summary>
    public class IssueIVMD5Test
    {
        [Fact]
        public void TestMD5Value()
        {
            var jsonStr = "{\"hash\":\"d41d8cd98f00b204e9800998ecf8427e\"}";
            var bean = JSONUtil.ToBean<HashBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(32, bean.Hash.Length);
        }

        public class HashBean
        {
            public string Hash { get; set; }
        }
    }
}
