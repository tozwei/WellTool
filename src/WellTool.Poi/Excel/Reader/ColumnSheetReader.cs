using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// 读取单独一列
    /// </summary>
    public class ColumnSheetReader : AbstractSheetReader<List<object>>
    {
        private readonly int _columnIndex;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="columnIndex">列号，从0开始计数</param>
        /// <param name="startRowIndex">起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">结束行（包含，从0开始计数）</param>
        public ColumnSheetReader(int columnIndex, int startRowIndex, int endRowIndex)
            : base(startRowIndex, endRowIndex)
        {
            _columnIndex = columnIndex;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        public override List<object> Read(ISheet sheet)
        {
            var resultList = new List<object>();

            int startRowIndex = Math.Max(this.StartRowIndex, sheet.FirstRowNum); // 读取起始行（包含）
            int endRowIndex = Math.Min(this.EndRowIndex, sheet.LastRowNum); // 读取结束行（包含）

            object value;
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                var row = sheet.GetRow(i);
                var cell = CellUtil.GetCell(row, _columnIndex);
                value = CellUtil.GetCellValue(cell, CellEditor);
                if (value != null || !IgnoreEmptyRow)
                {
                    resultList.Add(value);
                }
            }

            return resultList;
        }
    }
}