using System;

namespace WellTool.Core.Bean.Copier;

/// <summary>
/// DynaBean值提供者
/// </summary>
public class DynaBeanValueProvider : IValueProvider<string>
{
    private readonly DynaBean _dynaBean;
    private readonly bool _ignoreError;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dynaBean">DynaBean</param>
    /// <param name="ignoreError">是否忽略错误</param>
    public DynaBeanValueProvider(DynaBean dynaBean, bool ignoreError)
    {
        _dynaBean = dynaBean;
        _ignoreError = ignoreError;
    }

    public object Value(string key, Type valueType)
        {
            try
            {
                var value = _dynaBean.Get<object>(key);
                return System.Convert.ChangeType(value, valueType);
            }
            catch
            {
                if (_ignoreError)
                {
                    return null;
                }
                throw;
            }
        }

    public bool ContainsKey(string key)
    {
        return _dynaBean.ContainsProp(key);
    }
}
