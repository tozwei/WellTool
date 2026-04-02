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

using System.Text.RegularExpressions;

namespace WellTool.Core.IO.Unit
{
    /// <summary>
    /// 数据大小，可以将类似于'12MB'表示转换为bytes长度的数字
    /// <p>
    /// 此类来自于：Spring-framework
    /// <pre>
    ///     byte        1B     1
    ///     kilobyte    1KB    1,024
    ///     megabyte    1MB    1,048,576
    ///     gigabyte    1GB    1,073,741,824
    ///     terabyte    1TB    1,099,511,627,776
    /// </pre>
    /// </summary>
    public class DataSize : IComparable<DataSize>
    {
        /// <summary>
        /// The pattern for parsing.
        /// </summary>
        private static readonly Regex Pattern = new Regex("^([+-]?\\d+(\\.\\d+)?)([a-zA-Z]{0,3})$");

        /// <summary>
        /// Bytes per Kilobyte(KB).
        /// </summary>
        private static readonly long BytesPerKb = 1024;

        /// <summary>
        /// Bytes per Megabyte(MB).
        /// </summary>
        private static readonly long BytesPerMb = BytesPerKb * 1024;

        /// <summary>
        /// Bytes per Gigabyte(GB).
        /// </summary>
        private static readonly long BytesPerGb = BytesPerMb * 1024;

        /// <summary>
        /// Bytes per Terabyte(TB).
        /// </summary>
        private static readonly long BytesPerTb = BytesPerGb * 1024;

        /// <summary>
        /// bytes长度
        /// </summary>
        private readonly long _bytes;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bytes">长度</param>
        private DataSize(long bytes)
        {
            _bytes = bytes;
        }

        /// <summary>
        /// 获得对应bytes的DataSize
        /// </summary>
        /// <param name="bytes">bytes大小，可正可负</param>
        /// <returns>DataSize</returns>
        public static DataSize OfBytes(long bytes)
        {
            return new DataSize(bytes);
        }

        /// <summary>
        /// 获得对应kilobytes的DataSize
        /// </summary>
        /// <param name="kilobytes">kilobytes大小，可正可负</param>
        /// <returns>DataSize</returns>
        public static DataSize OfKilobytes(long kilobytes)
        {
            return new DataSize(kilobytes * BytesPerKb);
        }

        /// <summary>
        /// 获得对应megabytes的DataSize
        /// </summary>
        /// <param name="megabytes">megabytes大小，可正可负</param>
        /// <returns>DataSize</returns>
        public static DataSize OfMegabytes(long megabytes)
        {
            return new DataSize(megabytes * BytesPerMb);
        }

        /// <summary>
        /// 获得对应gigabytes的DataSize
        /// </summary>
        /// <param name="gigabytes">gigabytes大小，可正可负</param>
        /// <returns>DataSize</returns>
        public static DataSize OfGigabytes(long gigabytes)
        {
            return new DataSize(gigabytes * BytesPerGb);
        }

        /// <summary>
        /// 获得对应terabytes的DataSize
        /// </summary>
        /// <param name="terabytes">terabytes大小，可正可负</param>
        /// <returns>DataSize</returns>
        public static DataSize OfTerabytes(long terabytes)
        {
            return new DataSize(terabytes * BytesPerTb);
        }

        /// <summary>
        /// 获得指定{@link DataUnit}对应的DataSize
        /// </summary>
        /// <param name="amount">大小</param>
        /// <param name="unit">数据大小单位，null表示默认的BYTES</param>
        /// <returns>DataSize</returns>
        public static DataSize Of(long amount, DataUnit unit)
        {
            if (unit == null)
            {
                unit = DataUnit.Bytes;
            }
            return new DataSize(amount * unit.Size().ToBytes());
        }

        /// <summary>
        /// 获得指定{@link DataUnit}对应的DataSize
        /// </summary>
        /// <param name="amount">大小</param>
        /// <param name="unit">数据大小单位，null表示默认的BYTES</param>
        /// <returns>DataSize</returns>
        public static DataSize Of(decimal amount, DataUnit unit)
        {
            if (unit == null)
            {
                unit = DataUnit.Bytes;
            }
            return new DataSize((long)(amount * unit.Size().ToBytes()));
        }

        /// <summary>
        /// 获取指定数据大小文本对应的DataSize对象，如果无单位指定，默认获取{@link DataUnit#Bytes}
        /// <p>
        /// 例如：
        /// <pre>
        /// "12KB" -- parses as "12 kilobytes"
        /// "5MB"  -- parses as "5 megabytes"
        /// "20"   -- parses as "20 bytes"
        /// </pre>
        /// </summary>
        /// <param name="text">the text to parse</param>
        /// <returns>the parsed DataSize</returns>
        public static DataSize Parse(string text)
        {
            return Parse(text, null);
        }

        /// <summary>
        /// Obtain a DataSize from a text string such as {@code 12MB} using
        /// the specified default {@link DataUnit} if no unit is specified.
        /// <p>
        /// The string starts with a number followed optionally by a unit matching one of the
        /// supported {@linkplain DataUnit suffixes}.
        /// <p>
        /// Examples:
        /// <pre>
        /// "12KB" -- parses as "12 kilobytes"
        /// "5MB"  -- parses as "5 megabytes"
        /// "20"   -- parses as "20 kilobytes" (where the {@code defaultUnit} is {@link DataUnit#Kilobytes})
        /// </pre>
        /// </summary>
        /// <param name="text">the text to parse</param>
        /// <param name="defaultUnit">默认的数据单位</param>
        /// <returns>the parsed DataSize</returns>
        public static DataSize Parse(string text, DataUnit defaultUnit)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text", "Text must not be null");
            }
            try
            {
                var matcher = Pattern.Match(text.Trim());
                if (!matcher.Success)
                {
                    throw new ArgumentException("Does not match data size pattern");
                }

                var unit = DetermineDataUnit(matcher.Groups[3].Value, defaultUnit);
                return Of(decimal.Parse(matcher.Groups[1].Value), unit);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"'{text}' is not a valid data size", ex);
            }
        }

        /// <summary>
        /// 决定数据单位，后缀不识别时使用默认单位
        /// </summary>
        /// <param name="suffix">后缀</param>
        /// <param name="defaultUnit">默认单位</param>
        /// <returns>{@link DataUnit}</returns>
        private static DataUnit DetermineDataUnit(string suffix, DataUnit defaultUnit)
        {
            var defaultUnitToUse = defaultUnit ?? DataUnit.Bytes;
            return !string.IsNullOrEmpty(suffix) ? DataUnit.FromSuffix(suffix) : defaultUnitToUse;
        }

        /// <summary>
        /// 是否为负数，不包括0
        /// </summary>
        /// <returns>负数返回true，否则false</returns>
        public bool IsNegative()
        {
            return _bytes < 0;
        }

        /// <summary>
        /// 返回bytes大小
        /// </summary>
        /// <returns>bytes大小</returns>
        public long ToBytes()
        {
            return _bytes;
        }

        /// <summary>
        /// 返回KB大小
        /// </summary>
        /// <returns>KB大小</returns>
        public long ToKilobytes()
        {
            return _bytes / BytesPerKb;
        }

        /// <summary>
        /// 返回MB大小
        /// </summary>
        /// <returns>MB大小</returns>
        public long ToMegabytes()
        {
            return _bytes / BytesPerMb;
        }

        /// <summary>
        /// 返回GB大小
        /// </summary>
        /// <returns>GB大小</returns>
        public long ToGigabytes()
        {
            return _bytes / BytesPerGb;
        }

        /// <summary>
        /// 返回TB大小
        /// </summary>
        /// <returns>TB大小</returns>
        public long ToTerabytes()
        {
            return _bytes / BytesPerTb;
        }

        /// <summary>
        /// 比较两个DataSize实例的大小
        /// </summary>
        /// <param name="other">另一个DataSize实例</param>
        /// <returns>比较结果</returns>
        public int CompareTo(DataSize other)
        {
            return _bytes.CompareTo(other._bytes);
        }

        /// <summary>
        /// 返回字符串表示
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"{_bytes}B";
        }

        /// <summary>
        /// 比较两个DataSize实例是否相等
        /// </summary>
        /// <param name="obj">另一个对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var otherSize = (DataSize)obj;
            return _bytes == otherSize._bytes;
        }

        /// <summary>
        /// 获取哈希码
        /// </summary>
        /// <returns>哈希码</returns>
        public override int GetHashCode()
        {
            return _bytes.GetHashCode();
        }
    }
}