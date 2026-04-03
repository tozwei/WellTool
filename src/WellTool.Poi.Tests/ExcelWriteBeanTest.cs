using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelWriteBeanTest 测试
    /// </summary>
    public class ExcelWriteBeanTest
    {
        [Fact]
        public void TestWriteBeanList()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                var beans = new List<TestBean>
                {
                    new TestBean { Id = 1, Name = "Item1", Value = 100.5m },
                    new TestBean { Id = 2, Name = "Item2", Value = 200.5m }
                };

                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Beans");
                writer.WriteBeans(sheetIndex, beans);
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
        public void TestWriteEmptyBeanList()
        {
            var tempFile = Path.GetTempFileName() + ".xlsx";
            try
            {
                var beans = new List<TestBean>();

                using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
                var sheetIndex = writer.CreateSheet("Empty");
                writer.WriteBeans(sheetIndex, beans);
                writer.Save();
                
                Assert.True(File.Exists(tempFile));
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        public class TestBean
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
}
