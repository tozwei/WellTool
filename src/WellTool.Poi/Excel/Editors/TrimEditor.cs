using NPOI.SS.UserModel;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Editors
{
    /// <summary>
    /// 去除String类型的单元格值两边的空格
    /// </summary>
    public class TrimEditor : ICellEditor
    {
        /// <summary>
        /// 编辑，去除字符串的前后空白
        /// </summary>
        /// <param name="cell">单元格对象，可以获取单元格行、列样式等信息</param>
        /// <param name="value">单元格值</param>
        /// <returns>编辑后的对象</returns>
        public object Edit(ICell cell, object value)
        {
            if (value is string)
            {
                return ((string)value).Trim();
            }
            return value;
        }
    }
}