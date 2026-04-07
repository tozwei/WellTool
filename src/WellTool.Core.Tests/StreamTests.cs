using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WellTool.Core.Tests
{
    public class StreamTests
    {
        [Fact]
        public void OfTest()
        {
            // 使用C#的LINQ实现类似StreamUtil.of的功能
            var result = Enumerable.Range(0, 4)
                .Select(i => (int)System.Math.Pow(2, i + 1))
                .ToArray();
            
            Assert.Equal(new[] { 2, 4, 8, 16 }, result);
        }

        [Fact]
        public void StreamTestEmptyList()
        {
            var emptyList = new List<int>();
            var result = emptyList.AsEnumerable().ToArray();
            Assert.Empty(result);
        }

        [Fact]
        public void StreamTestOrdinaryList()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.AsEnumerable().ToArray();
            Assert.Equal(new[] { 1, 2, 3 }, result);

            var hashSet = new HashSet<int> { 1, 2, 3 };
            var setResult = hashSet.AsEnumerable().ToHashSet();
            Assert.Equal(hashSet, setResult);
        }

        [Fact]
        public void ReduceListMapTest()
        {
            var nameScoreMapList = new List<Dictionary<string, int>>
            {
                new Dictionary<string, int> { { "苏格拉底", 1 }, { "特拉叙马霍斯", 3 } },
                new Dictionary<string, int> { { "苏格拉底", 2 } },
                new Dictionary<string, int> { { "特拉叙马霍斯", 1 } },
                new Dictionary<string, int> { { "特拉叙马霍斯", 2 } }
            };

            // 执行聚合
            var nameScoresMap = nameScoreMapList
                .SelectMany(map => map)
                .GroupBy(kv => kv.Key)
                .ToDictionary(g => g.Key, g => g.Select(kv => kv.Value).ToList());

            Assert.Equal(new List<int> { 1, 2 }, nameScoresMap["苏格拉底"]);
            Assert.Equal(new List<int> { 3, 1, 2 }, nameScoresMap["特拉叙马霍斯"]);

            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "name", "sam" }, { "count", "80" } },
                new Dictionary<string, string> { { "name", "sam" }, { "count", "81" } },
                new Dictionary<string, string> { { "name", "sam" }, { "count", "82" } },
                new Dictionary<string, string> { { "name", "jack" }, { "count", "80" } },
                new Dictionary<string, string> { { "name", "jack" }, { "count", "90" } }
            };

            var nameMap = data
                .GroupBy(d => d["name"])
                .ToDictionary(
                    g => g.Key,
                    g => g.SelectMany(d => d)
                          .GroupBy(kv => kv.Key)
                          .ToDictionary(
                              kg => kg.Key,
                              kg => kg.Select(kv => kv.Value).ToList()
                          )
                );

            Assert.Equal(new List<string> { "sam", "sam", "sam" }, nameMap["sam"]["name"]);
            Assert.Equal(new List<string> { "80", "81", "82" }, nameMap["sam"]["count"]);
            Assert.Equal(new List<string> { "jack", "jack" }, nameMap["jack"]["name"]);
            Assert.Equal(new List<string> { "80", "90" }, nameMap["jack"]["count"]);
        }

        [Fact]
        public void TestGroupingByAfterValueMapped()
        {
            var list = new List<int> { 1, 1, 2, 2, 3, 4 };

            var map = list
                .GroupBy(t => (t & 1) == 0)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ToString()).ToHashSet()
                );

            Assert.Contains(true, map.Keys);
            Assert.Contains(false, map.Keys);
            Assert.Equal(new HashSet<string> { "2", "4" }, map[true]);
            Assert.Equal(new HashSet<string> { "1", "3" }, map[false]);

            var map2 = list
                .GroupBy(t => (t & 1) == 0)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ToString()).ToList()
                );

            Assert.Equal(new List<string> { "2", "2", "4" }, map2[true]);
            Assert.Equal(new List<string> { "1", "1", "3" }, map2[false]);
        }
    }
}