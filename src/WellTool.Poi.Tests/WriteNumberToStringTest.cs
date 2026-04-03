using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// WriteNumberToStringTest 测试
    /// </summary>
    public class WriteNumberToStringTest
    {
        [Fact]
        public void TestWriteNumberAsString()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Numbers");
                
                // Write numbers that should be stored as strings
                var data = new List<List<object?>>
                {
                    new List<object?> { "ID", "Code" },
                    new List<object?> { "001", "002" },
                    new List<object?> { "003", "004" }
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
        public void TestWriteMixedTypes()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Mixed");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "String", "Number", "Boolean", "Null" },
                    new List<object?> { "Text", 12345, true, null },
                    new List<object?> { "123", 0.123, false, "" }
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
        public void TestWriteLongNumbers()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("LongNums");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "LargeInt", "Precision" },
                    new List<object?> { 9223372036854775807L, 0.123456789012345 },
                    new List<object?> { -9223372036854775808L, -0.000000001 }
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
