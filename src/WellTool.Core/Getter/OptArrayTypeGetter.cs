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
/// 可选默认值的数组类型的Get接口
/// 提供一个统一的接口定义返回不同类型的值（基本类型）<br>
/// 如果值不存在或获取错误，返回默认值
/// </summary>
public interface OptArrayTypeGetter
{
    /*-------------------------- 数组类型 start -------------------------------*/

    /// <summary>
    /// 获取Object型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    object[] GetObjs(string key, object[] defaultValue);

    /// <summary>
    /// 获取String型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    string[] GetStrs(string key, string[] defaultValue);

    /// <summary>
    /// 获取Integer型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    int[] GetInts(string key, int[] defaultValue);

    /// <summary>
    /// 获取Short型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    short[] GetShorts(string key, short[] defaultValue);

    /// <summary>
    /// 获取Boolean型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    bool[] GetBools(string key, bool[] defaultValue);

    /// <summary>
    /// 获取Long型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    long[] GetLongs(string key, long[] defaultValue);

    /// <summary>
    /// 获取Character型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    char[] GetChars(string key, char[] defaultValue);

    /// <summary>
    /// 获取Double型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    double[] GetDoubles(string key, double[] defaultValue);

    /// <summary>
    /// 获取Byte型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    byte[] GetBytes(string key, byte[] defaultValue);

    /// <summary>
    /// 获取BigInteger型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    BigInteger[] GetBigIntegers(string key, BigInteger[] defaultValue);

    /// <summary>
    /// 获取BigDecimal型属性值数组
    /// </summary>
    /// <param name="key">属性名</param>
    /// <param name="defaultValue">默认数组值</param>
    /// <returns>属性值列表</returns>
    decimal[] GetBigDecimals(string key, decimal[] defaultValue);
    /*-------------------------- 数组类型 end -------------------------------*/
}
