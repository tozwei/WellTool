using System.Collections;
using System.Collections.Generic;

namespace WellTool.Json;

/// <summary>
/// 此类用于在JSONArray中便于遍历JSONObject而封装的IEnumerable，可以借助foreach语法遍历
/// </summary>
public class JSONObjectIter : IEnumerable<JSONObject>
{
    private readonly IEnumerator<object> _iterator;

    public JSONObjectIter(IEnumerator<object> iterator)
    {
        _iterator = iterator;
    }

    /// <summary>
    /// 获取枚举器
    /// </summary>
    /// <returns>枚举器</returns>
    public IEnumerator<JSONObject> GetEnumerator()
    {
        while (_iterator.MoveNext())
        {
            yield return (JSONObject)_iterator.Current;
        }
    }

    /// <summary>
    /// 获取枚举器
    /// </summary>
    /// <returns>枚举器</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
