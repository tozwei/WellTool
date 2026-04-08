using System;
using System.IO;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 字节工具类
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        /// 将short转换为字节数组
        /// </summary>
        public static byte[] ToBytes(short value)
        {
            return new byte[]
            {
                (byte)(value >> 8),
                (byte)value
            };
        }

        /// <summary>
        /// 将short转换为字节数组（高位在前）
        /// </summary>
        public static byte[] ToBytesHighFirst(short value)
        {
            return ToBytes(value);
        }

        /// <summary>
        /// 将short转换为字节数组（低位在前）
        /// </summary>
        public static byte[] ToBytesLowFirst(short value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8)
            };
        }

        /// <summary>
        /// 将int转换为字节数组
        /// </summary>
        public static byte[] ToBytes(int value)
        {
            return new byte[]
            {
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
        }

        /// <summary>
        /// 将int转换为字节数组（高位在前）
        /// </summary>
        public static byte[] ToBytesHighFirst(int value)
        {
            return ToBytes(value);
        }

        /// <summary>
        /// 将int转换为字节数组（低位在前）
        /// </summary>
        public static byte[] ToBytesLowFirst(int value)
        {
            return new byte[]
            {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
        }

        /// <summary>
        /// 将long转换为字节数组
        /// </summary>
        public static byte[] ToBytes(long value)
        {
            return new byte[]
            {
                (byte)(value >> 56),
                (byte)(value >> 48),
                (byte)(value >> 40),
                (byte)(value >> 32),
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value
            };
        }

        /// <summary>
        /// 将字节数组转换为short
        /// </summary>
        public static short ToShort(byte[] bytes)
        {
            return ToShort(bytes, 0);
        }

        /// <summary>
        /// 将字节数组转换为short
        /// </summary>
        public static short ToShort(byte[] bytes, int offset)
        {
            if (bytes == null || bytes.Length < offset + 2)
            {
                throw new ArgumentException("Byte array too short");
            }
            return (short)((bytes[offset] << 8) | bytes[offset + 1]);
        }

        /// <summary>
        /// 将字节数组转换为int
        /// </summary>
        public static int ToInt(byte[] bytes)
        {
            return ToInt(bytes, 0);
        }

        /// <summary>
        /// 将字节数组转换为int
        /// </summary>
        public static int ToInt(byte[] bytes, int offset)
        {
            if (bytes == null || bytes.Length < offset + 4)
            {
                throw new ArgumentException("Byte array too short");
            }
            return (bytes[offset] << 24) | (bytes[offset + 1] << 16) |
                   (bytes[offset + 2] << 8) | bytes[offset + 3];
        }

        /// <summary>
        /// 将字节数组转换为long
        /// </summary>
        public static long ToLong(byte[] bytes)
        {
            return ToLong(bytes, 0);
        }

        /// <summary>
        /// 将字节数组转换为long
        /// </summary>
        public static long ToLong(byte[] bytes, int offset)
        {
            if (bytes == null || bytes.Length < offset + 8)
            {
                throw new ArgumentException("Byte array too short");
            }
            long value = 0;
            for (int i = 0; i < 8; i++)
            {
                value = (value << 8) | bytes[offset + i];
            }
            return value;
        }

        /// <summary>
        /// 将字节数组转换为无符号int
        /// </summary>
        public static uint ToUInt(byte[] bytes)
        {
            return (uint)ToInt(bytes);
        }

        /// <summary>
        /// 将字节数组转换为无符号long
        /// </summary>
        public static ulong ToULong(byte[] bytes)
        {
            return (ulong)ToLong(bytes);
        }

        /// <summary>
        /// 将float转换为字节数组
        /// </summary>
        public static byte[] ToBytes(float value)
        {
            return ToBytes(BitConverter.SingleToInt32Bits(value));
        }

        /// <summary>
        /// 将double转换为字节数组
        /// </summary>
        public static byte[] ToBytes(double value)
        {
            return ToBytes(BitConverter.DoubleToInt64Bits(value));
        }

        /// <summary>
        /// 将字节数组转换为float
        /// </summary>
        public static float ToFloat(byte[] bytes)
        {
            return BitConverter.Int32BitsToSingle(ToInt(bytes));
        }

        /// <summary>
        /// 将字节数组转换为double
        /// </summary>
        public static double ToDouble(byte[] bytes)
        {
            return BitConverter.Int64BitsToDouble(ToLong(bytes));
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        public static string ToHexString(byte[] bytes)
        {
            return ToHexString(bytes, "");
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        public static string ToHexString(byte[] bytes, string separator)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return "";
            }

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                if (i > 0 && !string.IsNullOrEmpty(separator))
                {
                    sb.Append(separator);
                }
                sb.Append(bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        public static byte[] FromHexString(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return Array.Empty<byte>();
            }

            hex = hex.Replace(" ", "").Replace("-", "");
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have even length");
            }

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = System.Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
    }
}
