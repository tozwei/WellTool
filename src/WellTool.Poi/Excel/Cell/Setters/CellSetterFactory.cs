using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// <see cref="ICellSetter"/> 简单静态工厂类，用于根据值类型创建对应的<see cref="ICellSetter"/>
    /// </summary>
    public static class CellSetterFactory
    {
        /// <summary>
        /// 创建值对应类型的<see cref="ICellSetter"/>
        /// </summary>
        /// <param name="value">值</param>
        /// <returns><see cref="ICellSetter"/></returns>
        public static ICellSetter CreateCellSetter(object value)
        {
            if (value == null)
            {
                return NullCellSetter.INSTANCE;
            }
            else if (value is ICellSetter)
            {
                return (ICellSetter)value;
            }
            else if (value is System.DateTime)
            {
                return new DateCellSetter((System.DateTime)value);
            }
            else if (value is System.Boolean)
            {
                return new BooleanCellSetter((System.Boolean)value);
            }
            else if (value is IRichTextString)
            {
                return new RichTextCellSetter((IRichTextString)value);
            }
            else if (value is double)
            {
                return new NumberCellSetter((double)value);
            }
            else if (value is float)
            {
                return new NumberCellSetter((float)value);
            }
            else if (value is int)
            {
                return new NumberCellSetter((int)value);
            }
            else if (value is long)
            {
                return new NumberCellSetter((long)value);
            }
            else if (value is decimal)
            {
                return new NumberCellSetter((decimal)value);
            }
            else if (value is short)
            {
                return new NumberCellSetter((short)value);
            }
            else if (value is byte)
            {
                return new NumberCellSetter((byte)value);
            }
            else if (value is IHyperlink)
            {
                return new HyperlinkCellSetter((IHyperlink)value);
            }
            else
            {
                return new CharSequenceCellSetter(value.ToString());
            }
        }
    }
}