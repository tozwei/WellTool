using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// WordWriterTest 测试
    /// </summary>
    public class WordWriterTest
    {
        [Fact]
        public void TestWriteBasicWord()
        {
            var tempFile = Path.GetTempFileName() + ".docx";
            try
            {
                using var writer = WellTool.Poi.WordUtil.GetWriter(tempFile);
                writer.Write("Hello World!");
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestWriteMultipleLines()
        {
            var tempFile = Path.GetTempFileName() + ".docx";
            try
            {
                using var writer = WellTool.Poi.WordUtil.GetWriter(tempFile);
                writer.Write("Line 1");
                writer.Write("Line 2");
                writer.Write("Line 3");
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestWriteEmptyDocument()
        {
            var tempFile = Path.GetTempFileName() + ".docx";
            try
            {
                using var writer = WellTool.Poi.WordUtil.GetWriter(tempFile);
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestWriteSpecialCharacters()
        {
            var tempFile = Path.GetTempFileName() + ".docx";
            try
            {
                using var writer = WellTool.Poi.WordUtil.GetWriter(tempFile);
                writer.Write("Special chars: <>&\"'中文测试");
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestWriteLongContent()
        {
            var tempFile = Path.GetTempFileName() + ".docx";
            try
            {
                using var writer = WellTool.Poi.WordUtil.GetWriter(tempFile);
                
                var content = new string('A', 10000);
                writer.Write(content);
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
                Assert.True(new FileInfo(tempFile).Length > 0);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
