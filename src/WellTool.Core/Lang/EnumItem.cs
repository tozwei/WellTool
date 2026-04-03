namespace WellTool.Core.Lang;

/// <summary>
/// 枚举元素通用接口，在自定义枚举上实现此接口可以用于数据转换
/// 数据库保存时建议保存 IntVal() 而非 Ordinal() 防备需求变更
/// </summary>
/// <typeparam name="E">Enum类型</typeparam>
public interface IEnumItem<E> where E : class, IEnumItem<E>
{
	/// <summary>
	/// 获取枚举名称
	/// </summary>
	string Name { get; }

	/// <summary>
	/// 在中文语境下，多数时间枚举会配合一个中文说明
	/// </summary>
	string Text { get; }

	/// <summary>
	/// 获取int值
	/// </summary>
	int IntVal { get; }
}
