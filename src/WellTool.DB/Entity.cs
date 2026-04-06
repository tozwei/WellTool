using System.Collections.Generic;
using System.Data;

namespace WellTool.DB
{
    /// <summary>
    /// 数据库表实体，一个实体对应数据库中的一行数据
    /// </summary>
    public class Entity : Dictionary<string, object>
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Entity() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public Entity(string tableName) : this()
        {
            TableName = tableName;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="capacity">初始容量</param>
        public Entity(string tableName, int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase)
        {
            TableName = tableName;
        }

        /// <summary>
        /// 获取指定字段的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="column">列名</param>
        /// <returns>值</returns>
        public T Get<T>(string column)
        {
            if (TryGetValue(column, out object value))
            {
                return (T)value;
            }
            return default(T);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public Entity Set(string column, object value)
        {
            this[column] = value;
            return this;
        }

        /// <summary>
        /// 移除字段
        /// </summary>
        /// <param name="column">列名</param>
        /// <returns>this</returns>
        public Entity Remove(string column)
        {
            base.Remove(column);
            return this;
        }

        /// <summary>
        /// 清空所有字段
        /// </summary>
        /// <returns>this</returns>
        public Entity Clear()
        {
            base.Clear();
            return this;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns>表名</returns>
        public string GetTableName()
        {
            return TableName;
        }

        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>this</returns>
        public Entity SetTableName(string tableName)
        {
            TableName = tableName;
            return this;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>实体</returns>
        public static Entity Create(string tableName)
        {
            return new Entity(tableName);
        }

        /// <summary>
        /// 过滤字段
        /// </summary>
        /// <param name="keys">要保留的字段</param>
        /// <returns>过滤后的实体</returns>
        public Entity Filter(params string[] keys)
        {
            var result = new Entity(TableName);
            foreach (var key in keys)
            {
                if (ContainsKey(key))
                {
                    result[key] = this[key];
                }
            }
            return result;
        }

        /// <summary>
        /// 移除新字段
        /// </summary>
        /// <param name="keys">要保留的字段</param>
        /// <returns>移除后的实体</returns>
        public Entity RemoveNew(params string[] keys)
        {
            var result = new Entity(TableName);
            foreach (var key in this.Keys)
            {
                if (!keys.Contains(key))
                {
                    result[key] = this[key];
                }
            }
            return result;
        }

        /// <summary>
        /// 克隆实体
        /// </summary>
        /// <returns>克隆后的实体</returns>
        public Entity Clone()
        {
            var result = new Entity(TableName);
            foreach (var key in this.Keys)
            {
                result[key] = this[key];
            }
            return result;
        }

        /// <summary>
        /// 获取字段名列表
        /// </summary>
        /// <param name="includeTableName">是否包含表名</param>
        /// <returns>字段名列表</returns>
        public List<string> GetFieldNames(bool includeTableName = false)
        {
            var result = new List<string>();
            foreach (var key in this.Keys)
            {
                if (includeTableName && !string.IsNullOrEmpty(TableName))
                {
                    result.Add($"{TableName}.{key}");
                }
                else
                {
                    result.Add(key);
                }
            }
            return result;
        }
    }
}