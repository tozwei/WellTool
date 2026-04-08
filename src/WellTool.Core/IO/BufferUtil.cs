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

using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 缓冲区工具类<br>
    /// 此工具来自于 t-io 项目以及其它项目的相关部分收集
    /// </summary>
    public class BufferUtil
    {
        /// <summary>
        /// 拷贝到一个新的字节数组
        /// </summary>
        /// <param name="src">源字节数组</param>
        /// <param name="start">起始位置（包括）</param>
        /// <param name="end">结束位置（不包括）</param>
        /// <returns>新的字节数组</returns>
        public static byte[] Copy(byte[] src, int start, int end)
        {
            var length = end - start;
            var dest = new byte[length];
            Array.Copy(src, start, dest, 0, length);
            return dest;
        }

        /// <summary>
        /// 拷贝字节数组
        /// </summary>
        /// <param name="src">源字节数组</param>
        /// <param name="dest">目标字节数组</param>
        /// <returns>目标字节数组</returns>
        public static byte[] Copy(byte[] src, byte[] dest)
        {
            var length = System.Math.Min(src.Length, dest.Length);
            return Copy(src, 0, dest, 0, length);
        }

        /// <summary>
        /// 拷贝字节数组
        /// </summary>
        /// <param name="src">源字节数组</param>
        /// <param name="dest">目标字节数组</param>
        /// <param name="length">长度</param>
        /// <returns>目标字节数组</returns>
        public static byte[] Copy(byte[] src, byte[] dest, int length)
        {
            return Copy(src, 0, dest, 0, length);
        }

        /// <summary>
        /// 拷贝字节数组
        /// </summary>
        /// <param name="src">源字节数组</param>
        /// <param name="srcStart">源开始的位置</param>
        /// <param name="dest">目标字节数组</param>
        /// <param name="destStart">目标开始的位置</param>
        /// <param name="length">长度</param>
        /// <returns>目标字节数组</returns>
        public static byte[] Copy(byte[] src, int srcStart, byte[] dest, int destStart, int length)
        {
            Array.Copy(src, srcStart, dest, destStart, length);
            return dest;
        }

        /// <summary>
        /// 读取剩余部分并转为UTF-8编码字符串
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <returns>字符串</returns>
        public static string ReadUtf8Str(byte[] buffer, ref int position)
        {
            return ReadStr(buffer, ref position, Encoding.UTF8);
        }

        /// <summary>
        /// 读取剩余部分并转为字符串
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static string ReadStr(byte[] buffer, ref int position, Encoding encoding)
        {
            var bytes = ReadBytes(buffer, ref position);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 读取剩余部分bytes<br>
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <returns>bytes</returns>
        public static byte[] ReadBytes(byte[] buffer, ref int position)
        {
            if (position >= buffer.Length)
            {
                return Array.Empty<byte>();
            }
            var ab = new byte[1];
            Array.Copy(buffer, position, ab, 0, 1);
            position += 1;
            return ab;
        }

        /// <summary>
        /// 读取指定长度的bytes<br>
        /// 如果长度不足，则读取剩余部分
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>bytes</returns>
        public static byte[] ReadBytes(byte[] buffer, ref int position, int maxLength)
        {
            var remaining = buffer.Length - position;
            if (maxLength > remaining)
            {
                maxLength = remaining;
            }
            var ab = new byte[maxLength];
            Array.Copy(buffer, position, ab, 0, maxLength);
            position += maxLength;
            return ab;
        }

        /// <summary>
        /// 读取指定区间的数据
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>bytes</returns>
        public static byte[] ReadBytes(byte[] buffer, int start, int end)
        {
            var bs = new byte[end - start];
            Array.Copy(buffer, start, bs, 0, bs.Length);
            return bs;
        }

        /// <summary>
        /// 一行的末尾位置，查找位置时位移position到结束位置
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <returns>末尾位置，未找到返回-1</returns>
        public static int LineEnd(byte[] buffer, ref int position)
        {
            return LineEnd(buffer, ref position, buffer.Length - position);
        }

        /// <summary>
        /// 一行的末尾位置，查找位置时位移position到结束位置<br>
        /// 支持的换行符如下：
        /// <pre>
        /// 1. \r\n
        /// 2. \n
        /// </pre>
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <param name="maxLength">读取最大长度</param>
        /// <returns>末尾位置，未找到或达到最大长度返回-1</returns>
        public static int LineEnd(byte[] buffer, ref int position, int maxLength)
        {
            int primitivePosition = position;
            bool canEnd = false;
            int charIndex = primitivePosition;
            byte b;
            while (position < buffer.Length)
            {
                b = buffer[position++];
                charIndex++;
                if (b == '\r')
                {
                    canEnd = true;
                }
                else if (b == '\n')
                {
                    return canEnd ? charIndex - 2 : charIndex - 1;
                }
                else
                {
                    // 只有\r无法确认换行
                    canEnd = false;
                }

                if (charIndex - primitivePosition > maxLength)
                {
                    // 查找到尽头，未找到，还原位置
                    position = primitivePosition;
                    throw new IndexOutOfRangeException($"Position is out of maxLength: {maxLength}");
                }
            }

            // 查找到buffer尽头，未找到，还原位置
            position = primitivePosition;
            // 读到结束位置
            return -1;
        }

        /// <summary>
        /// 读取一行，如果buffer中最后一部分并非完整一行，则返回null<br>
        /// 支持的换行符如下：
        /// <pre>
        /// 1. \r\n
        /// 2. \n
        /// </pre>
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="position">当前位置</param>
        /// <param name="encoding">编码</param>
        /// <returns>一行</returns>
        public static string ReadLine(byte[] buffer, ref int position, Encoding encoding)
        {
            int startPosition = position;
            int endPosition = LineEnd(buffer, ref position);

            if (endPosition > startPosition)
            {
                byte[] bs = ReadBytes(buffer, startPosition, endPosition);
                return encoding.GetString(bs);
            }
            else if (endPosition == startPosition)
            {
                return string.Empty;
            }

            return null;
        }

        /// <summary>
        /// 创建新字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>字节数组</returns>
        public static byte[] Create(byte[] data)
        {
            return (byte[])data.Clone();
        }

        /// <summary>
        /// 从字符串创建新字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">编码</param>
        /// <returns>字节数组</returns>
        public static byte[] Create(string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        /// <summary>
        /// 从字符串创建新字节数组，使用UTF-8编码
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>字节数组</returns>
        public static byte[] CreateUtf8(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}