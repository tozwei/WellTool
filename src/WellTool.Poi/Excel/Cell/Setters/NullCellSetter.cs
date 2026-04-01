using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 空值单元格设置器
    /// </summary>
    public class NullCellSetter : ICellSetter
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static readonly NullCellSetter INSTANCE = new NullCellSetter();

        /// <summary>
        /// 设置单元格值为空白
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(string.Empty);
        }
    }
}