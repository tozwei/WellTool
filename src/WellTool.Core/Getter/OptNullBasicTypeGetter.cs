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
/// 基本类型的getter接口抽象实现<br>
/// 提供一个统一的接口定义返回不同类型的值（基本类型）<br>
/// 在不提供默认值的情况下， 如果值不存在或获取错误，返回null<br>
/// 用户只需实现{@link OptBasicTypeGetter}接口即可
/// </summary>
/// <typeparam name="K">键类型</typeparam>
public interface OptNullBasicTypeGetter<K> : OptBasicTypeGetter<K>
{
    /// <summary>
    /// 获取Object属性值
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    object? GetObj(K key);

    /// <summary>
    /// 获取字符串型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    string? GetStr(K key);

    /// <summary>
    /// 获取int型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    int? GetInt(K key);

    /// <summary>
    /// 获取short型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    short? GetShort(K key);

    /// <summary>
    /// 获取boolean型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    bool? GetBool(K key);

    /// <summary>
    /// 获取long型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    long? GetLong(K key);

    /// <summary>
    /// 获取char型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    char? GetChar(K key);

    /// <summary>
    /// 获取float型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    float? GetFloat(K key);

    /// <summary>
    /// 获取double型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    double? GetDouble(K key);

    /// <summary>
    /// 获取byte型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    byte? GetByte(K key);

    /// <summary>
    /// 获取BigDecimal型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    decimal? GetBigDecimal(K key);

    /// <summary>
    /// 获取BigInteger型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    BigInteger? GetBigInteger(K key);

    /// <summary>
    /// 获取Enum型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <typeparam name="E">枚举类型</typeparam>
    /// <param name="clazz">Enum 的 Class</param>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    E? GetEnum<E>(Type clazz, K key) where E : Enum;

    /// <summary>
    /// 获取Date型属性值<br>
    /// 无值或获取错误返回null
    /// </summary>
    /// <param name="key">属性名</param>
    /// <returns>属性值</returns>
    DateTime? GetDate(K key);
}
