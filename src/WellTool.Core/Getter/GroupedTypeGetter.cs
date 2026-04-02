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

using System.Numerics;

namespace WellTool.Core.Getter;

/// <summary>
/// 基于分组的Get接口
/// </summary>
public interface GroupedTypeGetter
{
    /*-------------------------- 基本类型 start -------------------------------*/
    /// <summary>
    /// 获取字符串型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    string? GetStrByGroup(string key, string group);

    /// <summary>
    /// 获取int型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    int? GetIntByGroup(string key, string group);

    /// <summary>
    /// 获取short型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    short? GetShortByGroup(string key, string group);

    /// <summary>
    /// 获取boolean型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    bool? GetBoolByGroup(string key, string group);

    /// <summary>
    /// 获取Long型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    long? GetLongByGroup(string key, string group);

    /// <summary>
    /// 获取char型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    char? GetCharByGroup(string key, string group);

    /// <summary>
    /// 获取double型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    double? GetDoubleByGroup(string key, string group);

    /// <summary>
    /// 获取byte型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    byte? GetByteByGroup(string key, string group);

    /// <summary>
    /// 获取BigDecimal型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    decimal? GetBigDecimalByGroup(string key, string group);

    /// <summary>
    /// 获取BigInteger型属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="group">分组</param>
    /// <returns>属性值</returns>
    BigInteger? GetBigIntegerByGroup(string key, string group);
    /*-------------------------- 基本类型 end -------------------------------*/
}
