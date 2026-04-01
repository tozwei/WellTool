using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为对象列表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class BeanListHandler<T> : RsHandler<List<T>> where T : new()
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public List<T> Handle(IDataReader reader)
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                T bean = new T();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    object value = reader.GetValue(i);

                    if (value != DBNull.Value)
                    {
                        PropertyInfo property = properties.FirstOrDefault(p => p.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
                        if (property != null && property.CanWrite)
                        {
                            try
                            {
                                property.SetValue(bean, Convert.ChangeType(value, property.PropertyType));
                            }
                            catch { }
                        }
                    }
                }

                list.Add(bean);
            }

            return list;
        }
    }
}