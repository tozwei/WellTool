using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Cell.Values
{
    /// <summary>
    /// 数字类型单元格值<br>
    /// 单元格值可能为Long、Double、Date
    /// </summary>
    public class NumericCellValue : ICellValue<object>
    {
        private readonly ICell _cell;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="cell">单元格</param>
        public NumericCellValue(ICell cell)
        {
            _cell = cell;
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <returns>值，可能为Long、Double、Date</returns>
        public object GetValue()
        {
            var value = _cell.NumericCellValue;

            var style = _cell.CellStyle;
            if (style != null)
            {
                // 判断是否为日期
                try
                {
                    var dateCellValue = _cell.DateCellValue;
                    if (dateCellValue.Year == 1899)
                    {
                        // 1899年写入会导致数据错乱，读取到1899年证明这个单元格的信息不关注年月日
                        return dateCellValue.TimeOfDay;
                    }
                    // 直接返回日期
                    return dateCellValue;
                }
                catch
                {
                    // 不是日期类型，继续处理
                }

                var format = style.GetDataFormatString();
                // 普通数字
                if (!string.IsNullOrEmpty(format) && format.IndexOf('.') < 0)
                {
                    var longPart = (long)value;
                    if (longPart == value)
                    {
                        // 对于无小数部分的数字类型，转为Long
                        return longPart;
                    }
                }
            }

            // 某些Excel单元格值为double计算结果，可能导致精度问题，通过转换解决精度问题。
            return double.Parse(NumberToTextConverter.ToText(value));
        }
    }
}