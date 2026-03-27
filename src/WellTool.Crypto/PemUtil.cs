using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto
{
    /// <summary>
    /// PEM工具类
    /// </summary>
    public static class PemUtil
    {
        /// <summary>
        /// 从PEM字符串中读取对象
        /// </summary>
        /// <param name="pemStr">PEM字符串</param>
        /// <returns>对象</returns>
        public static object ReadPem(string pemStr)
        {
            using var reader = new StringReader(pemStr);
            var pemReader = new PemReader(reader);
            return pemReader.ReadObject();
        }

        /// <summary>
        /// 从PEM文件中读取对象
        /// </summary>
        /// <param name="pemPath">PEM文件路径</param>
        /// <returns>对象</returns>
        public static object ReadPemFile(string pemPath)
        {
            var pemStr = File.ReadAllText(pemPath, Encoding.UTF8);
            return ReadPem(pemStr);
        }

        /// <summary>
        /// 将对象写入PEM字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>PEM字符串</returns>
        public static string WritePem(object obj)
        {
            using var writer = new StringWriter();
            var pemWriter = new PemWriter(writer);
            pemWriter.WriteObject(obj);
            pemWriter.Writer.Flush();
            return writer.ToString();
        }

        /// <summary>
        /// 将对象写入PEM文件
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="pemPath">PEM文件路径</param>
        public static void WritePemFile(object obj, string pemPath)
        {
            var pemStr = WritePem(obj);
            File.WriteAllText(pemPath, pemStr, Encoding.UTF8);
        }
    }
}