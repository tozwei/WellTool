namespace WellTool.Crypto
{
    /// <summary>
    /// 填充模式
    /// </summary>
    public enum Padding
    {
        /// <summary>
        /// 无填充
        /// </summary>
        NoPadding,

        /// <summary>
        /// PKCS#5填充
        /// </summary>
        PKCS5Padding,

        /// <summary>
        /// PKCS#7填充
        /// </summary>
        PKCS7Padding,

        /// <summary>
        /// ISO10126填充
        /// </summary>
        ISO10126Padding,

        /// <summary>
        /// ISO7816-4填充
        /// </summary>
        ISO7816_4Padding,

        /// <summary>
        /// 零填充
        /// </summary>
        ZeroPadding
    }
}