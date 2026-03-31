namespace WellTool.Crypto
{
    public class SmUtil
    {
        /// <summary>
        /// 创建SM2加密对象
        /// </summary>
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
            return new Symmetric.SM4(key);
        }
        
        /// <summary>
        /// SM3摘要
        /// </summary>
        public static string SM3(string data)
        {
            return Digest.SM3.DigestHex(data);
        }
    }
}