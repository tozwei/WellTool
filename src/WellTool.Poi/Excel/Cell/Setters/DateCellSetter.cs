using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 日期值单元格设置器
    /// </summary>
    public class DateCellSetter : ICellSetter
    {
        private readonly System.DateTime _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public DateCellSetter(System.DateTime value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为日期
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(_value);
        }
    }
}