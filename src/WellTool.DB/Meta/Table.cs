using System.Collections.Generic;
using System.Linq;

namespace WellTool.DB.Meta
{
    /// <summary>
    /// 数据库表信息
    /// </summary>
    public class Table
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表类型
        /// </summary>
        public TableType TableType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 列信息列表
        /// </summary>
        public List<Column> Columns { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Table()
        {
            Columns = new List<Column>();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">表名</param>
        public Table(string name)
        {
            Name = name;
            Columns = new List<Column>();
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="tableType">表类型</param>
        /// <param name="comment">描述</param>
        /// <param name="columns">列信息列表</param>
        public Table(string name, TableType tableType, string comment, List<Column> columns)
        {
            Name = name;
            TableType = tableType;
            Comment = comment;
            Columns = columns ?? new List<Column>();
        }

        /// <summary>
        /// 添加列信息
        /// </summary>
        /// <param name="column">列信息</param>
        /// <returns>this</returns>
        public Table AddColumn(Column column)
        {
            Columns.Add(column);
            return this;
        }

        /// <summary>
        /// 获取列信息
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>列信息</returns>
        public Column GetColumn(string columnName)
        {
            return Columns.FirstOrDefault(c => c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 获取主键列信息
        /// </summary>
        /// <returns>主键列信息</returns>
        public Column GetPrimaryKey()
        {
            return Columns.FirstOrDefault(c => c.IsPrimaryKey);
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return string.Format("Table{{name='{0}', tableType={1}, comment='{2}', columns={3}}}",
                Name, TableType, Comment, Columns.Count);
        }
    }
}