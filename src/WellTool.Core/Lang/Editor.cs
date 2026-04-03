namespace WellTool.Core.Lang;

/// <summary>
/// 编辑器接口，用于转换对象
/// </summary>
/// <typeparam name="T">被编辑的对象类型</typeparam>
public interface Editor<T>
{
    /// <summary>
    /// 编辑方法
    /// </summary>
    /// <param name="t">要编辑的对象</param>
    /// <returns>编辑后的对象</returns>
    T Edit(T t);
}

/// <summary>
/// 编辑器接口（无返回值）
/// </summary>
/// <typeparam name="T">被编辑的对象类型</typeparam>
public interface VoidEditor<T>
{
    /// <summary>
    /// 编辑方法
    /// </summary>
    /// <param name="t">要编辑的对象</param>
    void Edit(T t);
}

/// <summary>
/// 链式编辑器
/// </summary>
public class ChainEditor<T> : Editor<T>
{
    private readonly System.Collections.Generic.List<Editor<T>> _editors = new System.Collections.Generic.List<Editor<T>>();

    /// <summary>
    /// 添加编辑器
    /// </summary>
    public ChainEditor<T> Add(Editor<T> editor)
    {
        _editors.Add(editor);
        return this;
    }

    /// <summary>
    /// 执行编辑
    /// </summary>
    public T Edit(T t)
    {
        foreach (var editor in _editors)
        {
            t = editor.Edit(t);
        }
        return t;
    }
}
