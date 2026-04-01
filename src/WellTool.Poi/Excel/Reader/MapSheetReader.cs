using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using WellTool.Core.Util;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// 读取<see cref="ISheet"/>为Map的List列表形式
    /// </summary>
    public class MapSheetReader : AbstractSheetReader<List<Dictionary<string, object>>>
    {
        private readonly int _headerRowIndex;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="headerRowIndex">标题所在行，如果标题行在读取的内容行中间，这行做为数据将忽略</param>
        /// <param name="startRowIndex">起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">结束行（包含，从0开始计数）</param>
        public MapSheetReader(int headerRowIndex, int startRowIndex, int endRowIndex)
            : base(startRowIndex, endRowIndex)
        {
            _headerRowIndex = headerRowIndex;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        public override List<Dictionary<string, object>> Read(ISheet sheet)
        {
            // 边界判断
            var firstRowNum = sheet.FirstRowNum;
            var lastRowNum = sheet.LastRowNum;
            if (lastRowNum < 0)
            {
                return new List<Dictionary<string, object>>();
            }

            if (_headerRowIndex < firstRowNum)
            {
                throw new IndexOutOfRangeException($"Header row index {_headerRowIndex} is lower than first row index {firstRowNum}.");
            }
            else if (_headerRowIndex > lastRowNum)
            {
                throw new IndexOutOfRangeException($"Header row index {_headerRowIndex} is greater than last row index {lastRowNum}.");
            }
            else if (StartRowIndex > lastRowNum)
            {
                // 只有标题行的Excel，起始行是1，标题行（最后的行号是0）
                return new List<Dictionary<string, object>>();
            }
            var startRowIndex = Math.Max(this.StartRowIndex, firstRowNum); // 读取起始行（包含）
            var endRowIndex = Math.Min(this.EndRowIndex, lastRowNum); // 读取结束行（包含）

            // 读取header
            var headerList = AliasHeader(ReadRow(sheet, _headerRowIndex));

            var result = new List<Dictionary<string, object>>(endRowIndex - startRowIndex + 1);
            List<object> rowList;
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                // 跳过标题行
                if (i != _headerRowIndex)
                {
                    rowList = ReadRow(sheet, i);
                    if (rowList.Count > 0 || !IgnoreEmptyRow)
                    {
                        result.Add(IterUtil.ToDictionary(headerList, rowList, true));
                    }
                }
            }
            return result;
        }
    }
}