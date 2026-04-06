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
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            Assert.NotNull(workbook);
        }

        [Fact]
        public void TestCreateSheet()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            var sheet = workbook.CreateSheet("TestSheet");
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestGetSheetNames()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            workbook.CreateSheet("Sheet1");
            workbook.CreateSheet("Sheet2");
            
            var sheetCount = workbook.NumberOfSheets;
            Assert.True(sheetCount > 0);
            Assert.Equal(2, sheetCount);
        }

        [Fact]
        public void TestGetSheetByIndex()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            workbook.CreateSheet("First");
            workbook.CreateSheet("Second");
            
            var sheet = workbook.GetSheetAt(1);
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestGetSheetByName()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            workbook.CreateSheet("TestSheet");
            
            var sheet = workbook.GetSheet("TestSheet");
            Assert.NotNull(sheet);
        }

        [Fact]
        public void TestRemoveSheet()
        {
            var workbook = WellTool.Poi.Excel.WorkbookUtil.CreateBook(true);
            workbook.CreateSheet("ToRemove");
            
            workbook.RemoveSheetAt(0);
            var sheetCount = workbook.NumberOfSheets;
            Assert.Equal(0, sheetCount);
        }
    }
}
