using System;
using System.Text;

namespace WellTool.Core.Net
{
    /// <summary>
    /// 密码认证信息
    /// </summary>
    public class PassAuth
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PassAuth()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PassAuth(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        /// <summary>
        /// 从Base64编码的认证字符串创建
        /// </summary>
        /// <param name="base64Auth">Base64编码的认证字符串</param>
        public static PassAuth FromBase64(string base64Auth)
        {
            if (string.IsNullOrEmpty(base64Auth))
            {
                return new PassAuth();
            }

            try
            {
                var decoded = Encoding.UTF8.GetString(System.Convert.FromBase64String(base64Auth));
                var parts = decoded.Split(':');
                if (parts.Length >= 2)
                {
                    return new PassAuth(parts[0], parts[1]);
                }
            }
            catch
            {
                // 解析失败，返回空认证
            }

            return new PassAuth();
        }

        /// <summary>
        /// 转换为Base64编码的认证字符串
        /// </summary>
        /// <returns>Base64编码的认证字符串</returns>
        public string ToBase64()
        {
            var raw = $"{UserName}:{Password}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
        }

        /// <summary>
        /// 获取HTTP Basic认证头
        /// </summary>
        /// <returns>认证头值</returns>
        public string GetBasicAuthHeader()
        {
            return $"Basic {ToBase64()}";
        }
    }
}
