using System.Data;
using System.Collections.Generic;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为值列表
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class ValueListHandler<T> : RsHandler<List<T>>
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public List<T> Handle(IDataReader reader)
        {
            List<T> list = new List<T>();

            while (reader.Read())
            {
                object value = reader.GetValue(0);
                if (value != DBNull.Value)
                {
                    try
                    {
                        list.Add((T)Convert.ChangeType(value, typeof(T)));
                    }
                    catch { }
                }
            }

            return list;
        }
    }
}