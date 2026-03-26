using System;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// 值提供者，用于提供Bean注入时参数对应值得抽象接口<br>
	/// 继承或匿名实例化此接口<br>
	/// 在Bean注入过程中，Bean获得字段名，通过外部方式根据这个字段名查找相应的字段值，然后注入Bean<br>
	/// </summary>
	/// <typeparam name="T">KEY类型，一般情况下为 {@link String}</typeparam>
	/// <author>Looly</author>
	public interface IValueProvider<T>
	{
		/// <summary>
		/// 获取值<br>
		/// 返回值一般需要匹配被注入类型，如果不匹配会调用默认转换实现转换
		/// </summary>
		/// <param name="key">Bean对象中参数名</param>
		/// <param name="valueType">被注入的值的类型</param>
		/// <returns>对应参数名的值</returns>
		object Value(T key, Type valueType);

		/// <summary>
		/// 是否包含指定KEY，如果不包含则忽略注入<br>
		/// 此接口方法单独需要实现的意义在于：有些值提供者（比如Map）key是存在的，但是value为null，此时如果需要注入这个null，需要根据此方法判断
		/// </summary>
		/// <param name="key">Bean对象中参数名</param>
		/// <returns>是否包含指定KEY</returns>
		bool ContainsKey(T key);
	}
}