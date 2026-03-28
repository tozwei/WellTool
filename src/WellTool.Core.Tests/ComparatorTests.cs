using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WellTool.Core.Comparator;

namespace WellTool.Core.Tests
{
    public class ComparatorTests
    {
        [Fact]
        public void CompareTest()
        {
            var compare = CompareUtil.Compare(null, "a", true);
            Assert.True(compare > 0);

            compare = CompareUtil.Compare(null, "a", false);
            Assert.True(compare < 0);
        }

        [Fact]
        public void ComparingPinyin()
        {
            var list = new List<string> { "成都", "北京", "上海", "深圳" };

            var ascendingOrderResult = new List<string> { "北京", "成都", "上海", "深圳" };
            var descendingOrderResult = new List<string> { "深圳", "上海", "成都", "北京" };

            // 正序
            list.Sort(CompareUtil.ComparingPinyin<string>(e => e));
            Assert.Equal(list, ascendingOrderResult);

            // 反序
            list.Sort(CompareUtil.ComparingPinyin<string>(e => e, true));
            Assert.Equal(list, descendingOrderResult);
        }

        [Fact]
        public void VersionComparatorTest()
        {
            var comparator = new VersionComparator();
            
            // 测试版本号比较
            Assert.True(comparator.Compare("1.0", "1.1") < 0);
            Assert.True(comparator.Compare("1.1", "1.0") > 0);
            Assert.True(comparator.Compare("1.0.1", "1.0.2") < 0);
            Assert.True(comparator.Compare("1.0.1", "1.0") > 0);
            Assert.True(comparator.Compare("1.0", "1.0") == 0);
        }

        [Fact]
        public void WindowsExplorerStringComparatorTest()
        {
            var comparator = new WindowsExplorerStringComparator();
            
            // 测试Windows资源管理器风格的字符串比较
            var testList = new List<string> { "file10.txt", "file2.txt", "file1.txt", "file.txt" };
            testList.Sort(comparator);
            
            var expected = new List<string> { "file.txt", "file1.txt", "file2.txt", "file10.txt" };
            Assert.Equal(expected, testList);
        }

        [Fact]
        public void TestPropertyComparator()
        {
            var person1 = new Person { Name = "张三", Age = 30 };
            var person2 = new Person { Name = "李四", Age = 25 };
            var person3 = new Person { Name = "王五", Age = 35 };

            var list = new List<Person> { person1, person2, person3 };

            // 按年龄排序
            var ageComparator = new PropertyComparator<Person>("Age");
            list.Sort(ageComparator);

            Assert.Equal(25, list[0].Age);
            Assert.Equal(30, list[1].Age);
            Assert.Equal(35, list[2].Age);
        }

        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}