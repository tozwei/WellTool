using NPOI.SS.UserModel;

namespace WellTool.Poi.Excel.Cell.Setters
{
    /// <summary>
    /// 数值单元格设置器
    /// </summary>
    public class NumberCellSetter : ICellSetter
    {
        private readonly double _value;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(double value)
        {
            _value = value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(float value)
        {
            _value = value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(int value)
        {
            _value = value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(long value)
        {
            _value = value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(decimal value)
        {
            _value = (double)value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(short value)
        {
            _value = value;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value">值</param>
        public NumberCellSetter(byte value)
        {
            _value = value;
        }

        /// <summary>
        /// 设置单元格值为数值
        /// </summary>
        /// <param name="cell">单元格</param>
        public void SetValue(ICell cell)
        {
            cell.SetCellValue(_value);
        }
    }
}