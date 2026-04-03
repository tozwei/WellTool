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

using System.Collections.Generic;
using System.IO;
using WellTool.Core.Util;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 文件类型判断工具类
    /// <p>此工具根据文件的前几位bytes猜测文件类型，对于文本、zip判断不准确，对于视频、图片类型判断准确</p>
    /// <p>需要注意的是，xlsx、docx等Office2007格式，全部识别为zip，因为新版采用了OpenXML格式，这些格式本质上是XML文件打包为zip</p>
    /// </summary>
    public class FileTypeUtil
    {
        private static readonly SortedDictionary<string, string> FileTypeMap = new SortedDictionary<string, string>();

        /// <summary>
        /// 增加文件类型映射<br>
        /// 如果已经存在将覆盖之前的映射
        /// </summary>
        /// <param name="fileStreamHexHead">文件流头部Hex信息</param>
        /// <param name="extName">文件扩展名</param>
        /// <returns>之前已经存在的文件扩展名</returns>
        public static string PutFileType(string fileStreamHexHead, string extName)
        {
            if (FileTypeMap.TryGetValue(fileStreamHexHead, out var oldExtName))
            {
                FileTypeMap[fileStreamHexHead] = extName;
                return oldExtName;
            }
            FileTypeMap.Add(fileStreamHexHead, extName);
            return null;
        }

        /// <summary>
        /// 移除文件类型映射
        /// </summary>
        /// <param name="fileStreamHexHead">文件流头部Hex信息</param>
        /// <returns>移除的文件扩展名</returns>
        public static string RemoveFileType(string fileStreamHexHead)
        {
            if (FileTypeMap.TryGetValue(fileStreamHexHead, out var extName))
            {
                FileTypeMap.Remove(fileStreamHexHead);
                return extName;
            }
            return null;
        }

        /// <summary>
        /// 根据文件流的头部信息获得文件类型
        /// </summary>
        /// <param name="fileStreamHexHead">文件流头部16进制字符串</param>
        /// <returns>文件类型，未找到为{@code null}</returns>
        public static string GetType(string fileStreamHexHead)
        {
            if (string.IsNullOrEmpty(fileStreamHexHead))
            {
                return null;
            }
            if (FileTypeMap.Count > 0)
            {
                foreach (var entry in FileTypeMap)
                {
                    if (fileStreamHexHead.StartsWith(entry.Key, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return entry.Value;
                    }
                }
            }
            var bytes = HexUtil.DecodeHex(fileStreamHexHead);
            return FileMagicNumber.GetMagicNumber(bytes).Extension;
        }

        /// <summary>
        /// 根据文件流的头部信息获得文件类型<br>
        /// 注意此方法会读取头部64个bytes，造成此流接下来读取时缺少部分bytes<br>
        /// 因此如果想复用此流，流需支持{@link Stream#Seek(long, SeekOrigin)}方法。
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="isExact">是否精确匹配，如果为false，使用前64个bytes匹配，如果为true，使用前8192bytes匹配</param>
        /// <returns>类型，文件的扩展名，提供的stream为{@code null}或未找到为{@code null}</returns>
        /// <exception cref="IORuntimeException">读取流引起的异常</exception>
        public static string GetType(Stream stream, bool isExact = false)
        {
            if (stream == null)
            {
                return null;
            }
            var hexHead = isExact ? WellTool.Core.Util.IOUtil.ReadHex8192Upper(stream) : WellTool.Core.Util.IOUtil.ReadHex64Upper(stream);
            return GetType(hexHead);
        }

        /// <summary>
        /// 根据文件流的头部信息获得文件类型
        /// 注意此方法会读取头部64个bytes，造成此流接下来读取时缺少部分bytes<br>
        /// 因此如果想复用此流，流需支持{@link Stream#Seek(long, SeekOrigin)}方法。
        /// <pre>
        ///     1、无法识别类型默认按照扩展名识别
        ///     2、xls、doc、msi头信息无法区分，按照扩展名区分
        ///     3、zip可能为docx、xlsx、pptx、jar、war、ofd头信息无法区分，按照扩展名区分
        /// </pre>
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="filename">文件名</param>
        /// <param name="isExact">是否精确匹配，如果为false，使用前64个bytes匹配，如果为true，使用前8192bytes匹配</param>
        /// <returns>类型，文件的扩展名，未找到为{@code null}</returns>
        /// <exception cref="IORuntimeException">读取流引起的异常</exception>
        public static string GetType(Stream stream, string filename, bool isExact = false)
        {
            var typeName = GetType(stream, isExact);
            if (typeName == null)
            {
                // 未成功识别类型，扩展名辅助识别
                typeName = FileUtil.ExtName(filename);
            }
            else if (typeName == "zip")
            {
                // zip可能为docx、xlsx、pptx、jar、war、ofd等格式，扩展名辅助判断
                var extName = FileUtil.ExtName(filename);
                if (string.Equals(extName, "docx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "docx";
                }
                else if (string.Equals(extName, "xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "xlsx";
                }
                else if (string.Equals(extName, "pptx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "pptx";
                }
                else if (string.Equals(extName, "jar", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "jar";
                }
                else if (string.Equals(extName, "war", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "war";
                }
                else if (string.Equals(extName, "ofd", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "ofd";
                }
                else if (string.Equals(extName, "apk", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "apk";
                }
            }
            else if (typeName == "jar")
            {
                // wps编辑过的.xlsx文件与.jar的开头相同,通过扩展名判断
                var extName = FileUtil.ExtName(filename);
                if (string.Equals(extName, "xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "xlsx";
                }
                else if (string.Equals(extName, "docx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "docx";
                }
                else if (string.Equals(extName, "pptx", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "pptx";
                }
                else if (string.Equals(extName, "zip", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "zip";
                }
                else if (string.Equals(extName, "apk", StringComparison.OrdinalIgnoreCase))
                {
                    typeName = "apk";
                }
            }
            return typeName;
        }

        /// <summary>
        /// 根据文件流的头部信息获得文件类型
        /// <pre>
        ///     1、无法识别类型默认按照扩展名识别
        ///     2、xls、doc、msi头信息无法区分，按照扩展名区分
        ///     3、zip可能为jar、war头信息无法区分，按照扩展名区分
        /// </pre>
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="isExact">是否精确匹配，如果为false，使用前64个bytes匹配，如果为true，使用前8192bytes匹配</param>
        /// <returns>类型，文件的扩展名，未找到为{@code null}</returns>
        /// <exception cref="IORuntimeException">读取文件引起的异常</exception>
        public static string GetType(FileInfo file, bool isExact = false)
        {
            if (!file.Exists || file.Attributes.HasFlag(FileAttributes.Directory))
            {
                throw new ArgumentException("Not a regular file!");
            }
            using (var stream = file.OpenRead())
            {
                return GetType(stream, file.Name, isExact);
            }
        }

        /// <summary>
        /// 通过路径获得文件类型
        /// </summary>
        /// <param name="path">路径，绝对路径或相对路径</param>
        /// <param name="isExact">是否精确匹配，如果为false，使用前64个bytes匹配，如果为true，使用前8192bytes匹配</param>
        /// <returns>类型</returns>
        /// <exception cref="IORuntimeException">读取文件引起的异常</exception>
        public static string GetTypeByPath(string path, bool isExact = false)
        {
            var file = new FileInfo(path);
            return GetType(file, isExact);
        }
    }
}