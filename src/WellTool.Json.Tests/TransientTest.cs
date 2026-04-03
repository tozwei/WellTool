using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Transient 字段测试
    /// </summary>
    public class TransientTest
    {
        [Fact]
        public void TestTransientField()
        {
            var obj = new TransientBean
            {
                Id = 1,
                Name = "test",
                TransientValue = "shouldNotSerialize"
            };

            var jsonStr = JSONUtil.ToJsonStr(obj);
            Assert.DoesNotContain("shouldNotSerialize", jsonStr);
            Assert.Contains("\"Id\":1", jsonStr);
            Assert.Contains("\"Name\":\"test\"", jsonStr);
        }

        [Fact]
        public void TestTransientDeserialize()
        {
            var jsonStr = "{\"Id\":1,\"Name\":\"test\",\"TransientValue\":\"test\"}";
            var bean = JSONUtil.ToBean<TransientBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(1, bean.Id);
            Assert.Equal("test", bean.Name);
        }

        public class TransientBean
        {
            public int Id { get; set; }
            public string Name { get; set; }
            [JsonIgnore]
            public string TransientValue { get; set; }
        }
    }
}
