using System.IO;
using Xunit;
using WellTool.Poi.Ofd;

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
                using (var writer = new OfdWriter(tempFile))
                {
                    writer.AddText("Hello, OFD!")
                          .AddText("This is a test OFD file")
                          .Close();
                }
                Assert.True(File.Exists(tempFile));
                Assert.True(new FileInfo(tempFile).Length > 0);
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
                using (var writer = new OfdWriter(tempFile))
                {
                    writer.AddText("Test OFD creation")
                          .Close();
                }
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
