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

namespace WellTool.Core.IO.Unit
{
    /// <summary>
    /// 数据单位封装<p>
    /// 此类来自于：Spring-framework
    /// <pre>
    ///     BYTES      1B      2^0     1
    ///     KILOBYTES  1KB     2^10    1,024
    ///     MEGABYTES  1MB     2^20    1,048,576
    ///     GIGABYTES  1GB     2^30    1,073,741,824
    ///     TERABYTES  1TB     2^40    1,099,511,627,776
    /// </pre>
    /// </summary>
    public enum DataUnit
    {
        /// <summary>
        /// Bytes, 后缀表示为： {@code B}.
        /// </summary>
        Bytes("B", DataSize.OfBytes(1)),

        /// <summary>
        /// Kilobytes, 后缀表示为： {@code KB}.
        /// </summary>
        Kilobytes("KB", DataSize.OfKilobytes(1)),

        /// <summary>
        /// Megabytes, 后缀表示为： {@code MB}.
        /// </summary>
        Megabytes("MB", DataSize.OfMegabytes(1)),

        /// <summary>
        /// Gigabytes, 后缀表示为： {@code GB}.
        /// </summary>
        Gigabytes("GB", DataSize.OfGigabytes(1)),

        /// <summary>
        /// Terabytes, 后缀表示为： {@code TB}.
        /// </summary>
        Terabytes("TB", DataSize.OfTerabytes(1));

        /// <summary>
        /// 单位后缀
        /// </summary>
        public static readonly string[] UnitNames = new string[] { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

        private readonly string _suffix;
        private readonly DataSize _size;

        DataUnit(string suffix, DataSize size)
        {
            _suffix = suffix;
            _size = size;
        }

        /// <summary>
        /// 单位后缀
        /// </summary>
        /// <returns>单位后缀</returns>
        public string GetSuffix()
        {
            return _suffix;
        }

        internal DataSize Size()
        {
            return _size;
        }

        /// <summary>
        /// 通过后缀返回对应的 DataUnit
        /// </summary>
        /// <param name="suffix">单位后缀，如KB、GB、GiB等</param>
        /// <returns>匹配到的{@code DataUnit}</returns>
        /// <exception cref="ArgumentException">后缀无法识别报错</exception>
        public static DataUnit FromSuffix(string suffix)
        {
            // 兼容KiB、MiB、GiB
            if (suffix.Length == 3 && char.ToLower(suffix[1]) == 'i')
            {
                suffix = $"{suffix[0]}{suffix[2]}";
            }

            foreach (var candidate in Enum.GetValues(typeof(DataUnit)))
            {
                var dataUnit = (DataUnit)candidate;
                // 支持类似于 3MB，3M，3m等写法
                if (dataUnit._suffix.StartsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    return dataUnit;
                }
            }
            throw new ArgumentException($"Unknown data unit suffix '{suffix}'");
        }
    }
}