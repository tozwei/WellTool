namespace WellTool.Core.Bean.Copier;

/// <summary>
/// DynaBean值提供者
/// </summary>
public class DynaBeanValueProvider : ValueProvider<string>
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

    public object? Value(string key, Type valueType)
    {
        var value = _dynaBean.Get(key);
        return ConvertUtil.ConvertWithCheck(valueType, value, null, _ignoreError);
    }

    public bool ContainsKey(string key)
    {
        return _dynaBean.ContainsProp(key);
    }
}
