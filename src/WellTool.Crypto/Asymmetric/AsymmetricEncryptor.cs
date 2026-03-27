namespace WellTool.Crypto.Asymmetric
{
    /// <summary>
    /// 非对称加密接口
    /// </summary>
    public interface AsymmetricEncryptor
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <returns>加密后的数据</returns>
        byte[] Encrypt(byte[] data);
    }
}