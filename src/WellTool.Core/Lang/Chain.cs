namespace WellTool.Core.Lang;

/// <summary>
/// 责任链接口
/// </summary>
/// <typeparam name="E">元素类型</typeparam>
/// <typeparam name="T">目标类类型，用于返回this对象</typeparam>
public interface IChain<E, T> : IEnumerable<E>
{
	/// <summary>
	/// 加入责任链
	/// </summary>
	/// <param name="element">责任链新的环节元素</param>
	/// <returns>this</returns>
	T AddChain(E element);
}
