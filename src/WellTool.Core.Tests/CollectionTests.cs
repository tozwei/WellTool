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
            Assert.True(WellTool.Core.Collection.CollUtil.IsEmpty<int>(emptyList));
            Assert.False(WellTool.Core.Collection.CollUtil.IsNotEmpty<int>(emptyList));
            Assert.False(WellTool.Core.Collection.CollUtil.IsEmpty<int>(list1));
            Assert.True(WellTool.Core.Collection.CollUtil.IsNotEmpty<int>(list1));
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

        // [Fact]
        // public void TestBoundedPriorityQueue()
        // {
        //     // 测试有界优先队列
        //     var queue = new WellTool.Core.Collection.BoundedPriorityQueue<int>(3, (a, b) => a.CompareTo(b));
        //     
        //     // 添加元素
        //     queue.Add(3);
        //     queue.Add(1);
        //     queue.Add(2);
        //     
        //     // 队列已满，添加新元素会移除最小的元素
        //     queue.Add(4);
        //     
        //     Assert.Equal(3, queue.Count);
        //     Assert.Equal(2, queue.Peek());
        // }

        [Fact]
        public void TestConcurrentHashSet()
        {
            // 测试并发哈希集
            var set = new WellTool.Core.Collection.ConcurrentHashSet<int>();
            
            // 添加元素
            set.Add(1);
            set.Add(2);
            set.Add(3);
            
            Assert.True(set.Contains(1));
            Assert.True(set.Contains(2));
            Assert.True(set.Contains(3));
            Assert.False(set.Contains(4));
            
            // 移除元素
            set.Remove(2);
            Assert.False(set.Contains(2));
        }
    }
}
