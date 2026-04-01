using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 富文本单元格设置器
    /// </summary>
    public class RichTextCellSetter : ICellSetter
    {
        private readonly IRichTextString _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public RichTextCellSetter(IRichTextString value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为富文本
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(_value);
        }
    }
}