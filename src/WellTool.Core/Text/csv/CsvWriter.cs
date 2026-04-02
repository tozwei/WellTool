using System.IO;
using System.Text;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV写入器
    /// </summary>
    public class CsvWriter
    {
        private readonly TextWriter _writer;
        private readonly CsvConfig _config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="writer">文本写入器</param>
        /// <param name="config">CSV配置</param>
        public CsvWriter(TextWriter writer, CsvConfig config = null)
        {
            _writer = writer;
            _config = config ?? new CsvConfig();
        }

        /// <summary>
        /// 写入一行
        /// </summary>
        /// <param name="values">值数组</param>
        public void WriteLine(params string[] values)
        {
            if (values == null)
            {
                return;
            }

            for (var i = 0; i < values.Length; i++)
            {
                if (i > 0)
                {
                    _writer.Write(_config.Separator);
                }
                WriteValue(values[i]);
            }
            _writer.WriteLine();
        }

        /// <summary>
        /// 写入一个值
        /// </summary>
        /// <param name="value">值</param>
        private void WriteValue(string value)
        {
            if (value == null)
            {
                return;
            }

            var needsQuotes = false;
            var builder = new StringBuilder();

            for (var i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (c == _config.QuoteChar || c == _config.Separator || c == '\n' || c == '\r')
                {
                    needsQuotes = true;
                }
                if (c == _config.QuoteChar)
                {
                    builder.Append(_config.EscapeChar);
                }
                builder.Append(c);
            }

            if (needsQuotes)
            {
                _writer.Write(_config.QuoteChar);
                _writer.Write(builder.ToString());
                _writer.Write(_config.QuoteChar);
            }
            else
            {
                _writer.Write(builder.ToString());
            }
        }

        /// <summary>
        /// 关闭写入器
        /// </summary>
        public void Close()
        {
            _writer?.Close();
        }
    }
}
