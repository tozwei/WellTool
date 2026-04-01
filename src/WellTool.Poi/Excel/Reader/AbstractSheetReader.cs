using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// 抽象<see cref="ISheet"/>数据读取实现
    /// </summary>
    /// <typeparam name="T">读取类型</typeparam>
    public abstract class AbstractSheetReader<T> : ISheetReader<T>
    {
        /// <summary>
        /// 读取起始行（包含，从0开始计数）
        /// </summary>
        protected readonly int StartRowIndex;
        
        /// <summary>
        /// 读取结束行（包含，从0开始计数）
        /// </summary>
        protected readonly int EndRowIndex;
        
        /// <summary>
        /// 是否忽略空行
        /// </summary>
        protected bool IgnoreEmptyRow = true;
        
        /// <summary>
        /// 单元格值处理接口
        /// </summary>
        protected ICellEditor CellEditor;
        
        /// <summary>
        /// 标题别名
        /// </summary>
        private Dictionary<string, string> _headerAlias;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="startRowIndex">起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">结束行（包含，从0开始计数）</param>
        public AbstractSheetReader(int startRowIndex, int endRowIndex)
        {
            StartRowIndex = startRowIndex;
            EndRowIndex = endRowIndex;
        }

        /// <summary>
        /// 设置单元格值处理逻辑<br>
        /// 当Excel中的值并不能满足我们的读取要求时，通过传入一个编辑接口，可以对单元格值自定义，例如对数字和日期类型值转换为字符串等
        /// </summary>
        /// <param name="cellEditor">单元格值处理接口</param>
        public void SetCellEditor(ICellEditor cellEditor)
        {
            CellEditor = cellEditor;
        }

        /// <summary>
        /// 设置是否忽略空行
        /// </summary>
        /// <param name="ignoreEmptyRow">是否忽略空行</param>
        public void SetIgnoreEmptyRow(bool ignoreEmptyRow)
        {
            IgnoreEmptyRow = ignoreEmptyRow;
        }

        /// <summary>
        /// 设置标题行的别名Map
        /// </summary>
        /// <param name="headerAlias">别名Map</param>
        public void SetHeaderAlias(Dictionary<string, string> headerAlias)
        {
            _headerAlias = headerAlias;
        }

        /// <summary>
        /// 增加标题别名
        /// </summary>
        /// <param name="header">标题</param>
        /// <param name="alias">别名</param>
        public void AddHeaderAlias(string header, string alias)
        {
            var headerAlias = _headerAlias;
            if (headerAlias == null)
            {
                headerAlias = new Dictionary<string, string>();
                _headerAlias = headerAlias;
            }
            _headerAlias[header] = alias;
        }

        /// <summary>
        /// 转换标题别名，如果没有别名则使用原标题，当标题为空时，列号对应的字母便是header
        /// </summary>
        /// <param name="headerList">原标题列表</param>
        /// <returns>转换别名列表</returns>
        protected List<string> AliasHeader(List<object> headerList)
        {
            if (headerList == null || headerList.Count == 0)
            {
                return new List<string>();
            }

            var size = headerList.Count;
            var result = new List<string>(size);
            for (int i = 0; i < size; i++)
            {
                result.Add(AliasHeader(headerList[i], i));
            }
            return result;
        }

        /// <summary>
        /// 转换标题别名，如果没有别名则使用原标题，当标题为空时，列号对应的字母便是header
        /// </summary>
        /// <param name="headerObj">原标题</param>
        /// <param name="index">标题所在列号，当标题为空时，列号对应的字母便是header</param>
        /// <returns>转换别名</returns>
        protected string AliasHeader(object headerObj, int index)
        {
            if (headerObj == null)
            {
                return ExcelUtil.IndexToColName(index);
            }

            var header = headerObj.ToString();
            if (_headerAlias != null && _headerAlias.TryGetValue(header, out var alias))
            {
                return alias;
            }
            return header;
        }

        /// <summary>
        /// 读取某一行数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <param name="rowIndex">行号，从0开始</param>
        /// <returns>一行数据</returns>
        protected List<object> ReadRow(ISheet sheet, int rowIndex)
        {
            var row = sheet.GetRow(rowIndex);
            if (row == null)
            {
                return new List<object>();
            }

            var result = new List<object>();
            var cellCount = row.LastCellNum;
            for (int i = 0; i < cellCount; i++)
            {
                var cell = row.GetCell(i);
                var value = CellUtil.GetCellValue(cell);
                if (CellEditor != null)
                {
                    value = CellEditor.Edit(cell, value);
                }
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        public abstract T Read(ISheet sheet);
    }
}