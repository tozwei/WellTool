using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV读取器
    /// </summary>
    public class CsvReader
    {
        private readonly TextReader _reader;
        private readonly CsvConfig _config;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reader">文本读取器</param>
        /// <param name="config">CSV配置</param>
        public CsvReader(TextReader reader, CsvConfig config = null)
        {
            _reader = reader;
            _config = config ?? new CsvConfig();
        }

        /// <summary>
        /// 读取所有行
        /// </summary>
        /// <returns>所有行的数据</returns>
        public List<List<string>> ReadAll()
        {
            var result = new List<List<string>>();
            List<string> line;
            while ((line = ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }

        /// <summary>
        /// 读取一行
        /// </summary>
        /// <returns>一行的数据</returns>
        public List<string> ReadLine()
        {
            var line = _reader.ReadLine();
            if (line == null)
            {
                return null;
            }

            var result = new List<string>();
            var builder = new StringBuilder();
            var inQuotes = false;
            var i = 0;

            while (i < line.Length)
            {
                var c = line[i];

                if (c == _config.QuoteChar && (i == 0 || line[i - 1] != _config.EscapeChar))
                {
                    inQuotes = !inQuotes;
                }
                else if (c == _config.Separator && !inQuotes)
                {
                    result.Add(builder.ToString());
                    builder.Clear();
                }
                else if (c == _config.EscapeChar && i + 1 < line.Length)
                {
                    builder.Append(line[i + 1]);
                    i++;
                }
                else
                {
                    builder.Append(c);
                }

                i++;
            }

            result.Add(builder.ToString());
            return result;
        }

        /// <summary>
        /// 关闭读取器
        /// </summary>
        public void Close()
        {
            _reader?.Close();
        }
    }
}
