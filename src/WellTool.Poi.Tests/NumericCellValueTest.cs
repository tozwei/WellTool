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
                
                Console.WriteLine("Step 1: Write completed. File exists: " + File.Exists(tempFile));
                
                // Read them back - wrap in try-catch to see actual exception
                // 读取Numbers sheet（索引1，因为构造函数会创建默认Sheet1）
                List<List<object?>> result;
                try
                {
                    using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile, "Numbers");
                    result = reader.ReadAll();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception during read: " + ex.GetType().Name);
                    Console.WriteLine("Message: " + ex.Message);
                    Console.WriteLine("Stack: " + ex.StackTrace);
                    throw;
                }
                
                Assert.NotNull(result);
                Console.WriteLine($"Step 2: Read completed. Result count: {result.Count}");
                
                // 验证至少有2行数据
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
