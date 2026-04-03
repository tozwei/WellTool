using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelReaderToWriterTest 测试
    /// </summary>
    public class ExcelReaderToWriterTest
    {
        [Fact]
        public void TestReadAndWrite()
        {
            var tempFile1 = Path.GetTempFileName() + ".xlsx";
            var tempFile2 = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Create source file
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile1))
                {
                    var sheetIndex = writer.CreateSheet("Source");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "Col1", "Col2", "Col3" },
                        new List<object?> { "A", "B", "C" },
                        new List<object?> { 1, 2, 3 }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                // Read and write to new file
                using (var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile1))
                {
                    var data = reader.ReadAll();
                    
                    using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile2);
                    var sheetIndex = writer.CreateSheet("Copy");
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                Assert.True(File.Exists(tempFile2));
            }
            finally
            {
                if (File.Exists(tempFile1))
                    File.Delete(tempFile1);
                if (File.Exists(tempFile2))
                    File.Delete(tempFile2);
            }
        }

        [Fact]
        public void TestModifyAndSave()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // Create original file
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    var sheetIndex = writer.CreateSheet("Original");
                    var data = new List<List<object?>>
                    {
                        new List<object?> { "Value" },
                        new List<object?> { "Original" }
                    };
                    writer.Write(sheetIndex, data);
                    writer.Save();
                }
                
                // Read, modify, and save
                using (var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile))
                {
                    var data = reader.ReadAll();
                    
                    // Modify first cell in first data row
                    if (data.Count > 1)
                    {
                        data[1][0] = "Modified";
                    }
                    
                    using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                    writer.Write(0, data);
                    writer.Save();
                }
                
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
