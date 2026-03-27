using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WellTool.Core.Tests
{
    public class CollectionTests
    {
        [Fact]
        public void TestCollUtil()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new List<int> { 3, 4, 5 };
            
            // 测试Union方法
            var union = WellTool.Core.Collection.CollUtil.Union(list1, list2);
            Assert.Equal(5, union.Count);
            Assert.Contains(1, union);
            Assert.Contains(2, union);
            Assert.Contains(3, union);
            Assert.Contains(4, union);
            Assert.Contains(5, union);
            
            // 测试Intersection方法
            var intersection = WellTool.Core.Collection.CollUtil.Intersection(list1, list2);
            Assert.Single(intersection);
            Assert.Equal(3, intersection.First());
            
            // 测试IsEmpty和IsNotEmpty方法
            var emptyList = new List<int>();
            Assert.True(WellTool.Core.Collection.CollUtil.IsEmpty(emptyList));
            Assert.False(WellTool.Core.Collection.CollUtil.IsNotEmpty(emptyList));
            Assert.False(WellTool.Core.Collection.CollUtil.IsEmpty(list1));
            Assert.True(WellTool.Core.Collection.CollUtil.IsNotEmpty(list1));
        }

        [Fact]
        public void TestIterUtil()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var result = new List<int>();
            
            // 测试ForEach方法
            WellTool.Core.Collection.IterUtil.ForEach(list.GetEnumerator(), item => result.Add(item * 2));
            Assert.Equal(5, result.Count);
            Assert.Equal(2, result[0]);
            Assert.Equal(4, result[1]);
            Assert.Equal(6, result[2]);
            Assert.Equal(8, result[3]);
            Assert.Equal(10, result[4]);
        }

        [Fact]
        public void TestListUtil()
        {
            // 测试Of方法
            var list = WellTool.Core.Collection.ListUtil.Of(1, 2, 3, 4, 5);
            Assert.Equal(5, list.Count);
            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
            Assert.Equal(4, list[3]);
            Assert.Equal(5, list[4]);
        }

        [Fact]
        public void TestCollUtilAddAll()
        {
            var target = new List<int> { 1, 2, 3 };
            var items = new[] { 4, 5, 6 };
            
            // 测试AddAll方法
            WellTool.Core.Collection.CollUtil.AddAll(target, items);
            Assert.Equal(6, target.Count);
            Assert.Contains(4, target);
            Assert.Contains(5, target);
            Assert.Contains(6, target);
        }
    }
}
