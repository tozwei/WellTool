using System.Data;
using System.Collections.Generic;
using WellTool.DB;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 结果集处理辅助类
    /// </summary>
    public static class HandleHelper
    {
        /// <summary>
        /// 将结果集转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">结果集</param>
        /// <returns>对象</returns>
        public static T ToBean<T>(IDataReader reader) where T : new()
        {
            return new BeanHandler<T>().Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为对象列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="reader">结果集</param>
        /// <returns>对象列表</returns>
        public static List<T> ToBeanList<T>(IDataReader reader) where T : new()
        {
            return new BeanListHandler<T>().Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为Entity对象
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <param name="tableName">表名</param>
        /// <returns>Entity对象</returns>
        public static Entity ToEntity(IDataReader reader, string tableName = null)
        {
            return new EntityHandler(tableName).Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为Entity列表
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <param name="tableName">表名</param>
        /// <returns>Entity列表</returns>
        public static List<Entity> ToEntityList(IDataReader reader, string tableName = null)
        {
            return new EntityListHandler(tableName).Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为Entity集合
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <param name="tableName">表名</param>
        /// <returns>Entity集合</returns>
        public static HashSet<Entity> ToEntitySet(IDataReader reader, string tableName = null)
        {
            return new EntitySetHandler(tableName).Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为数字
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>数字</returns>
        public static decimal ToNumber(IDataReader reader)
        {
            return new NumberHandler().Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为字符串
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>字符串</returns>
        public static string ToString(IDataReader reader)
        {
            return new StringHandler().Handle(reader);
        }

        /// <summary>
        /// 将结果集转换为值列表
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="reader">结果集</param>
        /// <returns>值列表</returns>
        public static List<T> ToValueList<T>(IDataReader reader)
        {
            return new ValueListHandler<T>().Handle(reader);
        }
    }
}