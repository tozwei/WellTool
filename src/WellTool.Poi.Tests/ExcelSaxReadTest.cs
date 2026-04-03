using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelSaxReadTest 测试 SAX 方式读取 Excel
    /// </summary>
    public class ExcelSaxReadTest
    {
        [Fact]
        public void TestSaxReadBasic()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Create test file
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Test");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "A", "B", "C" },
                        new List<object?> { 1, 2, 3 },
                        new List<object?> { 4, 5, 6 }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                // Read using SAX
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                Assert.True(result.Count >= 3);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestSaxReadLargeFile()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Create large file
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Large");
                    var data = new List<List<object?>>();
                    
                    for (int i = 0; i < 100; i++)
                    {
                        var row = new List<object?>();
                        for (int j = 0; j < 10; j++)
                        {
                            row.Add($"Cell_{i}_{j}");
                        }
                        data.Add(row);
                    }
                    
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                // Read back
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                Assert.True(result.Count >= 100);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
