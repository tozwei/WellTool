using Xunit;
using WellTool.Core.Util;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3136 测试 - XML转Bean时空节点处理问题
    /// https://github.com/chinabugotech/hutool/issues/3136
    /// </summary>
    public class Issue3136Test
    {
        /// <summary>
        /// 此用例中，message节点无content，理解为空节点，转换为map后，此节点值为""，转为对象时，理应为null
        /// </summary>
        [Fact]
        public void XmlToBeanTest()
        {
            var xmlStr = "<?xml version=\"1.0\" encoding=\"gbk\" ?><response><code>02</code><message></message></response>";
            var smsRes = XmlUtil.XmlToBean<SmsRes>(xmlStr);

            Assert.NotNull(smsRes);
            Assert.Equal("02", smsRes.Code);
            Assert.NotNull(smsRes.Message);
            // 空节点的item应该为空列表
            Assert.NotNull(smsRes.Message.Item);
        }

        /// <summary>
        /// 测试不含空节点的情况
        /// </summary>
        [Fact]
        public void XmlToBeanTest2()
        {
            var xmlStr = "<?xml version=\"1.0\" encoding=\"gbk\" ?><response><code>02</code></response>";
            var smsRes = XmlUtil.XmlToBean<SmsRes>(xmlStr);

            Assert.NotNull(smsRes);
            Assert.Equal("02", smsRes.Code);
        }

        /// <summary>
        /// 测试XmlToMap方法
        /// </summary>
        [Fact]
        public void XmlToMapTest()
        {
            var xmlStr = "<?xml version=\"1.0\" ?><response><code>02</code><message></message></response>";
            var map = XmlUtil.XmlToMap(xmlStr);

            Assert.NotNull(map);
            Assert.Equal("02", map["code"]);
            Assert.Equal("", map["message"]); // 空节点应该为空字符串
        }

        /// <summary>
        /// 测试使用XElement重载方法
        /// </summary>
        [Fact]
        public void XmlToBeanWithNodeTest()
        {
            var xmlStr = "<?xml version=\"1.0\" encoding=\"gbk\" ?><response><code>02</code><message></message></response>";
            var doc = XDocument.Parse(xmlStr);
            var smsRes = XmlUtil.XmlToBean<SmsRes>(doc.Root);

            Assert.NotNull(smsRes);
            Assert.Equal("02", smsRes.Code);
        }

        /// <summary>
        /// 测试ParseXml方法
        /// </summary>
        [Fact]
        public void ParseXmlTest()
        {
            var xmlStr = "<?xml version=\"1.0\" ?><root><item>value</item></root>";
            var doc = XmlUtil.ParseXml(xmlStr);

            Assert.NotNull(doc);
            Assert.NotNull(doc.Root);
            Assert.Equal("root", doc.Root.Name.LocalName);
        }

        #region Model Classes

        public class SmsRes
        {
            /// <summary>
            /// 状态码
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// 消息
            /// </summary>
            public Message Message { get; set; }
        }

        public class Message
        {
            /// <summary>
            /// 消息项
            /// </summary>
            public List<MessageItem> Item { get; set; } = new List<MessageItem>();
        }

        public class MessageItem
        {
            /// <summary>
            /// 手机号
            /// </summary>
            public string Desmobile { get; set; }

            /// <summary>
            /// 消息id
            /// </summary>
            public string Msgid { get; set; }
        }

        #endregion
    }
}