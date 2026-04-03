using Xunit;

namespace WellTool.Poi.Tests
{
    /// <summary>
    /// CellEditorTest 测试
    /// </summary>
    public class CellEditorTest
    {
        [Fact]
        public void TestCreateCellEditor()
        {
            var editor = new WellTool.Poi.Excel.CellEditor();
            Assert.NotNull(editor);
        }

        [Fact]
        public void TestCellEditorSetValue()
        {
            var editor = new WellTool.Poi.Excel.CellEditor();
            // Test setting string value
            editor.SetValue("Test Value");
            Assert.Equal("Test Value", editor.GetValue());
        }

        [Fact]
        public void TestCellEditorSetNumericValue()
        {
            var editor = new WellTool.Poi.Excel.CellEditor();
            editor.SetValue(12345);
            Assert.Equal(12345, editor.GetValue());
        }

        [Fact]
        public void TestCellEditorSetBooleanValue()
        {
            var editor = new WellTool.Poi.Excel.CellEditor();
            editor.SetValue(true);
            Assert.Equal(true, editor.GetValue());
        }

        [Fact]
        public void TestCellEditorSetFormula()
        {
            var editor = new WellTool.Poi.Excel.CellEditor();
            editor.SetFormula("SUM(A1:A10)");
            Assert.Equal("SUM(A1:A10)", editor.GetFormula());
        }
    }
}
