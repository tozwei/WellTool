using System.Text;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV写出配置
    /// </summary>
    public class CsvWriteConfig
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
        public bool WriteHeader { get; set; } = true;

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 是否在值两端加引号
        /// </summary>
        public bool QuoteAll { get; set; } = false;

        /// <summary>
        /// 是否在包含分隔符的值两端加引号
        /// </summary>
        public bool QuoteAuto { get; set; } = true;

        /// <summary>
        /// 是否添加BOM
        /// </summary>
        public bool AddBOM { get; set; } = false;

        /// <summary>
        /// 换行符
        /// </summary>
        public string LineEnding { get; set; } = "\r\n";

        /// <summary>
        /// 创建默认配置
        /// </summary>
        public static CsvWriteConfig Default => new CsvWriteConfig();

        /// <summary>
        /// 创建TSV配置
        /// </summary>
        public static CsvWriteConfig Tsv => new CsvWriteConfig { Separator = '\t' };
    }
}
