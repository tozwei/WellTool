namespace WellTool.Core.Codec
{
    /// <summary>
    /// 编码接口
    /// </summary>
    /// <typeparam name="T">被编码的数据类型</typeparam>
    /// <typeparam name="R">编码后的数据类型</typeparam>
    public interface Encoder<in T, out R>
    {
        /// <summary>
        /// 执行编码
        /// </summary>
        /// <param name="data">被编码的数据</param>
        /// <returns>编码后的数据</returns>
        R Encode(T data);
    }
}