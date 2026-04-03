using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Version工具单元测试
    /// </summary>
    public class VersionUtilTest
    {
        [Fact]
        public void GetVersionTest()
        {
            var version = VersionUtil.GetVersion();
            Assert.NotNull(version);
        }

        [Fact]
        public void GetVersionFromManifestTest()
        {
            var version = VersionUtil.GetVersionFromManifest();
            Assert.NotNull(version);
        }

        [Fact]
        public void IsGETest()
        {
            Assert.True(VersionUtil.IsGE("1.0.0", "1.0.0"));
            Assert.True(VersionUtil.IsGE("1.1.0", "1.0.0"));
            Assert.True(VersionUtil.IsGE("2.0.0", "1.0.0"));
            Assert.False(VersionUtil.IsGE("0.9.0", "1.0.0"));
        }

        [Fact]
        public void IsGTTest()
        {
            Assert.False(VersionUtil.IsGT("1.0.0", "1.0.0"));
            Assert.True(VersionUtil.IsGT("1.1.0", "1.0.0"));
            Assert.True(VersionUtil.IsGT("2.0.0", "1.0.0"));
            Assert.False(VersionUtil.IsGT("0.9.0", "1.0.0"));
        }

        [Fact]
        public void IsLETest()
        {
            Assert.True(VersionUtil.IsLE("1.0.0", "1.0.0"));
            Assert.True(VersionUtil.IsLE("0.9.0", "1.0.0"));
            Assert.False(VersionUtil.IsLE("2.0.0", "1.0.0"));
        }

        [Fact]
        public void IsLTTest()
        {
            Assert.False(VersionUtil.IsLT("1.0.0", "1.0.0"));
            Assert.True(VersionUtil.IsLT("0.9.0", "1.0.0"));
            Assert.False(VersionUtil.IsLT("2.0.0", "1.0.0"));
        }

        [Fact]
        public void IsEQTest()
        {
            Assert.True(VersionUtil.IsEQ("1.0.0", "1.0.0"));
            Assert.False(VersionUtil.IsEQ("1.0.1", "1.0.0"));
        }

        [Fact]
        public void CompareVersionTest()
        {
            Assert.Equal(0, VersionUtil.CompareVersion("1.0.0", "1.0.0"));
            Assert.True(VersionUtil.CompareVersion("1.1.0", "1.0.0") > 0);
            Assert.True(VersionUtil.CompareVersion("0.9.0", "1.0.0") < 0);
        }

        [Fact]
        public void IsValidTest()
        {
            Assert.True(VersionUtil.IsValid("1.0.0"));
            Assert.True(VersionUtil.IsValid("1.0"));
            Assert.True(VersionUtil.IsValid("1"));
            Assert.False(VersionUtil.IsValid(""));
            Assert.False(VersionUtil.IsValid("invalid"));
        }

        [Fact]
        public void GetVersionPartsTest()
        {
            var parts = VersionUtil.GetVersionParts("1.2.3");
            Assert.Equal(3, parts.Length);
            Assert.Equal(1, parts[0]);
            Assert.Equal(2, parts[1]);
            Assert.Equal(3, parts[2]);
        }
    }
}
