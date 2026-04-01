using NPOI.SS.UserModel;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Editors
{
    /// <summary>
    /// POI中NUMRIC类型的值默认返回的是Double类型，此编辑器用于转换其为int型
    /// </summary>
    public class NumericToIntEditor : ICellEditor
    {
        /// <summary>
        /// 编辑，将数值转换为整数
        /// </summary>
        /// <param name="cell">单元格对象，可以获取单元格行、列样式等信息</param>
        /// <param name="value">单元格值</param>
        /// <returns>编辑后的对象</returns>
        public object Edit(ICell cell, object value)
        {
            if (value is double || value is int || value is long || value is float || value is decimal)
            {
                return System.Convert.ToInt32(value);
            }
            return value;
        }
    }
}