namespace WellTool.Crypto
{
    /// <summary>
    /// 加密模式
    /// </summary>
    public enum CipherMode
    {
        /// <summary>
        /// 电子密码本模式
        /// </summary>
        ECB,

        /// <summary>
        /// 密码块链接模式
        /// </summary>
        CBC,

        /// <summary>
        /// 密码反馈模式
        /// </summary>
        CFB,

        /// <summary>
        /// 输出反馈模式
        /// </summary>
        OFB,

        /// <summary>
        /// 计数器模式
        /// </summary>
        CTR
    }
}