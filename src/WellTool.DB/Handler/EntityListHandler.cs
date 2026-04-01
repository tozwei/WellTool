using System.Data;
using System.Collections.Generic;
using WellTool.DB;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为Entity列表
    /// </summary>
    public class EntityListHandler : RsHandler<List<Entity>>
    {
        private string _tableName;

        /// <summary>
        /// 构造
        /// </summary>
        public EntityListHandler() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public EntityListHandler(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public List<Entity> Handle(IDataReader reader)
        {
            List<Entity> list = new List<Entity>();

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

                list.Add(entity);
            }

            return list;
        }
    }
}