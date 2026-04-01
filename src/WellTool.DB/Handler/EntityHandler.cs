using System.Data;
using WellTool.DB;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为单个Entity对象
    /// </summary>
    public class EntityHandler : RsHandler<Entity>
    {
        private string _tableName;

        /// <summary>
        /// 构造
        /// </summary>
        public EntityHandler() : this(null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public EntityHandler(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public Entity Handle(IDataReader reader)
        {
            if (!reader.Read())
            {
                return null;
            }

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

            return entity;
        }
    }
}