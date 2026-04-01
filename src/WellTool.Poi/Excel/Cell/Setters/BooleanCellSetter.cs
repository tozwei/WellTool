using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 布尔值单元格设置器
    /// </summary>
    public class BooleanCellSetter : ICellSetter
    {
        private readonly bool _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public BooleanCellSetter(bool value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为布尔值
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(_value);
        }
    }
}