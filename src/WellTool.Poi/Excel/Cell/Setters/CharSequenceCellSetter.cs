using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 字符串单元格设置器
    /// </summary>
    public class CharSequenceCellSetter : ICellSetter
    {
        private readonly string _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public CharSequenceCellSetter(string value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为字符串
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(_value);
        }
    }
}