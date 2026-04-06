using NPOI.SS.Util;
using System.Text.RegularExpressions;

namespace WellTool.Poi
{
    /// <summary>
    /// 单元格工具类
    /// </summary>
    public static class CellUtil
    {
        private static readonly Regex CellRefRegex = new Regex(@"^([A-Z]+)(\d+)$", RegexOptions.Compiled);

        /// <summary>
        /// 获取单元格地址
        /// </summary>
        /// <param name="col">列索引（从0开始）</param>
        /// <param name="row">行索引（从0开始）</param>
        /// <returns>单元格地址，如 "A1"</returns>
        public static string GetCellAddress(int col, int row)
        {
            return new CellReference(row, col).FormatAsString();
        }

        /// <summary>
        /// 获取列索引
        /// </summary>
        /// <param name="cellRef">单元格地址，如 "A1"</param>
        /// <returns>列索引（从0开始）</returns>
        public static int GetCellColumn(string cellRef)
        {
            var match = CellRefRegex.Match(cellRef);
            if (!match.Success)
                throw new System.ArgumentException("Invalid cell reference: " + cellRef);

            string colStr = match.Groups[1].Value;
            return ColumnToIndex(colStr);
        }

        /// <summary>
        /// 获取行索引
        /// </summary>
        /// <param name="cellRef">单元格地址，如 "A1"</param>
        /// <returns>行索引（从0开始）</returns>
        public static int GetCellRow(string cellRef)
        {
            var match = CellRefRegex.Match(cellRef);
            if (!match.Success)
                throw new System.ArgumentException("Invalid cell reference: " + cellRef);

            string rowStr = match.Groups[2].Value;
            return int.Parse(rowStr) - 1;
        }

        private static int ColumnToIndex(string column)
        {
            int result = 0;
            foreach (char c in column)
            {
                result = result * 26 + (c - 'A' + 1);
            }
            return result - 1;
        }
    }
}