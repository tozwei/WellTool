using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WellTool.Core.Map;
using WellTool.Core.Converter;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Map工具单元测试
    /// </summary>
    public class MapTests
    {
        enum PeopleEnum { GIRL, BOY, CHILD }

        public class User
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }

        public class Group
        {
            public long Id { get; set; }
            public List<User> Users { get; set; }
        }

        public class UserGroup
        {
            public long UserId { get; set; }
            public long GroupId { get; set; }
        }

        [Fact]
        public void FilterTest()
        {
            var map = new Dictionary<string, string>();
            map["a"] = "1";
            map["b"] = "2";
            map["c"] = "3";
            map["d"] = "4";

            var map2 = MapUtil.Filter(map, t => System.Convert.ToInt32(t.Value) % 2 == 0);

            Assert.Equal(2, map2.Count);
            Assert.Equal("2", map2["b"]);
            Assert.Equal("4", map2["d"]);
        }

        [Fact]
        public void MapTest()
        {
            var adjectivesMap = new Dictionary<int, string>
            {
                { 0, "lovely" },
                { 1, "friendly" },
                { 2, "happily" }
            };

            var peopleEnums = (PeopleEnum[])PeopleEnum.GetValues(typeof(PeopleEnum));
            var resultMap = MapUtil.Map(adjectivesMap, (k, v) => v + " " + peopleEnums[k].ToString().ToLower());

            Assert.Equal("lovely girl", resultMap[0]);
            Assert.Equal("friendly boy", resultMap[1]);
            Assert.Equal("happily child", resultMap[2]);

            var customers = new Queue<string>();
            customers.Enqueue("刑部尚书手工耿");
            customers.Enqueue("木瓜大盗大漠叔");
            customers.Enqueue("竹鼠发烧找华农");
            customers.Enqueue("朴实无华朱一旦");

            var groups = Enumerable.Range(0, 4).Select(i => new Group { Id = i + 1L }).ToList();

            var idUserMap = Enumerable.Range(0, 4).Select(i => new User { Id = i + 1L, Name = customers.Dequeue() }).ToDictionary(u => u.Id);

            var groupIdUserIdsMap = groups.SelectMany(group => idUserMap.Keys.Select(userId => new UserGroup { GroupId = group.Id, UserId = userId }))
                .GroupBy(ug => ug.GroupId)
                .ToDictionary(g => g.Key, g => g.Select(ug => ug.UserId).ToList());

            var groupIdUserMap = MapUtil.Map(groupIdUserIdsMap, (groupId, userIds) => userIds.Select(id => idUserMap[id]).ToList());

            groups.ForEach(group =>
            {
                if (groupIdUserMap.TryGetValue(group.Id, out var users))
                {
                    group.Users = users;
                }
            });

            groups.ForEach(group =>
            {
                var users = group.Users;
                Assert.Equal("刑部尚书手工耿", users[0].Name);
                Assert.Equal("木瓜大盗大漠叔", users[1].Name);
                Assert.Equal("竹鼠发烧找华农", users[2].Name);
                Assert.Equal("朴实无华朱一旦", users[3].Name);
            });

            var nullMap = MapUtil.Map(new Dictionary<int, int> { { 0, 0 } }, (k, v) => (int?)null);
            foreach (var kvp in nullMap)
            {
                Assert.Null(kvp.Value);
            }
        }

        [Fact]
        public void FilterMapWrapperTest()
        {
            var map = new Dictionary<string, string>();
            map["a"] = "1";
            map["b"] = "2";
            map["c"] = "3";
            map["d"] = "4";

            var camelCaseMap = MapUtil.ToCamelCaseMap(map);

            var map2 = MapUtil.Filter(camelCaseMap, t => System.Convert.ToInt32(t.Value) % 2 == 0);

            Assert.Equal(2, map2.Count);
            Assert.Equal("2", map2["b"]);
            Assert.Equal("4", map2["d"]);
        }

        [Fact]
        public void FilterContainsTest()
        {
            var map = new Dictionary<string, string>();
            map["abc"] = "1";
            map["bcd"] = "2";
            map["def"] = "3";
            map["fgh"] = "4";

            var map2 = MapUtil.Filter(map, t => t.Key.Contains("bc"));
            Assert.Equal(2, map2.Count);
            Assert.Equal("1", map2["abc"]);
            Assert.Equal("2", map2["bcd"]);
        }

        [Fact]
        public void EditTest()
        {
            var map = new Dictionary<string, string>();
            map["a"] = "1";
            map["b"] = "2";
            map["c"] = "3";
            map["d"] = "4";

            var map2 = MapUtil.Edit(map, t =>
            {
                return new KeyValuePair<string, string>(t.Key, t.Value + "0");
            });

            Assert.Equal(4, map2.Count);
            Assert.Equal("10", map2["a"]);
            Assert.Equal("20", map2["b"]);
            Assert.Equal("30", map2["c"]);
            Assert.Equal("40", map2["d"]);
        }

        // [Fact]
        // public void ReverseTest()
        // {
        //     var map = new Dictionary<string, string>();
        //     map["a"] = "1";
        //     map["b"] = "2";
        //     map["c"] = "3";
        //     map["d"] = "4";

        //     var map2 = MapUtil.Reverse(map);

        //     Assert.Equal("a", map2["1"]);
        //     Assert.Equal("b", map2["2"]);
        //     Assert.Equal("c", map2["3"]);
        //     Assert.Equal("d", map2["4"]);
        // }

        // [Fact]
        // public void ToObjectArrayTest()
        // {
        //     var map = new Dictionary<string, string>();
        //     map["a"] = "1";
        //     map["b"] = "2";
        //     map["c"] = "3";
        //     map["d"] = "4";

        //     var objectArray = MapUtil.ToObjectArray(map);
        //     Assert.Equal("a", objectArray[0][0]);
        //     Assert.Equal("1", objectArray[0][1]);
        //     Assert.Equal("b", objectArray[1][0]);
        //     Assert.Equal("2", objectArray[1][1]);
        //     Assert.Equal("c", objectArray[2][0]);
        //     Assert.Equal("3", objectArray[2][1]);
        //     Assert.Equal("d", objectArray[3][0]);
        //     Assert.Equal("4", objectArray[3][1]);
        // }

        // [Fact]
        // public void SortJoinTest()
        // {
        //     var build = new Dictionary<string, string>
        //     {
        //         { "key1", "value1" },
        //         { "key3", "value3" },
        //         { "key2", "value2" }
        //     };

        //     var join1 = MapUtil.SortJoin(build, string.Empty, string.Empty, false);
        //     Assert.Equal("key1value1key2value2key3value3", join1);

        //     var join2 = MapUtil.SortJoin(build, string.Empty, string.Empty, false, "123");
        //     Assert.Equal("key1value1key2value2key3value3123", join2);

        //     var join3 = MapUtil.SortJoin(build, string.Empty, string.Empty, false, "123", "abc");
        //     Assert.Equal("key1value1key2value2key3value3123abc", join3);
        // }

        // [Fact]
        // public void OfEntriesTest()
        // {
        //     var map = MapUtil.OfEntries(MapUtil.Entry("a", 1), MapUtil.Entry("b", 2));
        //     Assert.Equal(2, map.Count);
        //     Assert.Equal(1, map["a"]);
        //     Assert.Equal(2, map["b"]);
        // }

        // [Fact]
        // public void GetIntTest()
        // {
        //     var map = new Dictionary<string, string> { { "age", "d" } };
        //     Assert.Throws<FormatException>(() => MapUtil.GetInt(map, "age"));
        // }

        // [Fact]
        // public void JoinIgnoreNullTest()
        // {
        //     var v1 = Dict.Of().Set("id", 12).Set("name", "张三").Set("age", null);
        //     var s = MapUtil.JoinIgnoreNull(v1, ",", "=");
        //     Assert.Equal("id=12,name=张三", s);
        // }

        // [Fact]
        // public void RenameKeyTest()
        // {
        //     var v1 = Dict.Of().Set("id", 12).Set("name", "张三").Set("age", null);
        //     var map = MapUtil.RenameKey(v1, "name", "newName");
        //     Assert.Equal("张三", map["newName"]);
        // }

        // [Fact]
        // public void RenameKeyMapEmptyNoChange()
        // {
        //     var map = new Dictionary<string, string>();
        //     var result = MapUtil.RenameKey(map, "oldKey", "newKey");
        //     Assert.True(result.Count == 0);
        // }

        // [Fact]
        // public void RenameKeyOldKeyNotPresentNoChange()
        // {
        //     var map = new Dictionary<string, string> { { "anotherKey", "value" } };
        //     var result = MapUtil.RenameKey(map, "oldKey", "newKey");
        //     Assert.Equal(1, result.Count);
        //     Assert.Equal("value", result["anotherKey"]);
        // }

        // [Fact]
        // public void RenameKeyOldKeyPresentNewKeyNotPresentKeyRenamed()
        // {
        //     var map = new Dictionary<string, string> { { "oldKey", "value" } };
        //     var result = MapUtil.RenameKey(map, "oldKey", "newKey");
        //     Assert.Equal(1, result.Count);
        //     Assert.Equal("value", result["newKey"]);
        // }

        // [Fact]
        // public void RenameKeyNewKeyPresentThrowsException()
        // {
        //     var map = new Dictionary<string, string> { { "oldKey", "value" }, { "newKey", "existingValue" } };
        //     Assert.Throws<ArgumentException>(() => MapUtil.RenameKey(map, "oldKey", "newKey"));
        // }

        // [Fact]
        // public void Issue3162Test()
        // {
        //     var map = new Dictionary<string, object>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" }
        //     };
        //     var filtered = MapUtil.Filter(map, "a", "b");
        //     Assert.Equal(2, filtered.Count);
        //     Assert.Equal("1", filtered["a"]);
        //     Assert.Equal("2", filtered["b"]);
        // }

        // [Fact]
        // public void PartitionNullMapThrowsException()
        // {
        //     Assert.Throws<ArgumentException>(() => MapUtil.Partition(null, 2));
        // }

        // [Fact]
        // public void PartitionSizeZeroThrowsException()
        // {
        //     var map = new Dictionary<string, string> { { "a", "1" } };
        //     Assert.Throws<ArgumentException>(() => MapUtil.Partition(map, 0));
        // }

        // [Fact]
        // public void PartitionSizeNegativeThrowsException()
        // {
        //     var map = new Dictionary<string, string> { { "a", "1" } };
        //     Assert.Throws<ArgumentException>(() => MapUtil.Partition(map, -1));
        // }

        // [Fact]
        // public void PartitionEmptyMapReturnsEmptyList()
        // {
        //     var map = new Dictionary<string, string>();
        //     var result = MapUtil.Partition(map, 2);
        //     Assert.True(result.Count == 0);
        // }

        // [Fact]
        // public void PartitionMapSizeMultipleOfSizePartitionsCorrectly()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" },
        //         { "d", "4" }
        //     };

        //     var result = MapUtil.Partition(map, 2);

        //     Assert.Equal(2, result.Count);
        //     Assert.Equal(2, result[0].Count);
        //     Assert.Equal(2, result[1].Count);
        // }

        // [Fact]
        // public void PartitionMapSizeNotMultipleOfSizePartitionsCorrectly()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" },
        //         { "d", "4" },
        //         { "e", "5" }
        //     };

        //     var result = MapUtil.Partition(map, 2);

        //     Assert.Equal(3, result.Count);
        //     Assert.Equal(2, result[0].Count);
        //     Assert.Equal(2, result[1].Count);
        //     Assert.Equal(1, result[2].Count);
        // }

        // [Fact]
        // public void PartitionGeneralCasePartitionsCorrectly()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" },
        //         { "d", "4" },
        //         { "e", "5" },
        //         { "f", "6" }
        //     };

        //     var result = MapUtil.Partition(map, 3);

        //     Assert.Equal(2, result.Count);
        //     Assert.Equal(3, result[0].Count);
        //     Assert.Equal(3, result[1].Count);
        // }

        // [Fact]
        // public void ComputeIfAbsentKeyExistsReturnsExistingValue()
        // {
        //     var map = new Dictionary<string, int> { { "key", 10 } };
        //     var result = MapUtil.ComputeIfAbsent(map, "key", k => 20);
        //     Assert.Equal(10, result);
        // }

        // [Fact]
        // public void ComputeIfAbsentKeyDoesNotExistComputesAndInsertsValue()
        // {
        //     var map = new Dictionary<string, int>();
        //     var result = MapUtil.ComputeIfAbsent(map, "key", k => 20);
        //     Assert.Equal(20, result);
        //     Assert.Equal(20, map["key"]);
        // }

        // [Fact]
        // public void ComputeIfAbsentNullValueComputesAndInsertsValue()
        // {
        //     var map = new Dictionary<string, int?> { { "key", null } };
        //     var result = MapUtil.ComputeIfAbsent(map, "key", k => 20);
        //     Assert.Equal(20, result);
        //     Assert.Equal(20, map["key"]);
        // }

        // [Fact]
        // public void ComputeIfAbsentEmptyMapInsertsValue()
        // {
        //     var map = new Dictionary<string, int>();
        //     var result = MapUtil.ComputeIfAbsent(map, "newKey", k => 100);
        //     Assert.Equal(100, result);
        //     Assert.Equal(100, map["newKey"]);
        // }

        // [Fact]
        // public void ValuesOfKeysEmptyIteratorReturnsEmptyList()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" }
        //     };
        //     var emptyIterator = System.Linq.Enumerable.Empty<string>().GetEnumerator();
        //     var result = MapUtil.ValuesOfKeys(map, emptyIterator);
        //     Assert.True(result.Count == 0);
        // }

        // [Fact]
        // public void ValuesOfKeysNonEmptyIteratorReturnsValuesList()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" }
        //     };
        //     var iterator = new List<string> { "a", "b" }.GetEnumerator();
        //     var result = MapUtil.ValuesOfKeys(map, iterator);
        //     Assert.Equal(new List<string> { "1", "2" }, result);
        // }

        // [Fact]
        // public void ValuesOfKeysKeysNotInMapReturnsNulls()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" }
        //     };
        //     var iterator = new List<string> { "d", "e" }.GetEnumerator();
        //     var result = MapUtil.ValuesOfKeys(map, iterator);
        //     Assert.Equal(new List<string> { null, null }, result);
        // }

        // [Fact]
        // public void ValuesOfKeysMixedKeysReturnsMixedValues()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "a", "1" },
        //         { "b", "2" },
        //         { "c", "3" }
        //     };
        //     var iterator = new List<string> { "a", "d", "b" }.GetEnumerator();
        //     var result = MapUtil.ValuesOfKeys(map, iterator);
        //     Assert.Equal(new List<string> { "1", null, "2" }, result);
        // }

        // [Fact]
        // public void ClearNoMapsProvidedNoAction()
        // {
        //     MapUtil.Clear();
        // }

        [Fact]
        public void ClearEmptyMapNoChange()
        {
            var map = new Dictionary<string, string>();
            MapUtil.Clear(map);
            Assert.True(map.Count == 0);
        }

        [Fact]
        public void ClearNonEmptyMapClearsMap()
        {
            var map = new Dictionary<string, string> { { "key", "value" } };
            MapUtil.Clear(map);
            Assert.True(map.Count == 0);
        }

        // [Fact]
        // public void ClearMultipleMapsClearsNonEmptyMaps()
        // {
        //     var map1 = new Dictionary<string, string> { { "key1", "value1" } };
        //     var map2 = new Dictionary<string, string> { { "key2", "value2" } };
        //     var map3 = new Dictionary<string, string>();

        //     MapUtil.Clear(map1, map2, map3);

        //     Assert.True(map1.Count == 0);
        //     Assert.True(map2.Count == 0);
        //     Assert.True(map3.Count == 0);
        // }

        // [Fact]
        // public void ClearMixedMapsClearsNonEmptyMaps()
        // {
        //     var map = new Dictionary<string, string> { { "key", "value" } };
        //     var emptyMap = new Dictionary<string, string>();

        //     MapUtil.Clear(map, emptyMap);

        //     Assert.True(map.Count == 0);
        //     Assert.True(emptyMap.Count == 0);
        // }

        [Fact]
        public void EmptyNoParametersReturnsEmptyMap()
        {
            var emptyMap = MapUtil.Empty<string, string>();
            Assert.True(emptyMap.Count == 0);
        }

        // [Fact]
        // public void RemoveNullValueNullMapReturnsNull()
        // {
        //     var result = MapUtil.RemoveNullValue<string, string>(null);
        //     Assert.Null(result);
        // }

        // [Fact]
        // public void RemoveNullValueEmptyMapReturnsEmptyMap()
        // {
        //     var map = new Dictionary<string, string>();
        //     var result = MapUtil.RemoveNullValue(map);
        //     Assert.Equal(0, result.Count);
        // }

        // [Fact]
        // public void RemoveNullValueNoNullValuesReturnsSameMap()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "key1", "value1" },
        //         { "key2", "value2" }
        //     };

        //     var result = MapUtil.RemoveNullValue(map);

        //     Assert.Equal(2, result.Count);
        //     Assert.Equal("value1", result["key1"]);
        //     Assert.Equal("value2", result["key2"]);
        // }

        // [Fact]
        // public void RemoveNullValueWithNullValuesRemovesNullEntries()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "key1", "value1" },
        //         { "key2", null },
        //         { "key3", "value3" }
        //     };

        //     var result = MapUtil.RemoveNullValue(map);

        //     Assert.Equal(2, result.Count);
        //     Assert.Equal("value1", result["key1"]);
        //     Assert.Equal("value3", result["key3"]);
        //     Assert.False(result.ContainsKey("key2"));
        // }

        // [Fact]
        // public void RemoveNullValueAllNullValuesReturnsEmptyMap()
        // {
        //     var map = new Dictionary<string, string>
        //     {
        //         { "key1", null },
        //         { "key2", null }
        //     };

        //     var result = MapUtil.RemoveNullValue(map);

        //     Assert.Equal(0, result.Count);
        // }

        [Fact]
        public void GetQuietlyMapIsNullReturnsDefaultValue()
        {
            var result = MapUtil.Get<string, string>(null, "key1", "default");
            Assert.Equal("default", result);
        }

        [Fact]
        public void GetQuietlyKeyExistsReturnsConvertedValue()
        {
            var map = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 123 }
            };
            var result = MapUtil.Get<string, object>(map, "key1", "default");
            Assert.Equal("value1", result);
        }

        [Fact]
        public void GetQuietlyKeyDoesNotExistReturnsDefaultValue()
        {
            var map = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 123 }
            };
            var result = MapUtil.Get<string, object>(map, "key3", "default");
            Assert.Equal("default", result);
        }

        [Fact]
        public void GetQuietlyKeyExistsWithNullValueReturnsDefaultValue()
        {
            var map = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", 123 },
                { "key3", null }
            };
            var result = MapUtil.Get<string, object>(map, "key3", "default");
            Assert.Equal("default", result);
        }

        [Fact]
        public void GetMapIsNullReturnsDefaultValue()
        {
            var result = MapUtil.Get<string, object>(null, "age", null);
            Assert.Null(result);
        }

        [Fact]
        public void GetKeyExistsReturnsConvertedValue()
        {
            var map = new Dictionary<string, object>
            {
                { "age", "18" },
                { "name", "WellTool" }
            };
            Assert.Equal("18", MapUtil.Get<string, object>(map, "age", "default"));
        }

        [Fact]
        public void GetKeyDoesNotExistReturnsDefaultValue()
        {
            var map = new Dictionary<string, object>
            {
                { "age", "18" },
                { "name", "WellTool" }
            };
            Assert.Equal("default", MapUtil.Get<string, object>(map, "nonexistent", "default"));
        }

        [Fact]
        public void FlattenMapReturnsTest()
        {
            var clothes = new Dictionary<string, string>
            {
                { "clothesName", "ANTA" },
                { "clothesPrice", "200" }
            };

            var person = new Dictionary<string, object>
            {
                { "personName", "XXXX" },
                { "clothes", clothes }
            };

            var map = new Dictionary<string, object>
            {
                { "home", "AAA" },
                { "person", person }
            };

            var flattenMap = MapUtil.Flatten(map);
            Assert.Equal("ANTA", MapUtil.Get<string, object>(flattenMap, "personclothesclothesName"));
            Assert.Equal("200", MapUtil.Get<string, object>(flattenMap, "personclothesclothesPrice"));
            Assert.Equal("XXXX", MapUtil.Get<string, object>(flattenMap, "personpersonName"));
            Assert.Equal("AAA", MapUtil.Get<string, object>(flattenMap, "home"));
        }

        #region BiMap测试

        [Fact]
        public void TestBiMap()
        {
            // 测试BiMap
            var biMap = new BiMap<string, int>();
            biMap.Add("one", 1);
            biMap.Add("two", 2);
            biMap.Add("three", 3);

            Assert.Equal(1, biMap["one"]);
            Assert.Equal("two", biMap.GetKey(2));
            Assert.True(biMap.ContainsKey("three"));
            Assert.True(biMap.ContainsValue(3));
        }

        #endregion

        #region CamelCaseMap测试

        [Fact]
        public void TestCamelCaseMap()
        {
            // 测试CamelCaseMap
            var camelCaseMap = new CamelCaseMap<string>();
            camelCaseMap["user_name"] = "张三";
            camelCaseMap["user_age"] = "18";

            Assert.Equal("张三", camelCaseMap["userName"]);
            Assert.Equal("18", camelCaseMap["userAge"]);
        }

        #endregion

        #region CaseInsensitiveMap测试

        [Fact]
        public void TestCaseInsensitiveMap()
        {
            // 测试CaseInsensitiveMap
            var caseInsensitiveMap = new CaseInsensitiveMap<string>();
            caseInsensitiveMap["NAME"] = "张三";
            caseInsensitiveMap["AGE"] = "18";

            Assert.Equal("张三", caseInsensitiveMap["name"]);
            Assert.Equal("18", caseInsensitiveMap["age"]);
        }

        #endregion

        #region MapWrapper测试

        [Fact]
        public void TestMapWrapper()
        {
            // 测试MapWrapper
            var originalMap = new Dictionary<string, string>
            {
                { "name", "张三" },
                { "age", "18" }
            };

            var mapWrapper = new MapWrapper<string, string>(originalMap);
            Assert.Equal("张三", mapWrapper["name"]);
            Assert.Equal("18", mapWrapper["age"]);
        }

        #endregion

        #region 集合值映射测试

        [Fact]
        public void TestListValueMap()
        {
            // 测试ListValueMap
            var listValueMap = new WellTool.Core.Map.Multi.ListValueMap<string, string>();
            listValueMap.Add("group1", new List<string> { "user1" });
            listValueMap.Add("group2", new List<string> { "user3" });

            var usersInGroup1 = listValueMap["group1"];
            Assert.Single(usersInGroup1);
            Assert.Contains("user1", usersInGroup1);
        }

        [Fact]
        public void TestSetValueMap()
        {
            // 测试SetValueMap
            var setValueMap = new WellTool.Core.Map.Multi.SetValueMap<string, string>();
            setValueMap.Add("group1", new HashSet<string> { "user1" });
            setValueMap.Add("group2", new HashSet<string> { "user3" });

            var usersInGroup1 = setValueMap["group1"];
            Assert.Single(usersInGroup1);
            Assert.Contains("user1", usersInGroup1);
        }

        #endregion
    }
}