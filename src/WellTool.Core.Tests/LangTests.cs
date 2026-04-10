using System;
using System.Collections.Generic;
using Xunit;
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests
{
    public class LangTests
    {
        [Fact]
        public void TestSingleton()
        {
            // 测试Singleton类的单例特性
            // 与Java版本的getTest()方法对应
            var instance1 = WellTool.Core.Lang.Singleton.Get<TestBean>();
            var instance2 = WellTool.Core.Lang.Singleton.Get<TestBean>();
            
            // 验证两个实例是同一个对象
            XAssert.Same(instance1, instance2);
            
            // 测试带key的单例
            var instance3 = WellTool.Core.Lang.Singleton.Get<string>("test_key", () => "test_value");
            var instance4 = WellTool.Core.Lang.Singleton.Get<string>("test_key", () => "test_value");
            XAssert.Same(instance3, instance4);
            XAssert.Equal("test_value", instance3);
        }

        [Fact]
        public void TestSnowflake()
        {
            var snowflake = new WellTool.Core.Lang.Snowflake(1, 1);
            var id1 = snowflake.NextId();
            var id2 = snowflake.NextId();
            XAssert.NotEqual(id1, id2);
        }

        [Fact]
        public void TestObjectUtil()
        {
            var obj1 = new object();
            var obj2 = obj1;
            var obj3 = new object();
            
            XAssert.True(WellTool.Core.Lang.ObjectUtil.Equals(obj1, obj2));
            XAssert.False(WellTool.Core.Lang.ObjectUtil.Equals(obj1, obj3));
            XAssert.True(WellTool.Core.Lang.ObjectUtil.Equals(null, null));
            XAssert.False(WellTool.Core.Lang.ObjectUtil.Equals(obj1, null));
        }

        [Fact]
        public void TestPair()
        {
            var pair = new WellTool.Core.Lang.Pair<string, int>("test", 123);
            XAssert.Equal("test", pair.Key);
            XAssert.Equal(123, pair.Value);
        }

        [Fact]
        public void TestAssert()
        {
            // 测试Assert.NotNull
            var obj = new object();
            XAssert.Throws<ArgumentNullException>(() => WellTool.Core.Lang.AssertUtil.NotNull(null, "Object cannot be null"));
            
            // 测试Assert.IsTrue
            XAssert.Throws<ArgumentException>(() => WellTool.Core.Lang.AssertUtil.IsTrue(false, () => new ArgumentException("Condition must be true")));
        }

        private class TestObject
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            
            private TestObject() { }
        }

        private class TestBean
        {
            public string Name { get; set; } = string.Empty;
            public string Age { get; set; } = string.Empty;
        }

        [Fact]
        public void TestConsole()
        {
            // 测试Console类的基本功能
            // 这里我们只测试方法是否能正常调用，不测试实际输出
            System.Console.WriteLine("Test Console");
            System.Console.Write("Test Console");
        }

        [Fact]
        public void TestStringUtil()
        {
            // 测试StringUtil的基本功能
            string str = "  test  ";
            string trimmed = WellTool.Core.Lang.StringUtil.Trim(str);
            XAssert.Equal("test", trimmed);

            string emptyStr = string.Empty;
            bool isEmpty = WellTool.Core.Lang.StringUtil.IsEmpty(emptyStr);
            XAssert.True(isEmpty);

            bool isNotEmpty = WellTool.Core.Lang.StringUtil.IsNotEmpty(str);
            XAssert.True(isNotEmpty);
        }

        [Fact]
        public void HashTest()
        {
            string str = "test";

            // 测试MD5
            string md5 = WellTool.Crypto.CryptoUtil.MD5(str);
            XAssert.NotNull(md5);
            XAssert.Equal(32, md5.Length);

            // 测试SHA1
            string sha1 = WellTool.Crypto.CryptoUtil.SHA1(str);
            XAssert.NotNull(sha1);
            XAssert.Equal(40, sha1.Length);

            // 测试SHA256
            string sha256 = WellTool.Crypto.CryptoUtil.SHA256(str);
            XAssert.NotNull(sha256);
            XAssert.Equal(64, sha256.Length);
        }

        [Fact]
        public void TestFunc()
        {
            // 测试Func接口
            WellTool.Core.Lang.Func.Func1<string, int> func = (s) => s.Length;
            int length = func("test");
            XAssert.Equal(4, length);

            // 测试Func0接口
            WellTool.Core.Lang.Func.Func0<string> func0 = () => "test";
            string result = func0();
            XAssert.Equal("test", result);

            // 测试Func1接口
            WellTool.Core.Lang.Func.Func1<string, int> func1 = (s) => s.Length;
            int length1 = func1("test");
            XAssert.Equal(4, length1);
        }

        [Fact]
        public void TestSupplier()
        {
            // 测试Func0接口作为无参数供应者
            WellTool.Core.Lang.Func.Func0<string> supplier = () => "test";
            string result = supplier();
            XAssert.Equal("test", result);
        }
    }
}
