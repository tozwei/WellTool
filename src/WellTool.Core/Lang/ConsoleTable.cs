using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 控制台表格
    /// </summary>
    public class ConsoleTable
    {
        private readonly List<string[]> _rows = new List<string[]>();
        private string[] _headers;
        private readonly List<int> _columnWidths;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConsoleTable(params string[] headers)
        {
            _headers = headers;
            _columnWidths = new List<int>(headers.Length);
            for (int i = 0; i < headers.Length; i++)
            {
                _columnWidths.Add(headers[i].Length);
            }
        }

        /// <summary>
        /// 添加行
        /// </summary>
        public ConsoleTable AddRow(params object[] values)
        {
            if (values.Length != _headers.Length)
            {
                throw new ArgumentException($"Row must have {_headers.Length} columns");
            }

            var row = values.Select(v => v?.ToString() ?? "").ToArray();
            _rows.Add(row);

            for (int i = 0; i < row.Length; i++)
            {
                if (row[i].Length > _columnWidths[i])
                {
                    _columnWidths[i] = row[i].Length;
                }
            }

            return this;
        }

        /// <summary>
        /// 添加多行
        /// </summary>
        public ConsoleTable AddRows(IEnumerable<object[]> rows)
        {
            foreach (var row in rows)
            {
                AddRow(row);
            }
            return this;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            // 标题行
            sb.Append('|');
            for (int i = 0; i < _headers.Length; i++)
            {
                sb.Append(' ');
                sb.Append(_headers[i].PadRight(_columnWidths[i]));
                sb.Append(" |");
            }
            sb.AppendLine();

            // 分隔行
            sb.Append('|');
            for (int i = 0; i < _headers.Length; i++)
            {
                sb.Append('-');
                sb.Append(new string('-', _columnWidths[i]));
                sb.Append("-|");
            }
            sb.AppendLine();

            // 数据行
            foreach (var row in _rows)
            {
                sb.Append('|');
                for (int i = 0; i < row.Length; i++)
                {
                    sb.Append(' ');
                    sb.Append(row[i].PadRight(_columnWidths[i]));
                    sb.Append(" |");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 输出到控制台
        /// </summary>
        public void Print()
        {
            Console.WriteLine(ToString());
        }

        /// <summary>
        /// 创建表格
        /// </summary>
        public static ConsoleTable Create(params string[] headers)
        {
            return new ConsoleTable(headers);
        }
    }
}
