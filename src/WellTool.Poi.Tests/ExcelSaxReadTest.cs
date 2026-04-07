using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelSaxReadTest 测试 SAX 方式读取 Excel
    /// </summary>
    [Collection("EPPlusCollection")]
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
                
                // Read using SAX - 使用创建的sheet名称"Test"
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile, "Test");
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                // 验证至少有写入的3行数据
                Assert.True(result.Count >= 3, $"Expected at least 3 rows, got {result.Count}");
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
                
                // Read back - 使用"Large" sheet
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile, "Large");
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                // 验证至少有100行数据
                Assert.True(result.Count >= 100, $"Expected at least 100 rows, got {result.Count}");
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
