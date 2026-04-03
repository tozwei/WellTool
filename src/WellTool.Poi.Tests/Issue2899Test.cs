using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// Issue2899Test 测试
    /// </summary>
    public class Issue2899Test
    {
        [Fact]
        public void TestIssue2899()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Special");
                
                var data = new List<List<object?>>
                {
                    new List<object?> { "Test" },
                    new List<object?> { null }
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
