using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// NumericCellValueTest 测试
    /// </summary>
    [Collection("EPPlusCollection")]
    public class NumericCellValueTest
    {
        [Fact]
        public void TestWriteIntegerValues()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Integers");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Positive", "Negative", "Zero" },
                    new List<object?> { 100, -50, 0 },
                    new List<object?> { int.MaxValue, int.MinValue, 1 }
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
        public void TestWriteDoubleValues()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Doubles");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Normal", "Scientific", "Infinity" },
                    new List<object?> { 3.14159, 1.23E+10, double.PositiveInfinity }
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
        public void TestWriteDecimalValues()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Decimals");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Price", "Amount" },
                    new List<object?> { 99.99m, 1234.56m },
                    new List<object?> { 0.01m, -0.99m }
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
        public void TestReadNumericValues()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Write numbers
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Numbers");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { 123, 456.78 },
                        new List<object?> { -100, 0.0 }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                // Read them back
                using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
                var result = reader.ReadAll();
                
                Assert.NotNull(result);
                // 验证读取的数据（只验证至少有写入的数据）
                Assert.True(result.Count >= 2, $"Expected at least 2 rows, got {result.Count}");
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
