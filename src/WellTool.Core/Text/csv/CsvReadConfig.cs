using System.Text;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV读取配置
    /// </summary>
    public class CsvReadConfig
    {
        /// <summary>
        /// 分隔符，默认为逗号
        /// </summary>
        public char Separator { get; set; } = ',';

        /// <summary>
        /// 引号字符，默认为双引号
        /// </summary>
        public char QuoteChar { get; set; } = '"';

        /// <summary>
        /// 是否包含表头
        /// </summary>
        public bool HasHeader { get; set; } = true;

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 跳过前几行
        /// </summary>
        public int SkipLines { get; set; } = 0;

        /// <summary>
        /// 最大列数
        /// </summary>
        public int MaxColumns { get; set; } = 0;

        /// <summary>
        /// 是否忽略空行
        /// </summary>
        public bool IgnoreEmptyLines { get; set; } = true;

        /// <summary>
        /// 是否忽略引号
        /// </summary>
        public bool IgnoreQuotes { get; set; } = false;

        /// <summary>
        /// 创建默认配置
        /// </summary>
        public static CsvReadConfig Default => new CsvReadConfig();

        /// <summary>
        /// 创建TSV配置
        /// </summary>
        public static CsvReadConfig Tsv => new CsvReadConfig { Separator = '\t' };
    }
}
