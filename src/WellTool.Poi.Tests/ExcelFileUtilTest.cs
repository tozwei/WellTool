using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelFileUtilTest 测试
    /// </summary>
    public class ExcelFileUtilTest
    {
        [Fact]
        public void TestCreateExcelFile()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                WellTool.Poi.ExcelFileUtil.Create(tempFile);
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestCreateExcelFileWithSheets()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                var sheetNames = new List<string> { "Sheet1", "Sheet2", "Sheet3" };
                WellTool.Poi.ExcelFileUtil.Create(tempFile, sheetNames);
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void TestOpenExistingFile()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                // First create a file
                using (var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile))
                {
                    writer.CreateSheet("Original");
                    writer.Save();
                }
                
                // Then open it with FileUtil
                using var workbook = WellTool.Poi.ExcelFileUtil.Open(tempFile);
                Assert.NotNull(workbook);
                Assert.True(workbook.GetSheetNames().Count > 0);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
