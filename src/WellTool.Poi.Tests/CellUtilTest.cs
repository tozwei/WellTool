using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// CellUtilTest 测试
    /// </summary>
    public class CellUtilTest
    {
        [Fact]
        public void TestGetCellAddress()
        {
            // Test basic cell address
            var address = WellTool.Poi.Excel.Cell.CellUtil.GetCellAddress(0, 0);
            Assert.Equal("A1", address);
            
            var address2 = WellTool.Poi.Excel.Cell.CellUtil.GetCellAddress(1, 1);
            Assert.Equal("B2", address2);
        }

        [Fact]
        public void TestGetCellColumn()
        {
            Assert.Equal(0, WellTool.Poi.Excel.Cell.CellUtil.GetCellColumn("A1"));
            Assert.Equal(1, WellTool.Poi.Excel.Cell.CellUtil.GetCellColumn("B2"));
            Assert.Equal(25, WellTool.Poi.Excel.Cell.CellUtil.GetCellColumn("Z1"));
        }

        [Fact]
        public void TestGetCellRow()
        {
            Assert.Equal(0, WellTool.Poi.Excel.Cell.CellUtil.GetCellRow("A1"));
            Assert.Equal(1, WellTool.Poi.Excel.Cell.CellUtil.GetCellRow("B2"));
            Assert.Equal(9, WellTool.Poi.Excel.Cell.CellUtil.GetCellRow("A10"));
        }

        [Fact]
        public void TestGetCellAddressWithLargeValues()
        {
            var address = WellTool.Poi.Excel.Cell.CellUtil.GetCellAddress(26, 100);
            Assert.NotNull(address);
        }
    }
}
