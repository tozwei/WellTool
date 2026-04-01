using NPOI.SS.UserModel;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Cell.Values
{
    /// <summary>
    /// ERROR类型单元格值
    /// </summary>
    public class ErrorCellValue : ICellValue<string>
    {
        private readonly ICell _cell;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="cell">单元格</param>
        public ErrorCellValue(ICell cell)
        {
            _cell = cell;
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <returns>错误字符串</returns>
        public string GetValue()
        {
            var errorValue = _cell.ErrorCellValue;
            var error = FormulaError.ForInt(errorValue);
            return error?.String ?? string.Empty;
        }
    }
}