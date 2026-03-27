using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Xunit;
using WellToolConvert = WellTool.Core.Convert;

namespace WellTool.Core.Tests
{
    public class CoreTests
    {
        [Fact]
        public void TestStringUtil()
        {
            var result = WellTool.Core.Lang.StringUtil.IsBlank("");
            Assert.True(result);
            
            var result2 = WellTool.Core.Lang.StringUtil.IsBlank("  ");
            Assert.True(result2);
            
            var result3 = WellTool.Core.Lang.StringUtil.IsBlank("test");
            Assert.False(result3);
        }

        [Fact]
        public void TestDateUtil()
        {
            var now = WellTool.Core.Date.DateUtil.Now();
            Assert.True(now != default(DateTime));
            
            var today = WellTool.Core.Date.DateUtil.Today();
            Assert.Equal(DateTime.Today.ToString("yyyy-MM-dd"), today);
        }

        [Fact]
        public void TestBeanUtil()
        {
            var testObj = new TestObject { Name = "Test", Age = 10 };
            var dict = WellTool.Core.Bean.BeanUtil.BeanToMap(testObj);
            Assert.NotNull(dict);
            Assert.Contains("Name", dict);
            Assert.Contains("Age", dict);
            Assert.Equal("Test", dict["Name"]);
            Assert.Equal(10, dict["Age"]);
        }

        [Fact]
        public void TestCollUtil()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 3, 4, 5 };
            var union = WellTool.Core.Collection.CollUtil.Union(list1, list2);
            Assert.Equal(5, union.Count);
            
            var intersection = WellTool.Core.Collection.CollUtil.Intersection(list1, list2);
            Assert.Single(intersection);
            Assert.Equal(3, intersection.First());
        }

        [Fact]
        public void TestBase64()
        {
            var original = "Hello, World!";
            var encoded = WellTool.Core.Codec.Base64.Encode(original);
            var decoded = WellTool.Core.Codec.Base64.DecodeStr(encoded);
            Assert.Equal(original, decoded);
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
        public void TestMathUtil()
        {
            Assert.Equal(5, WellTool.Core.MathUtil.Max(3, 5));
            Assert.Equal(3, WellTool.Core.MathUtil.Min(3, 5));
            Assert.Equal(5, WellTool.Core.MathUtil.Abs(-5));
            Assert.Equal(8, WellTool.Core.MathUtil.Pow(2, 3));
        }

        [Fact]
        public void TestFileUtil()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                WellTool.Core.IO.FileUtil.WriteString(tempFile, "Test content");
                var content = WellTool.Core.IO.FileUtil.ReadString(tempFile);
                Assert.Equal("Test content", content);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        [Fact]
        public void TestConvert()
        {
            var intValue = WellToolConvert.Converter.To<int>("123");
            Assert.Equal(123, intValue);
            
            var stringValue = WellToolConvert.Converter.To<string>(123);
            Assert.Equal("123", stringValue);
        }

        [Fact]
        public void TestMapUtil()
        {
            var map = new Dictionary<string, int>();
            map["test"] = 123;
            Assert.Equal(123, map["test"]);
        }

        [Fact]
        public void TestBiMap()
        {
            var biMap = new WellTool.Core.Map.BiMap<string, int>();
            biMap.Add("one", 1);
            biMap.Add("two", 2);
            
            Assert.Equal(1, biMap["one"]);
            Assert.Equal("two", biMap.GetKey(2));
        }

        [Fact]
        public void TestGanZhi()
        {
            var ganZhi = WellTool.Core.Date.Chinese.GanZhi.GetGanzhiOfYear(2024);
            Assert.NotNull(ganZhi);
        }

        [Fact]
        public void TestCalendar()
        {
            var calendar = new WellTool.Core.Date.Calendar(TimeZoneInfo.Local, CultureInfo.CurrentCulture);
            var now = DateTime.Now;
            calendar.Time = now;
            Assert.Equal(now.Year, calendar.Get((int)WellTool.Core.Date.Calendar.CalendarField.Year));
        }

        [Fact]
        public void TestDateBetween()
        {
            var start = DateTime.Now;
            var end = start.AddHours(1);
            var between = new WellTool.Core.Date.DateBetween(start, end);
            Assert.True(between.Between(WellTool.Core.Date.DateUnit.Hour) >= 1);
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
        public void TestHashids()
        {
            var hashids = WellTool.Core.Codec.Hashids.Create("test".ToCharArray());
            var id = hashids.Encode(123);
            var decoded = hashids.Decode(id);
            Assert.Contains(123, decoded);
        }

        [Fact]
        public void TestMorse()
        {
            var text = "HELLO";
            var morse = new WellTool.Core.Codec.Morse();
            var encoded = morse.Encode(text);
            var decoded = morse.Decode(encoded);
            Assert.Equal(text, decoded);
        }

        [Fact]
        public void TestIterUtil()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var result = new List<int>();
            WellTool.Core.Collection.IterUtil.ForEach(list.GetEnumerator(), item => result.Add(item * 2));
            Assert.Equal(5, result.Count);
            Assert.Equal(2, result[0]);
            Assert.Equal(10, result[4]);
        }

        [Fact]
        public void TestListUtil()
        {
            var list = WellTool.Core.Collection.ListUtil.Of(1, 2, 3, 4, 5);
            Assert.Equal(5, list.Count);
            Assert.Equal(1, list[0]);
            Assert.Equal(5, list[4]);
        }

        [Fact]
        public void TestSingleton()
        {
            var instance1 = WellTool.Core.Lang.Singleton.Get<TestObject>();
            var instance2 = WellTool.Core.Lang.Singleton.Get<TestObject>();
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void TestPair()
        {
            var pair = new WellTool.Core.Lang.Pair<string, int>("test", 123);
            Assert.Equal("test", pair.Key);
            Assert.Equal(123, pair.Value);
        }

        private class TestObject
        {
            public string Name { get; set; } = string.Empty;
            public int Age { get; set; }
            
            public TestObject() { }
            
            // 用于单例模式的私有构造函数
        }
    }
}
