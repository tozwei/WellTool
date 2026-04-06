using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// WorkbookUtilTest 测试
    /// </summary>
    public class WorkbookUtilTest
    {
        [Fact]
        public void TestCreateWorkbook()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            Assert.NotNull(workbook);
        }

        [Fact]
        public void TestCreateSheet()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            var sheet = workbook.CreateSheet("TestSheet");
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestGetSheetNames()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            workbook.CreateSheet("Sheet1");
            workbook.CreateSheet("Sheet2");
            
            var sheetNames = workbook.GetSheetNames();
            Assert.NotNull(sheetNames);
            Assert.Equal(2, sheetNames.Count);
        }

        [Fact]
        public void TestGetSheetByIndex()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            workbook.CreateSheet("First");
            workbook.CreateSheet("Second");
            
            var sheet = workbook.GetSheet(1);
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestGetSheetByName()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            workbook.CreateSheet("TestSheet");
            
            var sheet = workbook.GetSheet("TestSheet");
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestRemoveSheet()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.Create();
            workbook.CreateSheet("ToRemove");
            
            workbook.RemoveSheet(0);
            var sheetNames = workbook.GetSheetNames();
            Assert.Equal(0, sheetNames.Count);
        }
    }
}
