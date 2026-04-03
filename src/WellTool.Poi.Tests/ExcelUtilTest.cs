using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// ExcelUtilTest 测试
    /// </summary>
    public class ExcelUtilTest
    {
        [Fact]
        public void TestIndexToColName()
        {
            Assert.Equal("A", WellTool.Poi.ExcelUtil.IndexToColName(0));
            Assert.Equal("B", WellTool.Poi.ExcelUtil.IndexToColName(1));
            Assert.Equal("C", WellTool.Poi.ExcelUtil.IndexToColName(2));
            
            Assert.Equal("AA", WellTool.Poi.ExcelUtil.IndexToColName(26));
            Assert.Equal("AB", WellTool.Poi.ExcelUtil.IndexToColName(27));
            Assert.Equal("AC", WellTool.Poi.ExcelUtil.IndexToColName(28));
            
            Assert.Equal("AAA", WellTool.Poi.ExcelUtil.IndexToColName(702));
            Assert.Equal("AAB", WellTool.Poi.ExcelUtil.IndexToColName(703));
            Assert.Equal("AAC", WellTool.Poi.ExcelUtil.IndexToColName(704));
        }

        [Fact]
        public void TestColNameToIndex()
        {
            Assert.Equal(0, WellTool.Poi.ExcelUtil.ColNameToIndex("A"));
            Assert.Equal(1, WellTool.Poi.ExcelUtil.ColNameToIndex("B"));
            Assert.Equal(2, WellTool.Poi.ExcelUtil.ColNameToIndex("C"));
            
            Assert.Equal(26, WellTool.Poi.ExcelUtil.ColNameToIndex("AA"));
            Assert.Equal(27, WellTool.Poi.ExcelUtil.ColNameToIndex("AB"));
            Assert.Equal(28, WellTool.Poi.ExcelUtil.ColNameToIndex("AC"));
            
            Assert.Equal(702, WellTool.Poi.ExcelUtil.ColNameToIndex("AAA"));
            Assert.Equal(703, WellTool.Poi.ExcelUtil.ColNameToIndex("AAB"));
            Assert.Equal(704, WellTool.Poi.ExcelUtil.ColNameToIndex("AAC"));
        }

        [Fact]
        public void TestToLocation()
        {
            var location = WellTool.Poi.ExcelUtil.ToLocation("A11");
            Assert.Equal(0, location.X);
            Assert.Equal(10, location.Y);
            
            var location2 = WellTool.Poi.ExcelUtil.ToLocation("B2");
            Assert.Equal(1, location2.X);
            Assert.Equal(1, location2.Y);
        }

        [Fact]
        public void TestRoundTripConversion()
        {
            // Test round-trip conversion for column names
            for (int i = 0; i < 1000; i++)
            {
                var colName = WellTool.Poi.ExcelUtil.IndexToColName(i);
                var backToIndex = WellTool.Poi.ExcelUtil.ColNameToIndex(colName);
                Assert.Equal(i, backToIndex);
            }
        }
    }
}
