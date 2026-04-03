using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// WriteStyleTest 测试
    /// </summary>
    public class WriteStyleTest
    {
        [Fact]
        public void TestWriteWithHeaderStyle()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Styled");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Header1", "Header2", "Header3" },
                    new List<object?> { "Data1", "Data2", "Data3" }
                };
                
                writer.Write(sheetIndex, data);
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
        public void TestWriteWithCellStyle()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("CellStyled");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Bold", "Italic", "Underline" },
                    new List<object?> { "Value1", "Value2", "Value3" }
                };
                
                writer.Write(sheetIndex, data);
                writer.Save();
                
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
