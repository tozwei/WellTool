using System.Data;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 结果集处理器接口，用于处理数据库查询结果
    /// </summary>
    /// <typeparam name="T">处理结果类型</typeparam>
    public interface RsHandler<T>
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        T Handle(IDataReader reader);
    }
}