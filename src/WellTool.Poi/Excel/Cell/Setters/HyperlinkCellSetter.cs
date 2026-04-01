using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 超链接单元格设置器
    /// </summary>
    public class HyperlinkCellSetter : ICellSetter
    {
        private readonly IHyperlink _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public HyperlinkCellSetter(IHyperlink value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为超链接
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.Hyperlink = _value;
            cell.SetCellValue(_value.Label);
        }
    }
}