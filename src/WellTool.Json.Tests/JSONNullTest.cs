using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONNull 单元测试
    /// </summary>
    public class JSONNullTest
    {
        [Fact]
        public void NullValueTest()
        {
            Assert.True(JSONNull.IsNull(null));
            Assert.True(JSONNull.IsNull(JSONNull.NULL));
            Assert.False(JSONNull.IsNull("test"));
        }

        [Fact]
        public void NullInJSONTest()
        {
            var json = "{\"name\":null,\"value\":\"test\"}";
            var obj = JSONUtil.ParseObj(json);
            
            Assert.NotNull(obj);
            Assert.True(JSONNull.IsNull(obj["name"]));
            Assert.Equal("test", obj["value"]);
        }

        [Fact]
        public void NullArrayTest()
        {
            var json = "[null, \"value1\", null, \"value2\"]";
            var array = JSONUtil.ParseArray(json);
            
            Assert.Equal(4, array.Count);
            Assert.True(JSONNull.IsNull(array[0]));
            Assert.Equal("value1", array[1]);
            Assert.True(JSONNull.IsNull(array[2]));
            Assert.Equal("value2", array[3]);
        }

        [Fact]
        public void NullSerializationTest()
        {
            var obj = JSONUtil.CreateObj();
            obj.Set("name", "test");
            obj.Set("nullValue", null);
            
            var json = obj.ToJSONString();
            Assert.Contains("name", json);
            Assert.Contains("test", json);
        }

        [Fact]
        public void NullEqualityTest()
        {
            Assert.Equal(JSONNull.NULL, JSONNull.NULL);
            var nullStr = JSONNull.NULL.ToString();
            Assert.Equal("null", nullStr);
        }

        [Fact]
        public void NullToStringTest()
        {
            Assert.Equal("null", JSONNull.NULL.ToString());
        }
    }
}
