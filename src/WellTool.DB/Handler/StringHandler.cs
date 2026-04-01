using System.Data;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为字符串
    /// </summary>
    public class StringHandler : RsHandler<string>
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public string Handle(IDataReader reader)
        {
            if (!reader.Read())
            {
                return null;
            }

            object value = reader.GetValue(0);
            if (value == DBNull.Value)
            {
                return null;
            }

            return value.ToString();
        }
    }
}