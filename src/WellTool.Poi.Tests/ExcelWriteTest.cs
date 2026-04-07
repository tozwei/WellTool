using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelWriteTest 测试
    /// </summary>
    [Collection("EPPlusCollection")]
    public class ExcelWriteTest
    {
        [Fact]
        public void TestWriteBasicData()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Data");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Header1", "Header2", "Header3" },
                    new List<object?> { "Value1", "Value2", "Value3" }
                };
                
                writer.Write(sheetIndex, data);
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

        [Fact]
        public void TestWriteNumericData()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Numbers");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Integer", "Double", "Decimal" },
                    new List<object?> { 42, 3.14159, 999.99 },
                    new List<object?> { -100, 0.0, -0.01 }
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
        public void TestWriteBooleanData()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Booleans");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Flag", "Status" },
                    new List<object?> { true, "Active" },
                    new List<object?> { false, "Inactive" }
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
        public void TestWriteEmptySheet()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Empty");
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
        public void TestWriteMultipleSheets()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                
                var sheet1 = writer.CreateSheet("Sheet1");
                writer.Write(sheet1, new List<List<object?>> { new List<object?> { "Data1" } });
                
                var sheet2 = writer.CreateSheet("Sheet2");
                writer.Write(sheet2, new List<List<object?>> { new List<object?> { "Data2" } });
                
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
