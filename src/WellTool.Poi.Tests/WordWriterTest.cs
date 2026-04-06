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
                using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
                writer.AddText(null, null, "Hello World!");
                writer.Flush();
                
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
                using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
                writer.AddText(null, null, "Line 1");
                writer.AddText(null, null, "Line 2");
                writer.AddText(null, null, "Line 3");
                writer.Flush();
                
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
                using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
                writer.Flush();
                
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
                using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
                writer.AddText(null, null, "Special chars: <>&\"'中文测试");
                writer.Flush();
                
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
                using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
                
                var content = new string('A', 10000);
                writer.AddText(null, null, content);
                writer.Flush();
                
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
