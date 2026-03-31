using System;

namespace WellTool.Crypto.Symmetric
{
    public class SM4 : SymmetricCrypto
    {
        public SM4(byte[] key, byte[] iv = null)
            : base(SymmetricAlgorithmType.SM4, key, iv)
        {
        }
        
        public SM4(string key, string iv = null)
            : base(SymmetricAlgorithmType.SM4, System.Text.Encoding.UTF8.GetBytes(key), 
                  string.IsNullOrEmpty(iv) ? null : System.Text.Encoding.UTF8.GetBytes(iv))
        {
        }
        
        public override byte[] Encrypt(byte[] data)
        {
            // 这里使用Bouncy Castle实现SM4加密
            // 实际实现需要引用Bouncy Castle库
            throw new NotImplementedException("SM4 encryption requires Bouncy Castle library");
        }
        
        public override byte[] Decrypt(byte[] data)
        {
            // 这里使用Bouncy Castle实现SM4解密
            // 实际实现需要引用Bouncy Castle库
            throw new NotImplementedException("SM4 decryption requires Bouncy Castle library");
        }
    }
}