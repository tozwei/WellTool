using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONPath 测试
    /// </summary>
    public class JSONPathTest
    {
        [Fact]
        public void GetByPathTest()
        {
            var json = "[{\"id\":\"1\",\"name\":\"xingming\"},{\"id\":\"2\",\"name\":\"mingzi\"}]";
            var array = JSONUtil.ParseArray(json);
            var value = array.GetByPath("[0].name");
            Assert.Equal("xingming", value);
            value = array.GetByPath("[1].name");
            Assert.Equal("mingzi", value);
        }

        [Fact]
        public void GetByPathTest2()
        {
            var str = "{'accountId':111}";
            var json = JSONUtil.Parse(str);
            var accountId = JSONUtil.GetByPath(json, "$.accountId", 0L);
            Assert.Equal(111L, accountId);
        }

        [Fact]
        public void GetByPathTest3()
        {
            var str = "[{'accountId':1},{'accountId':2},{'accountId':3}]";
            var json = JSONUtil.Parse(str);
            var accountIds = json.GetByPath<List<long>>("$.accountId");
            Assert.NotNull(accountIds);
            Assert.Equal(3, accountIds.Count);
            Assert.Equal(1L, accountIds[0]);
            Assert.Equal(2L, accountIds[1]);
            Assert.Equal(3L, accountIds[2]);
        }

        [Fact]
        public void GetByPathWithWildcardTest()
        {
            var root = new JSONObject()
                .SetOpt("actionMessage", new JSONObject()
                    .SetOpt("alertResults", new JSONArray())
                    .SetOpt("decodeFeas", new JSONArray()
                        .SetOpt(new JSONObject()
                            .SetOpt("body", new JSONObject()
                                .SetOpt("lats", new JSONArray()
                                    .SetOpt(new JSONObject().SetOpt("begin", 4260).SetOpt("text", "呵呵"))
                                    .SetOpt(new JSONObject().SetOpt("begin", 4260).SetOpt("text", "你好 "))
                                )
                            )
                        )
                    )
                );

            var byPath = JSONUtil.GetByPath(root, "$.actionMessage.decodeFeas[0].body.lats[*].text");
            Assert.NotNull(byPath);
            Assert.Equal("[\"呵呵\",\"你好 \"]", byPath.ToString());
        }
    }
}
