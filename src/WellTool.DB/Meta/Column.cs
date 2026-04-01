namespace WellTool.DB.Meta
{
    /// <summary>
    /// 数据库表列信息
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 列大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalDigits { get; set; }

        /// <summary>
        /// 是否允许为null
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Column()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="type">数据类型</param>
        public Column(string name, string type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">列名</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">列大小</param>
        /// <param name="decimalDigits">小数位数</param>
        /// <param name="isNullable">是否允许为null</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="comment">描述</param>
        /// <param name="isPrimaryKey">是否为主键</param>
        public Column(string name, string type, int size, int decimalDigits, bool isNullable, object defaultValue, string comment, bool isPrimaryKey)
        {
            Name = name;
            Type = type;
            Size = size;
            DecimalDigits = decimalDigits;
            IsNullable = isNullable;
            DefaultValue = defaultValue;
            Comment = comment;
            IsPrimaryKey = isPrimaryKey;
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("Column{{name='{0}', type='{1}', size={2}, decimalDigits={3}, isNullable={4}, defaultValue={5}, comment='{6}', isPrimaryKey={7}}}",
                Name, Type, Size, DecimalDigits, IsNullable, DefaultValue, Comment, IsPrimaryKey);
        }
    }
}