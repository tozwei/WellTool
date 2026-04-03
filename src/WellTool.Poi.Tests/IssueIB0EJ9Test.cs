using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// IssueIB0EJ9Test 测试
    /// </summary>
    public class IssueIB0EJ9Test
    {
        [Fact]
        public void TestIssueIB0EJ9()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Test");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Value" }
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
