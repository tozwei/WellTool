using System.Data;
using System.Collections.Generic;
using WellTool.DB;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为Entity集合
    /// </summary>
    public class EntitySetHandler : RsHandler<HashSet<Entity>>
    {
        private string _tableName;

        /// <summary>
        /// 构造
        /// </summary>
        public EntitySetHandler() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public EntitySetHandler(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public HashSet<Entity> Handle(IDataReader reader)
        {
            HashSet<Entity> set = new HashSet<Entity>();

            while (reader.Read())
            {
                Entity entity = new Entity(_tableName);

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    if (value != DBNull.Value)
                    {
                        entity[columnName] = value;
                    }
                }

                set.Add(entity);
            }

            return set;
        }
    }
}