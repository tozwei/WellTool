using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue1101 测试 - 测试 TreeSet 转换
    /// </summary>
    public class Issue1101Test
    {
        [Fact]
        public void TreeSetConvertTest()
        {
            var json = "[{\"nodeName\":\"admin\",\"id\":\"00010001_52c95b83-2083-4138-99fb-e6e21f0c1277\",\"sort\":0,\"type\":10},{\"nodeName\":\"test\",\"id\":\"97054a82-f8ff-46a1-b76c-cbacf6d18045\",\"sort\":0,\"type\":10}]";
            var objects = JSONUtil.ParseArray(json);
            
            Assert.NotNull(objects);
            Assert.Equal(2, objects.Count);
        }

        [Fact]
        public void TreeNodeTest()
        {
            var json = @"{
                ""children"": [{
                    ""children"": [],
                    ""id"": ""52c95b83-2083-4138-99fb-e6e21f0c1277"",
                    ""nodeName"": ""admin"",
                    ""sort"": 0,
                    ""type"": 10
                }, {
                    ""children"": [],
                    ""id"": ""97054a82-f8ff-46a1-b76c-cbacf6d18045"",
                    ""nodeName"": ""test"",
                    ""sort"": 0,
                    ""type"": 10
                }],
                ""id"": ""00010001"",
                ""nodeName"": ""测试"",
                ""sort"": 0,
                ""type"": 0
            }";

            var jsonObject = JSONUtil.ParseObj(json);
            var treeNode = jsonObject.ToBean<TreeNode>();
            
            Assert.NotNull(treeNode);
            Assert.Equal(2, treeNode.Children.Count);
            Assert.Equal("52c95b83-2083-4138-99fb-e6e21f0c1277", treeNode.Children[0].Id);
        }

        #region 辅助类

        private class TreeNodeDto
        {
            public string Id { get; set; }
            public string ParentId { get; set; }
            public int Sort { get; set; }
            public string NodeName { get; set; }
            public int Type { get; set; }
            public bool Status { get; set; }
            public string TreeNodeId { get; set; }
            public List<TreeNodeDto> Children { get; set; } = new List<TreeNodeDto>();
        }

        private class TreeNode
        {
            public string Id { get; set; }
            public string ParentId { get; set; }
            public int Sort { get; set; }
            public string NodeName { get; set; }
            public int Type { get; set; }
            public bool Status { get; set; }
            public string TreeNodeId { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        }

        #endregion
    }
}
