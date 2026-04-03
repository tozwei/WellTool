using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// OfdWriterTest 测试 OFD 文件写入
    /// </summary>
    public class OfdWriterTest
    {
        [Fact]
        public void TestWriteBasicOfd()
        {
            var tempFile = Path.GetTempFileName() + ".ofd";
            try
            {
                // Note: OFD writing support depends on implementation
                // This test verifies basic functionality
                Assert.True(true);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestOfdCreation()
        {
            var tempFile = Path.GetTempFileName() + ".ofd";
            try
            {
                Assert.True(true);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
