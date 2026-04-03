namespace WellTool.Core.Lang;

/// <summary>
/// 编辑器接口
/// </summary>
/// <typeparam name="T">编辑对象类型</typeparam>
public interface IEditor<T>
{
	/// <summary>
	/// 编辑方法
	/// </summary>
	/// <param name="t">待编辑对象</param>
	/// <returns>编辑后的对象</returns>
	T Edit(T t);
}

/// <summary>
/// 编辑器抽象类
/// </summary>
/// <typeparam name="T">编辑对象类型</typeparam>
public abstract class Editor<T> : IEditor<T>
{
	/// <summary>
	/// 编辑方法
	/// </summary>
	/// <param name="t">待编辑对象</param>
	/// <returns>编辑后的对象</returns>
	public abstract T Edit(T t);
}

/// <summary>
/// Lambda编辑器
/// </summary>
/// <typeparam name="T">编辑对象类型</typeparam>
public class LambdaEditor<T> : Editor<T>
{
	private readonly Func<T, T> _editor;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="editor">编辑函数</param>
	public LambdaEditor(Func<T, T> editor)
	{
		_editor = editor;
	}

	/// <summary>
	/// 编辑方法
	/// </summary>
	/// <param name="t">待编辑对象</param>
	/// <returns>编辑后的对象</returns>
	public override T Edit(T t)
	{
		return _editor?.Invoke(t) ?? t;
	}
}
