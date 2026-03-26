namespace WellTool.Core.Codec
{
    /// <summary>
    /// 解码接口
    /// </summary>
    /// <typeparam name="T">被解码的数据类型</typeparam>
    /// <typeparam name="R">解码后的数据类型</typeparam>
    public interface Decoder<in T, out R>
    {
        /// <summary>
        /// 执行解码
        /// </summary>
        /// <param name="encoded">被解码的数据</param>
        /// <returns>解码后的数据</returns>
        R Decode(T encoded);
    }
}