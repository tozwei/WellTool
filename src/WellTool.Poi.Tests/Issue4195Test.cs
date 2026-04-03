using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// Issue4195Test 测试
    /// </summary>
    public class Issue4195Test
    {
        [Fact]
        public void TestIssue4195()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Test");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "A", "B", "C" }
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
