using System.Data;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 将结果集处理为数字
    /// </summary>
    public class NumberHandler : RsHandler<decimal>
    {
        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public decimal Handle(IDataReader reader)
        {
            if (!reader.Read())
            {
                return 0;
            }

            object value = reader.GetValue(0);
            if (value == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToDecimal(value);
        }
    }
}