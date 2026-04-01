using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 控制台表格
    /// </summary>
    public class ConsoleTable
    {
        private readonly List<string> _headers;
        private readonly List<List<string>> _rows;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="headers">表头</param>
        public ConsoleTable(params string[] headers)
        {
            _headers = new List<string>(headers);
            _rows = new List<List<string>>();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="values">行数据</param>
        /// <returns>当前实例</returns>
        public ConsoleTable AddRow(params object[] values)
        {
            var row = new List<string>();
            foreach (var value in values)
            {
                row.Add(value?.ToString() ?? string.Empty);
            }
            _rows.Add(row);
            return this;
        }

        /// <summary>
        /// 输出表格
        /// </summary>
        public void Print()
        {
            if (_headers.Count == 0)
            {
                return;
            }

            // 计算每列的最大宽度
            var columnWidths = new int[_headers.Count];
            for (int i = 0; i < _headers.Count; i++)
            {
                columnWidths[i] = _headers[i].Length;
            }

            foreach (var row in _rows)
            {
                for (int i = 0; i < row.Count && i < _headers.Count; i++)
                {
                    if (row[i].Length > columnWidths[i])
                    {
                        columnWidths[i] = row[i].Length;
                    }
                }
            }

            // 输出表头
            PrintRow(_headers, columnWidths);

            // 输出分隔线
            PrintSeparator(columnWidths);

            // 输出数据行
            foreach (var row in _rows)
            {
                PrintRow(row, columnWidths);
            }
        }

        /// <summary>
        /// 输出行
        /// </summary>
        /// <param name="row">行数据</param>
        /// <param name="columnWidths">列宽</param>
        private void PrintRow(List<string> row, int[] columnWidths)
        {
            var sb = new StringBuilder();
            sb.Append("|");

            for (int i = 0; i < _headers.Count; i++)
            {
                var value = i < row.Count ? row[i] : string.Empty;
                sb.Append(" ")
                  .Append(value.PadRight(columnWidths[i]))
                  .Append(" |");
            }

            System.Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// 输出分隔线
        /// </summary>
        /// <param name="columnWidths">列宽</param>
        private void PrintSeparator(int[] columnWidths)
        {
            var sb = new StringBuilder();
            sb.Append("+");

            for (int i = 0; i < _headers.Count; i++)
            {
                sb.Append(new string('-', columnWidths[i] + 2))
                  .Append("+");
            }

            System.Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns>表格字符串</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var originalOut = System.Console.Out;

            using (var writer = new System.IO.StringWriter(sb))
            {
                System.System.Console.SetOut(writer);
                Print();
                System.System.Console.SetOut(originalOut);
            }

            return sb.ToString();
        }
    }
}