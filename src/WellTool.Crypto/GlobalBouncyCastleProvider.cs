using System.Security.Cryptography;

namespace WellTool.Crypto
{
    public static class GlobalBouncyCastleProvider
    {
        static GlobalBouncyCastleProvider()
        {
            // 注册 Bouncy Castle 提供者
        }

        public static void Register()
        {
            // 静态构造函数已经注册，此方法仅用于显式调用
        }
    }
}