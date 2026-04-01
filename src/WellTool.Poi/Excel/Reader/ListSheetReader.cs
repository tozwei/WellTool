using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// 读取<see cref="ISheet"/>为List列表形式
    /// </summary>
    public class ListSheetReader : AbstractSheetReader<List<List<object>>>
    {
        /// <summary>
        /// 是否首行作为标题行转换别名
        /// </summary>
        private readonly bool _aliasFirstLine;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="startRowIndex">起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">结束行（包含，从0开始计数）</param>
        /// <param name="aliasFirstLine">是否首行作为标题行转换别名</param>
        public ListSheetReader(int startRowIndex, int endRowIndex, bool aliasFirstLine)
            : base(startRowIndex, endRowIndex)
        {
            _aliasFirstLine = aliasFirstLine;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        public override List<List<object>> Read(ISheet sheet)
        {
            var resultList = new List<List<object>>();

            int startRowIndex = Math.Max(this.StartRowIndex, sheet.FirstRowNum); // 读取起始行（包含）
            int endRowIndex = Math.Min(this.EndRowIndex, sheet.LastRowNum); // 读取结束行（包含）
            List<object> rowList;
            for (int i = startRowIndex; i <= endRowIndex; i++)
            {
                rowList = ReadRow(sheet, i);
                if (rowList.Count > 0 || !IgnoreEmptyRow)
                {
                    if (_aliasFirstLine && i == startRowIndex)
                    {
                        // 第一行作为标题行，替换别名
                        rowList = AliasHeader(rowList).ConvertAll(obj => (object)obj);
                    }
                    resultList.Add(rowList);
                }
            }
            return resultList;
        }
    }
}