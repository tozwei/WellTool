using System.Collections;
using System.Text.RegularExpressions;
using WellTool.Core.Collection;
using WellTool.Core.Convert;
using WellTool.Core.Map;
using WellTool.Core.Util;

namespace WellTool.Core.Bean;

/// <summary>
/// Bean路径表达式，用于获取多层嵌套Bean中的字段值或Bean对象
/// 表达式分为两种：
/// 1. .表达式，可以获取Bean对象中的属性（字段）值或者Map中key对应的值
/// 2. []表达式，可以获取集合等对象中对应index的值
/// </summary>
public class BeanPath
{
    private static readonly char[] ExpChars = { '.', '[', ']' };

    private bool _isStartWith;
    private List<string> _patternParts = new();

    /// <summary>
    /// 解析Bean路径表达式为BeanPath
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>BeanPath</returns>
    public static BeanPath Create(string expression)
    {
        return new BeanPath(expression);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="expression">表达式</param>
    public BeanPath(string expression)
    {
        Init(expression);
    }

    /// <summary>
    /// 获取表达式解析后的分段列表
    /// </summary>
    public List<string> PatternParts => _patternParts;

    /// <summary>
    /// 获取Bean中对应表达式的值
    /// </summary>
    /// <param name="bean">Bean对象或Map或List等</param>
    /// <returns>值，如果对应值不存在，则返回null</returns>
    public object? Get(object bean)
    {
        return Get(_patternParts, bean, false);
    }

    /// <summary>
    /// 设置表达式指定位置的值
    /// </summary>
    /// <param name="bean">Bean、Map或List</param>
    /// <param name="value">值</param>
    public void Set(object bean, object value)
    {
        Set(bean, _patternParts, LastIsNumber(_patternParts), value);
    }

    public override string ToString() => string.Join(".", _patternParts);

    private object? Get(List<string> patternParts, object bean, bool ignoreLast)
    {
        int length = patternParts.Count;
        if (ignoreLast)
        {
            length--;
        }
        object? subBean = bean;
        bool isFirst = true;
        for (int i = 0; i < length; i++)
        {
            string patternPart = patternParts[i];

            // 支持通配符 * 语法
            if (patternPart == "*")
            {
                if (i < length - 1 && (subBean is IEnumerable && subBean is not string))
                {
                    return GetByWildcard(subBean, patternParts, i + 1, length);
                }
                return subBean;
            }

            subBean = GetFieldValue(subBean, patternPart);
            if (subBean == null)
            {
                if (isFirst && !_isStartWith && BeanUtil.IsMatchName(bean, patternPart, true))
                {
                    subBean = bean;
                    isFirst = false;
                }
                else
                {
                    return null;
                }
            }
        }
        return subBean;
    }

    private object? GetByWildcard(object collectionOrArray, List<string> patternParts, int startIndex, int endIndex)
    {
        var results = new List<object>();

        var remainingParts = patternParts.Skip(startIndex).Take(endIndex - startIndex).ToList();

        if (collectionOrArray is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item != null)
                {
                    var value = Get(remainingParts, item, false);
                    if (value != null)
                    {
                        results.Add(value);
                    }
                }
            }
        }

        return results.Count > 0 ? results : null;
    }

    private static bool LastIsNumber(List<string> patternParts)
    {
        var last = patternParts[patternParts.Count - 1];
        return int.TryParse(last, out _);
    }

    private static object? GetFieldValue(object? bean, string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return null;
        }

        if (expression.Contains(':'))
        {
            // [start:end:step] 模式
            var parts = expression.Split(':');
            var start = int.Parse(parts[0]);
            var end = int.Parse(parts[1]);
            var step = parts.Length == 3 ? int.Parse(parts[2]) : 1;

            if (bean is IList list)
            {
                return WellTool.Core.Collection.CollUtil.Sub(list, start, end, step);
            }
            if (bean is Array array)
            {
                return ArrayUtil.Sub((object[])array, start, end, step);
            }
        }
        else if (expression.Contains(','))
        {
            // [num0,num1,num2...]模式
            var keys = expression.Split(',');
            if (bean is IList list)
            {
                return WellTool.Core.Collection.CollUtil.GetAny(list, WellTool.Core.Convert.Convert.ToIntArray(keys));
            }
            if (bean is IDictionary dictionary)
            {
                var results = new List<object?>();
                foreach (var key in keys)
                {
                    results.Add(dictionary[key]);
                }
                return results;
            }
        }
        else
        {
            // 数字或普通字符串
            if (int.TryParse(expression, out var index) && bean is IList list)
            {
                if (index >= 0 && index < list.Count)
                {
                    return list[index];
                }
            }
            if (bean is IDictionary dict)
            {
                return dict[expression];
            }
            return BeanUtil.GetFieldValue(bean, expression);
        }

        return null;
    }

    private void Set(object bean, List<string> patternParts, bool nextNumberPart, object value)
    {
        object? subBean = Get(patternParts, bean, true);
        if (subBean == null)
        {
            var parentParts = patternParts.Take(patternParts.Count - 1).ToList();
            object newValue = nextNumberPart ? (object)new List<object>() : (object)new Dictionary<string, object>();
            Set(bean, parentParts, lastIsNumber(parentParts), newValue);
            subBean = Get(patternParts, bean, true);
        }

        if (subBean != null)
        {
            BeanUtil.SetFieldValue(subBean, patternParts[^1], value);
        }
    }

    private static bool lastIsNumber(List<string> patternParts)
    {
        return int.TryParse(patternParts[^1], out _);
    }

    private void Init(string expression)
    {
        var localPatternParts = new List<string>();
        int length = expression.Length;

        var builder = new System.Text.StringBuilder();
        bool isNumStart = false;
        bool isInWrap = false;

        for (int i = 0; i < length; i++)
        {
            char c = expression[i];

            if (i == 0 && c == '$')
            {
                _isStartWith = true;
                continue;
            }

            if (c == '\'')
            {
                isInWrap = !isInWrap;
                continue;
            }

            if (!isInWrap && ExpChars.Contains(c))
            {
                if (c == ']')
                {
                    if (!isNumStart)
                    {
                        throw new ArgumentException($"Bad expression '{expression}':{i}, we find ']' but no '[' !");
                    }
                    isNumStart = false;
                }
                else
                {
                    if (isNumStart)
                    {
                        throw new ArgumentException($"Bad expression '{expression}':{i}, we find '[' but no ']' !");
                    }
                    if (c == '[')
                    {
                        isNumStart = true;
                    }
                }

                if (builder.Length > 0)
                {
                    localPatternParts.Add(builder.ToString());
                }
                builder.Clear();
            }
            else
            {
                builder.Append(c);
            }
        }

        if (isNumStart)
        {
            throw new ArgumentException($"Bad expression '{expression}':{length - 1}, we find '[' but no ']' !");
        }
        else
        {
            if (builder.Length > 0)
            {
                localPatternParts.Add(builder.ToString());
            }
        }

        _patternParts = localPatternParts.AsReadOnly().ToList();
    }
}
