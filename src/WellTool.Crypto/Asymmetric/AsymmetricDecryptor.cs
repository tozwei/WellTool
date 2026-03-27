namespace WellTool.Crypto.Asymmetric
{
    /// <summary>
    /// 非对称解密接口
    /// </summary>
    public interface AsymmetricDecryptor
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <returns>解密后的数据</returns>
        byte[] Decrypt(byte[] data);
    }
}