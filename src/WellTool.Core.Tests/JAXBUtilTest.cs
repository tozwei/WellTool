using Xunit;
using WellTool.Core.Util;
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
        public void EscapeTest()
        {
            var input = "<test>";
            var escaped = XmlUtil.Escape(input);
            Assert.Equal("&lt;test&gt;", escaped);
        }

        [Fact]
        public void UnescapeTest()
        {
            var input = "&lt;test&gt;";
            var unescaped = XmlUtil.Unescape(input);
            Assert.Equal("<test>", unescaped);
        }

        [Fact]
        public void EscapeInvalidTest()
        {
            var input = "test\0test";
            var escaped = XmlUtil.EscapeInvalid(input);
            Assert.Equal("testtest", escaped);
        }

        [Fact]
        public void IsXmlSafeTest()
        {
            Assert.True(XmlUtil.IsXmlSafe('a'));
            Assert.True(XmlUtil.IsXmlSafe('1'));
            Assert.True(XmlUtil.IsXmlSafe(' '));
        }
    }
}
