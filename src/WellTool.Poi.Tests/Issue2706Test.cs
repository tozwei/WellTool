using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// Issue2706Test 测试
    /// </summary>
    public class Issue2706Test
    {
        [Fact]
        public void TestIssue2706()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Test");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { 1, 2, 3, 4, 5 }
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
