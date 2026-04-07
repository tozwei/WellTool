using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelReadTest 测试
    /// </summary>
    [Collection("EPPlusCollection")]
    public class ExcelReadTest
    {
        [Fact]
        public void TestReadExcelFile()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Create test data
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Sheet1");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "ID", "Name", "Value" },
                        new List<object?> { 1, "Item1", 100 },
                        new List<object?> { 2, "Item2", 200 }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }

                // Read the file
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                Assert.True(result.Count >= 2);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestReadSheetByName()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("TestSheet");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "Col1", "Col2" },
                        new List<object?> { "A", "B" }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }

                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadSheet(0);
                
                Assert.NotNull(result);
                // 验证至少有2行数据
                Assert.True(result.Count >= 2, $"Expected at least 2 rows, got {result.Count}");
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestReadEmptyCells()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Sheet1");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "A", null, "C" },
                        new List<object?> { null, "E", null }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }

                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
