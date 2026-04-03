using Xunit;
using WellTool.Core;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Zip工具单元测试
    /// </summary>
    public class ZipUtilTest
    {
        [Fact]
        public void GzipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data for gzip compression");
            var gzipped = ZipUtil.Gzip(data);
            Assert.NotNull(gzipped);
            Assert.NotEqual(data.Length, gzipped.Length);
        }

        [Fact]
        public void UngzipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data for gzip compression");
            var gzipped = ZipUtil.Gzip(data);
            var ungzipped = ZipUtil.Ungzip(gzipped);
            var result = Encoding.UTF8.GetString(ungzipped);
            Assert.Equal("test data for gzip compression", result);
        }

        [Fact]
        public void ZipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var zipped = ZipUtil.Zip(data);
            Assert.NotNull(zipped);
        }

        [Fact]
        public void UnzipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var zipped = ZipUtil.Zip(data);
            var unzipped = ZipUtil.Unzip(zipped);
            var result = Encoding.UTF8.GetString(unzipped);
            Assert.Equal("test data", result);
        }

        [Fact]
        public void ZipFileTest()
        {
            var tempFile = Path.GetTempFileName();
            var zipFile = Path.GetTempFileName();
            try
            {
                File.WriteAllText(tempFile, "test content");
                ZipUtil.ZipFile(tempFile, zipFile);
                Assert.True(File.Exists(zipFile));
                Assert.True(new FileInfo(zipFile).Length > 0);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
                if (File.Exists(zipFile)) File.Delete(zipFile);
            }
        }

        [Fact]
        public void UnzipFileTest()
        {
            var tempFile = Path.GetTempFileName();
            var zipFile = Path.GetTempFileName();
            var extractDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try
            {
                File.WriteAllText(tempFile, "test content");
                ZipUtil.ZipFile(tempFile, zipFile);
                Directory.CreateDirectory(extractDir);
                ZipUtil.UnzipFile(zipFile, extractDir);
                var extractedFiles = Directory.GetFiles(extractDir);
                Assert.True(extractedFiles.Length > 0);
            }
            finally
            {
                if (File.Exists(tempFile)) File.Delete(tempFile);
                if (File.Exists(zipFile)) File.Delete(zipFile);
                if (Directory.Exists(extractDir)) Directory.Delete(extractDir, true);
            }
        }

        [Fact]
        public void IsGzipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var gzipped = ZipUtil.Gzip(data);
            Assert.True(ZipUtil.IsGzip(gzipped));
        }

        [Fact]
        public void IsZipTest()
        {
            var data = Encoding.UTF8.GetBytes("test data");
            var zipped = ZipUtil.Zip(data);
            Assert.True(ZipUtil.IsZip(zipped));
        }
    }
}
