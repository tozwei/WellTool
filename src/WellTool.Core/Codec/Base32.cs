using System;
using System.Text;

namespace WellTool.Core.Codec
{
    /// <summary>
    /// Base32 - encodes and decodes RFC4648 Base32
    /// base32就是用32（2的5次方）个特定ASCII码来表示256个ASCII码。
    /// 所以，5个ASCII字符经过base32编码后会变为8个字符（公约数为40），长度增加3/5.不足8n用"="补足。
    /// </summary>
    public static class Base32
    {
        //----------------------------------------------------------------------------------------- encode

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <returns>base32</returns>
        public static string Encode(byte[] bytes)
        {
            return Base32Codec.INSTANCE.Encode(bytes);
        }

        /// <summary>
        /// base32编码
        /// </summary>
        /// <param name="source">被编码的base32字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(string source)
        {
            return Encode(source, Encoding.UTF8);
        }

        /// <summary>
        /// base32编码
        /// </summary>
        /// <param name="source">被编码的base32字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string Encode(string source, Encoding encoding)
        {
            return Encode(encoding.GetBytes(source));
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="bytes">数据（Hex模式）</param>
        /// <returns>base32</returns>
        public static string EncodeHex(byte[] bytes)
        {
            return Base32Codec.INSTANCE.Encode(bytes, true);
        }

        /// <summary>
        /// base32编码（Hex模式）
        /// </summary>
        /// <param name="source">被编码的base32字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeHex(string source)
        {
            return EncodeHex(source, Encoding.UTF8);
        }

        /// <summary>
        /// base32编码（Hex模式）
        /// </summary>
        /// <param name="source">被编码的base32字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string EncodeHex(string source, Encoding encoding)
        {
            return EncodeHex(encoding.GetBytes(source));
        }

        //----------------------------------------------------------------------------------------- decode

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="base32">base32编码</param>
        /// <returns>数据</returns>
        public static byte[] Decode(string base32)
        {
            return Base32Codec.INSTANCE.Decode(base32);
        }

        /// <summary>
        /// base32解码
        /// </summary>
        /// <param name="source">被解码的base32字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStr(string source)
        {
            return DecodeStr(source, Encoding.UTF8);
        }

        /// <summary>
        /// base32解码
        /// </summary>
        /// <param name="source">被解码的base32字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStr(string source, Encoding encoding)
        {
            return encoding.GetString(Decode(source));
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="base32">base32编码</param>
        /// <returns>数据</returns>
        public static byte[] DecodeHex(string base32)
        {
            return Base32Codec.INSTANCE.Decode(base32, true);
        }

        /// <summary>
        /// base32解码
        /// </summary>
        /// <param name="source">被解码的base32字符串</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStrHex(string source)
        {
            return DecodeStrHex(source, Encoding.UTF8);
        }

        /// <summary>
        /// base32解码
        /// </summary>
        /// <param name="source">被解码的base32字符串</param>
        /// <param name="encoding">字符集</param>
        /// <returns>被加密后的字符串</returns>
        public static string DecodeStrHex(string source, Encoding encoding)
        {
            return encoding.GetString(DecodeHex(source));
        }
    }
}