namespace WellTool.Crypto
{
    public class SmUtil
    {
        /// <summary>
        /// 创建SM2加密对象
        /// </summary>
        public static Asymmetric.SM2 CreateSM2()
        {
            return new Asymmetric.SM2();
        }

        /// <summary>
        /// 创建SM2加密对象
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="publicKey">公钥</param>
        public static Asymmetric.SM2 CreateSM2(byte[] privateKey, byte[] publicKey)
        {
            return new Asymmetric.SM2(privateKey, publicKey);
        }
        
        /// <summary>
        /// 创建SM2加密对象
        /// </summary>
        public static Asymmetric.SM2 CreateSM2(string privateKey, string publicKey)
        {
            return new Asymmetric.SM2(privateKey, publicKey);
        }
        
        /// <summary>
        /// 创建SM4加密对象
        /// </summary>
        public static Symmetric.SM4 CreateSM4(byte[] key)
        {
            return new Symmetric.SM4(key);
        }
        
        /// <summary>
        /// 创建SM4加密对象
        /// </summary>
        public static Symmetric.SM4 CreateSM4(string key)
        {
            return new Symmetric.SM4(System.Text.Encoding.UTF8.GetBytes(key));
        }
        
        /// <summary>
        /// SM3摘要
        /// </summary>
        public static string SM3(string data)
        {
            return new Digest.SM3().DigestHex(data);
        }
    }
}