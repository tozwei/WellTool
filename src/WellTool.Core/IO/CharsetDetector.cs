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
    /// 编码探测器
    /// </summary>
    public class CharsetDetector
    {
        /// <summary>
        /// 默认的参与测试的编码
        /// </summary>
        private static readonly Encoding[] DefaultEncodings;

        static CharsetDetector()
        {
            string[] names = {
                "UTF-8",
                "GBK",
                "GB2312",
                "GB18030",
                "UTF-16BE",
                "UTF-16LE",
                "UTF-16",
                "BIG5",
                "UNICODE",
                "US-ASCII"
            };
            DefaultEncodings = new Encoding[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                try
                {
                    DefaultEncodings[i] = Encoding.GetEncoding(names[i]);
                }
                catch
                {
                    // 忽略无法获取的编码
                }
            }
            // 过滤掉null值
            DefaultEncodings = DefaultEncodings.Where(e => e != null).ToArray();
        }

        /// <summary>
        /// 探测文件编码
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="encodings">需要测试用的编码，null或空使用默认的编码数组</param>
        /// <returns>编码</returns>
        public static Encoding Detect(FileInfo file, params Encoding[] encodings)
        {
            using (var stream = file.OpenRead())
            {
                return Detect(stream, encodings);
            }
        }

        /// <summary>
        /// 探测编码<br>
        /// 注意：此方法会读取流的一部分，然后关闭流，如重复使用流，请使用支持reset方法的流
        /// </summary>
        /// <param name="stream">流，使用后关闭此流</param>
        /// <param name="encodings">需要测试用的编码，null或空使用默认的编码数组</param>
        /// <returns>编码</returns>
        public static Encoding Detect(Stream stream, params Encoding[] encodings)
        {
            return Detect(4096, stream, encodings);
        }

        /// <summary>
        /// 探测编码<br>
        /// 注意：此方法会读取流的一部分，然后关闭此流，如重复使用流，请使用支持reset方法的流
        /// </summary>
        /// <param name="bufferSize">自定义缓存大小，即每次检查的长度</param>
        /// <param name="stream">流，使用后关闭此流</param>
        /// <param name="encodings">需要测试用的编码，null或空使用默认的编码数组</param>
        /// <returns>编码</returns>
        public static Encoding Detect(int bufferSize, Stream stream, params Encoding[] encodings)
        {
            if (encodings == null || encodings.Length == 0)
            {
                encodings = DefaultEncodings;
            }

            var buffer = new byte[bufferSize];
            try
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 只使用读取到的部分
                    var actualBuffer = new byte[read];
                    Array.Copy(buffer, 0, actualBuffer, 0, read);

                    foreach (var encoding in encodings)
                    {
                        if (Identify(actualBuffer, encoding))
                        {
                            return encoding;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IORuntimeException(e);
            }
            finally
            {
                stream.Close();
            }
            return null;
        }

        /// <summary>
        /// 通过try的方式测试指定bytes是否可以被解码，从而判断是否为指定编码
        /// </summary>
        /// <param name="bytes">测试的bytes</param>
        /// <param name="encoding">编码</param>
        /// <returns>是否是指定编码</returns>
        private static bool Identify(byte[] bytes, Encoding encoding)
        {
            try
            {
                // 尝试解码
                var str = encoding.GetString(bytes);
                // 尝试重新编码，看是否与原字节数组相同
                var encodedBytes = encoding.GetBytes(str);
                return bytes.SequenceEqual(encodedBytes);
            }
            catch
            {
                return false;
            }
        }
    }
}