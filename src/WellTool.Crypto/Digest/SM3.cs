using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace WellTool.Crypto.Digest
{
    /// <summary>
    /// 国密SM3杂凑（摘要）算法
    /// 
    /// <p>
    /// 国密算法包括：
    /// <ol>
    ///     <li>非对称加密和签名：SM2</li>
    ///     <li>摘要签名算法：SM3</li>
    ///     <li>对称加密：SM4</li>
    /// </ol>
    /// </summary>
    public class SM3
    {
        public const string ALGORITHM_NAME = "SM3";
        
        /// <summary>
        /// 盐值
        /// </summary>
        protected byte[]? Salt;
        
        /// <summary>
        /// 盐值位置
        /// </summary>
        protected int SaltPosition = 0;
        
        /// <summary>
        /// 摘要次数
        /// </summary>
        protected int DigestCount = 1;

        /// <summary>
        /// 创建SM3实例
        /// </summary>
        public static SM3 Create()
        {
            return new SM3();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SM3()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="salt">盐值</param>
        public SM3(byte[] salt)
        {
            Salt = salt;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="salt">盐值</param>
        /// <param name="digestCount">摘要次数，当此值小于等于1时默认为1</param>
        public SM3(byte[] salt, int digestCount)
        {
            Salt = salt;
            DigestCount = digestCount <= 1 ? 1 : digestCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="salt">盐值</param>
        /// <param name="saltPosition">加盐位置</param>
        /// <param name="digestCount">摘要次数</param>
        public SM3(byte[] salt, int saltPosition, int digestCount)
        {
            Salt = salt;
            SaltPosition = saltPosition;
            DigestCount = digestCount <= 1 ? 1 : digestCount;
        }
        
        /// <summary>
        /// 计算SM3摘要
        /// </summary>
        public byte[] Digest(byte[] data)
        {
            var digest = new SM3Digest();
            byte[] dataWithSalt = AddSalt(data);
            
            for (int i = 0; i < DigestCount; i++)
            {
                digest.Reset();
                digest.BlockUpdate(dataWithSalt, 0, dataWithSalt.Length);
                digest.DoFinal(dataWithSalt, 0);
            }
            
            return dataWithSalt;
        }

        /// <summary>
        /// 计算SM3摘要
        /// </summary>
        public static byte[] DoDigest(byte[] data)
        {
            var digest = new SM3Digest();
            digest.BlockUpdate(data, 0, data.Length);
            byte[] result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }

        /// <summary>
        /// 计算SM3摘要并转为十六进制字符串
        /// </summary>
        public string DigestHex(byte[] data)
        {
            byte[] digest = Digest(data);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 计算SM3摘要并转为十六进制字符串
        /// </summary>
        public string DigestHex(string data)
        {
            return DigestHex(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 计算SM3摘要
        /// </summary>
        public byte[] Digest(string data)
        {
            return Digest(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 计算SM3摘要
        /// </summary>
        public byte[] Digest(Stream stream)
        {
            var digest = new SM3Digest();
            byte[] buffer = new byte[8192];
            int bytesRead;
            
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                digest.BlockUpdate(buffer, 0, bytesRead);
            }
            
            byte[] result = new byte[digest.GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }

        /// <summary>
        /// 计算SM3摘要并转为十六进制字符串
        /// </summary>
        public string DigestHex(Stream stream)
        {
            byte[] digest = Digest(stream);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }
        
        /// <summary>
        /// 添加盐值
        /// </summary>
        private byte[] AddSalt(byte[] data)
        {
            if (Salt == null || Salt.Length == 0)
            {
                return data;
            }
            
            byte[] result;
            if (SaltPosition <= 0)
            {
                // 盐值在前面
                result = new byte[Salt.Length + data.Length];
                Array.Copy(Salt, 0, result, 0, Salt.Length);
                Array.Copy(data, 0, result, Salt.Length, data.Length);
            }
            else if (SaltPosition >= data.Length)
            {
                // 盐值在后面
                result = new byte[data.Length + Salt.Length];
                Array.Copy(data, 0, result, 0, data.Length);
                Array.Copy(Salt, 0, result, data.Length, Salt.Length);
            }
            else
            {
                // 盐值在中间
                result = new byte[data.Length + Salt.Length];
                Array.Copy(data, 0, result, 0, SaltPosition);
                Array.Copy(Salt, 0, result, SaltPosition, Salt.Length);
                Array.Copy(data, SaltPosition, result, SaltPosition + Salt.Length, data.Length - SaltPosition);
            }
            
            return result;
        }
    }
}