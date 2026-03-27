using System;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto
{
    /// <summary>
    /// BouncyCastle工具类
    /// </summary>
    public static class BCUtil
    {
        /// <summary>
        /// 创建安全随机数生成器
        /// </summary>
        /// <returns>安全随机数生成器</returns>
        public static SecureRandom CreateSecureRandom()
        {
            return new SecureRandom();
        }
    }
}