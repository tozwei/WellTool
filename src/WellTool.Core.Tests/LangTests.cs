using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Core.Tests
{
    public class LangTests
    {
        [Fact]
        public void TestSingleton()
        {
            var instance1 = WellTool.Core.Lang.Singleton.Get<TestObject>();
            var instance2 = WellTool.Core.Lang.Singleton.Get<TestObject>();
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void TestSnowflake()
        {
            var snowflake = new WellTool.Core.Lang.Snowflake(1, 1);
            var id1 = snowflake.NextId();
            var id2 = snowflake.NextId();
            Assert.NotEqual(id1, id2);
        }

        [Fact]
        public void TestObjectUtil()
        {
            var obj1 = new object();
            var obj2 = obj1;
            var obj3 = new object();
            
            Assert.True(WellTool.Core.Lang.ObjectUtil.Equals(obj1, obj2));
            Assert.False(WellTool.Core.Lang.ObjectUtil.Equals(obj1, obj3));
            Assert.True(WellTool.Core.Lang.ObjectUtil.Equals(null, null));
            Assert.False(WellTool.Core.Lang.ObjectUtil.Equals(obj1, null));
        }

        [Fact]
        public void TestPair()
        {
            var pair = new WellTool.Core.Lang.Pair<string, int>("test", 123);
            Assert.Equal("test", pair.Key);
            Assert.Equal(123, pair.Value);
        }

        [Fact]
        public void TestAssert()
        {
            // 测试Assert.NotNull
            var obj = new object();
            Assert.Throws<ArgumentNullException>(() => WellTool.Core.Lang.Assert.NotNull(null, "Object cannot be null"));
            
            // 测试Assert.True
            Assert.Throws<ArgumentException>(() => WellTool.Core.Lang.Assert.True(false, "Condition must be true"));
        }

        private class TestObject
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            
            private TestObject() { }
        }
    }
}
