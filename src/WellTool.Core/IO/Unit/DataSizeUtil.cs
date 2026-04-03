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
    /// 数据大小工具类
    /// </summary>
    public class DataSizeUtil
    {
        /// <summary>
        /// 解析数据大小字符串，转换为bytes大小
        /// </summary>
        /// <param name="text">数据大小字符串，类似于：12KB, 5MB等</param>
        /// <returns>bytes大小</returns>
        public static long Parse(string text)
        {
            return DataSize.Parse(text).ToBytes();
        }

        /// <summary>
        /// 可读的文件大小<br>
        /// 参考 http://stackoverflow.com/questions/3263892/format-file-size-as-mb-gb-etc
        /// </summary>
        /// <param name="size">Long类型大小</param>
        /// <returns>大小</returns>
        public static string Format(long size)
        {
            if (size <= 0)
            {
                return "0";
            }
            var digitGroups = System.Math.Min(DataUnit.UnitNames.Length - 1, (int)(System.Math.Log10(size) / System.Math.Log10(1024)));
            return $"{System.Math.Round(size / System.Math.Pow(1024, digitGroups), 2):#,##0.##} {DataUnit.UnitNames[digitGroups]}";
        }

        /// <summary>
        /// 根据单位，将文件大小转换为对应单位的大小
        /// </summary>
        /// <param name="size">文件大小</param>
        /// <param name="fileDataUnit">单位</param>
        /// <returns>大小</returns>
        public static string Format(long size, DataUnit fileDataUnit)
        {
            if (size <= 0)
            {
                return "0";
            }
            var digitGroups = Array.IndexOf(DataUnit.UnitNames, fileDataUnit.GetSuffix());
            return $"{System.Math.Round(size / System.Math.Pow(1024, digitGroups), 2):##0.##} {DataUnit.UnitNames[digitGroups]}";
        }
    }
}