using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WellTool.Core.Net
{
    /// <summary>
    /// SSL 工具类
    /// </summary>
    public class SSLUtil
    {
        /// <summary>
        /// 创建一个接受所有证书的证书验证回调
        /// </summary>
        /// <returns>证书验证回调</returns>
        public static RemoteCertificateValidationCallback CreateAcceptAllCertificatesCallback()
        {
            return (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// 加载证书
        /// </summary>
        /// <param name="certPath">证书路径</param>
        /// <param name="password">证书密码</param>
        /// <returns>X509 证书</returns>
        public static X509Certificate2 LoadCertificate(string certPath, string password)
        {
            return new X509Certificate2(certPath, password);
        }

        /// <summary>
        /// 从字节数组加载证书
        /// </summary>
        /// <param name="certBytes">证书字节数组</param>
        /// <param name="password">证书密码</param>
        /// <returns>X509 证书</returns>
        public static X509Certificate2 LoadCertificate(byte[] certBytes, string password)
        {
            return new X509Certificate2(certBytes, password);
        }

        /// <summary>
        /// 验证证书是否有效
        /// </summary>
        /// <param name="certificate">证书</param>
        /// <returns>是否有效</returns>
        public static bool IsCertificateValid(X509Certificate2 certificate)
        {
            if (certificate == null)
            {
                return false;
            }
            return certificate.NotBefore <= DateTime.Now && certificate.NotAfter >= DateTime.Now;
        }

        /// <summary>
        /// 获取证书的主题名称
        /// </summary>
        /// <param name="certificate">证书</param>
        /// <returns>主题名称</returns>
        public static string GetCertificateSubject(X509Certificate2 certificate)
        {
            return certificate?.Subject ?? string.Empty;
        }

        /// <summary>
        /// 获取证书的颁发者名称
        /// </summary>
        /// <param name="certificate">证书</param>
        /// <returns>颁发者名称</returns>
        public static string GetCertificateIssuer(X509Certificate2 certificate)
        {
            return certificate?.Issuer ?? string.Empty;
        }

        /// <summary>
        /// 获取证书的有效期开始时间
        /// </summary>
        /// <param name="certificate">证书</param>
        /// <returns>有效期开始时间</returns>
        public static DateTime GetCertificateNotBefore(X509Certificate2 certificate)
        {
            return certificate?.NotBefore ?? DateTime.MinValue;
        }

        /// <summary>
        /// 获取证书的有效期结束时间
        /// </summary>
        /// <param name="certificate">证书</param>
        /// <returns>有效期结束时间</returns>
        public static DateTime GetCertificateNotAfter(X509Certificate2 certificate)
        {
            return certificate?.NotAfter ?? DateTime.MaxValue;
        }
    }
}