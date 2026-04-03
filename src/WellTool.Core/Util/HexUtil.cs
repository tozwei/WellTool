// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 十六进制工具类
    /// </summary>
    public static class HexUtil
    {
        /// <summary>
        /// 十六进制字符数组
        /// </summary>
        private static readonly char[] HexChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string BytesToHexString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                sb.Append(HexChars[(b >> 4) & 0x0F]);
                sb.Append(HexChars[b & 0x0F]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将十六进制字符串转换为字节数组
        /// </summary>
        /// <param name="hexString">十六进制字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] DecodeHex(string hexString)
        {
            if (string.IsNullOrEmpty(hexString))
            {
                return Array.Empty<byte>();
            }

            int length = hexString.Length;
            if (length % 2 != 0)
            {
                hexString = "0" + hexString;
                length++;
            }

            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>十六进制字符串</returns>
        public static string ToHex(byte[] bytes)
        {
            return BytesToHexString(bytes);
        }
    }
}