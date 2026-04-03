using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// BigExcelWriteTest 测试大数据量写入
    /// </summary>
    public class BigExcelWriteTest
    {
        [Fact]
        public void TestWriteLargeDataSet()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("LargeData");
                
                var data = new List<List<object?>>();
                
                // Add header
                data.Add(new List<object?> { "ID", "Name", "Value", "Description" });
                
                // Add 1000 rows of data
                for (int i = 0; i < 1000; i++)
                {
                    data.Add(new List<object?> 
                    { 
                        i, 
                        $"Item_{i}", 
                        i * 1.5, 
                        $"Description for item {i}" 
                    });
                }
                
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
        public void TestWriteWideDataSet()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("WideData");
                
                var data = new List<List<object?>>();
                
                // Create a row with 50 columns
                var header = new List<object?>();
                for (int i = 0; i < 50; i++)
                {
                    header.Add($"Col_{i}");
                }
                data.Add(header);
                
                // Add one data row
                var row = new List<object?>();
                for (int i = 0; i < 50; i++)
                {
                    row.Add($"Value_{i}");
                }
                data.Add(row);
                
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
