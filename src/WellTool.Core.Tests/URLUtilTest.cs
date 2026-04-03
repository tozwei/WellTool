using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// URL工具单元测试
    /// </summary>
    public class URLUtilTest
    {
        [Fact]
        public void EncodeTest()
        {
            var encoded = URLUtil.Encode("hello world");
            Assert.NotEqual("hello world", encoded);
        }

        [Fact]
        public void DecodeTest()
        {
            var encoded = URLUtil.Encode("hello world");
            var decoded = URLUtil.Decode(encoded);
            Assert.Equal("hello world", decoded);
        }

        [Fact]
        public void EncodeAllTest()
        {
            var encoded = URLUtil.EncodeAll("hello world");
            Assert.Contains("%", encoded);
        }

        [Fact]
        public void NormalizeTest()
        {
            var normalized = URLUtil.Normalize("http://example.com/");
            Assert.StartsWith("http", normalized);
        }

        [Fact]
        public void NormalizeWithPathTest()
        {
            var normalized = URLUtil.Normalize("http://example.com/a/b/../c");
            Assert.Contains("c", normalized);
        }

        [Fact]
        public void GetPathTest()
        {
            var path = URLUtil.GetPath("http://example.com/a/b/c");
            Assert.Equal("/a/b/c", path);
        }

        [Fact]
        public void GetHostTest()
        {
            var host = URLUtil.GetHost("http://example.com/a/b/c");
            Assert.Equal("example.com", host);
        }

        [Fact]
        public void GetPortTest()
        {
            var port = URLUtil.GetPort("http://example.com:8080/a/b/c");
            Assert.Equal(8080, port);
        }

        [Fact]
        public void GetSchemeTest()
        {
            var scheme = URLUtil.GetScheme("http://example.com");
            Assert.Equal("http", scheme);
        }

        [Fact]
        public void CompleteUrlTest()
        {
            var url = URLUtil.CompleteUrl("http://example.com", "/path");
            Assert.Contains("example.com", url);
            Assert.Contains("path", url);
        }

        [Fact]
        public void JarUrlTest()
        {
            var jarUrl = URLUtil.JarUrl("http://example.com/jar!/class.class");
            Assert.NotNull(jarUrl);
        }

        [Fact]
        public void Fix schemeTest()
        {
            var url = URLUtil.FixScheme("example.com");
            Assert.StartsWith("http", url);
        }

        [Fact]
        public void IsAbsoluteTest()
        {
            Assert.True(URLUtil.IsAbsolute("http://example.com"));
            Assert.False(URLUtil.IsAbsolute("/path"));
            Assert.False(URLUtil.IsAbsolute("path"));
        }

        [Fact]
        public void ContainsSchemesTest()
        {
            Assert.True(URLUtil.ContainsSchemes("http://example.com"));
            Assert.False(URLUtil.ContainsSchemes("/path"));
        }
    }
}
