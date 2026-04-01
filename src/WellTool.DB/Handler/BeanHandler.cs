using System.Data;
using System.Reflection;
using System.Linq;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为单个对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class BeanHandler<T> : RsHandler<T> where T : new()
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public T Handle(IDataReader reader)
        {
            if (!reader.Read())
            {
                return default(T);
            }

            T bean = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();

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

            return bean;
        }
    }
}