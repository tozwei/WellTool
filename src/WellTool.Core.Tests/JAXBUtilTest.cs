using Xunit;
using WellTool.Core;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// JAXB工具单元测试
    /// </summary>
    public class JAXBUtilTest
    {
        [Fact]
        public void MarshalTest()
        {
            var xml = JAXBUtil.Marshal(new TestObject { Name = "test", Value = 123 });
            Assert.Contains("test", xml);
            Assert.Contains("123", xml);
        }

        [Fact]
        public void UnmarshalTest()
        {
            var xml = "<TestObject><Name>test</Name><Value>123</Value></TestObject>";
            var obj = JAXBUtil.Unmarshal<TestObject>(xml);
            Assert.Equal("test", obj.Name);
            Assert.Equal(123, obj.Value);
        }

        [Fact]
        public void MarshalWithDeclarationTest()
        {
            var xml = JAXBUtil.MarshalWithDeclaration(new TestObject { Name = "test", Value = 123 });
            Assert.Contains("<?xml", xml);
        }

        [Fact]
        public void MarshalWithoutHeaderTest()
        {
            var xml = JAXBUtil.MarshalWithoutHeader(new TestObject { Name = "test", Value = 123 });
            Assert.DoesNotContain("<?xml", xml);
        }

        [Fact]
        public void ConvertToXmlTest()
        {
            var xml = JAXBUtil.ConvertToXml(new TestObject { Name = "test", Value = 123 });
            Assert.Contains("test", xml);
        }

        [Fact]
        public void ConvertToObjectTest()
        {
            var xml = "<TestObject><Name>test</Name><Value>123</Value></TestObject>";
            var obj = JAXBUtil.ConvertToObject<TestObject>(xml, Encoding.UTF8);
            Assert.Equal("test", obj.Name);
        }

        [Fact]
        public void XmlToListTest()
        {
            var xml = "<ArrayList><TestObject><Name>a</Name><Value>1</Value></TestObject><TestObject><Name>b</Name><Value>2</Value></TestObject></ArrayList>";
            var list = JAXBUtil.XmlToList<TestObject>(xml);
            Assert.Equal(2, list.Count);
        }

        public class TestObject
        {
            public string Name { get; set; } = "";
            public int Value { get; set; }
        }
    }
}
