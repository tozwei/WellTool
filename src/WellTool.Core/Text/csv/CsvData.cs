using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV数据类
    /// </summary>
    public class CsvData
    {
        /// <summary>
        /// 表头
        /// </summary>
        public List<string> Headers { get; set; } = new List<string>();

        /// <summary>
        /// 行数据
        /// </summary>
        public List<CsvRow> Rows { get; set; } = new List<CsvRow>();

        /// <summary>
        /// 获取列数
        /// </summary>
        public int ColumnCount => Headers.Count;

        /// <summary>
        /// 获取行数
        /// </summary>
        public int RowCount => Rows.Count;

        /// <summary>
        /// 添加行
        /// </summary>
        public void AddRow(params string[] values)
        {
            Rows.Add(new CsvRow(this, values));
        }

        /// <summary>
        /// 添加行
        /// </summary>
        public void AddRow(IEnumerable<string> values)
        {
            Rows.Add(new CsvRow(this, values));
        }

        /// <summary>
        /// 获取行
        /// </summary>
        public CsvRow this[int index] => Rows[index];

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            Headers.Clear();
            Rows.Clear();
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            return $"CsvData {{ Headers: {ColumnCount}, Rows: {RowCount} }}";
        }
    }
}
