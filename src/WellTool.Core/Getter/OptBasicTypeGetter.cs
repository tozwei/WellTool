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
/// 可选默认值的基本类型的getter接口<br>
/// 提供一个统一的接口定义返回不同类型的值（基本类型）<br>
/// 如果值不存在或获取错误，返回默认值
/// </summary>
/// <typeparam name="K">键类型</typeparam>
public interface OptBasicTypeGetter<K>
{
    /*-------------------------- 基本类型 start -------------------------------*/

    /// <summary>
    /// 获取Object属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    object? GetObj(K key, object? defaultValue);

    /// <summary>
    /// 获取字符串型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    string? GetStr(K key, string? defaultValue);

    /// <summary>
    /// 获取int型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    int? GetInt(K key, int? defaultValue);

    /// <summary>
    /// 获取short型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    short? GetShort(K key, short? defaultValue);

    /// <summary>
    /// 获取boolean型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    bool? GetBool(K key, bool? defaultValue);

    /// <summary>
    /// 获取Long型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    long? GetLong(K key, long? defaultValue);

    /// <summary>
    /// 获取char型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    char? GetChar(K key, char? defaultValue);

    /// <summary>
    /// 获取float型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    float? GetFloat(K key, float? defaultValue);

    /// <summary>
    /// 获取double型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    double? GetDouble(K key, double? defaultValue);

    /// <summary>
    /// 获取byte型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    byte? GetByte(K key, byte? defaultValue);

    /// <summary>
    /// 获取BigDecimal型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    decimal? GetBigDecimal(K key, decimal? defaultValue);

    /// <summary>
    /// 获取BigInteger型属性值<br>
    /// 若获得的值为不可见字符，使用默认值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>属性值，无对应值返回defaultValue</returns>
    BigInteger? GetBigInteger(K key, BigInteger? defaultValue);

    /// <summary>
    /// 获得Enum类型的值
    /// </summary>
    /// <typeparam name="E">枚举类型</typeparam>
    /// <param name="clazz">Enum的Class</param>
    /// <param name="key">KEY</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>Enum类型的值，无则返回Null</returns>
    E? GetEnum<E>(Type clazz, K key, E? defaultValue) where E : Enum;

    /// <summary>
    /// 获取Date类型值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>Date类型属性值</returns>
    DateTime? GetDate(K key, DateTime? defaultValue);
    /*-------------------------- 基本类型 end -------------------------------*/
}
