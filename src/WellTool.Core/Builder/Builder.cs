using System;

namespace WellTool.Core.Builder
{
	/// <summary>
	/// 建造者模式接口定义
	/// </summary>
	/// <typeparam name="T">建造对象类型</typeparam>
	/// <author>Looly</author>
	/// <since>4.2.2</since>
	public interface Builder<T>
	{
		/// <summary>
		/// 构建
		/// </summary>
		/// <returns>被构建的对象</returns>
		T Build();
	}
}