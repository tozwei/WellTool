using System;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// 抽象的对象拷贝封装，提供来源对象、目标对象持有
	/// </summary>
	/// <typeparam name="S">来源对象类型</typeparam>
	/// <typeparam name="T">目标对象类型</typeparam>
	/// <author>looly</author>
	/// <since>5.8.0</since>
	public abstract class AbsCopier<S, T> : ICopier<T>
	{
		protected readonly S Source;
		protected readonly T Target;
		/// <summary>
		/// 拷贝选项
		/// </summary>
		protected readonly CopyOptions CopyOptions;

		public AbsCopier(S source, T target, CopyOptions copyOptions)
		{
			Source = source;
			Target = target;
			CopyOptions = copyOptions ?? CopyOptions.Create();
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public abstract T Copy();
	}

	/// <summary>
	/// 拷贝接口
	/// </summary>
	/// <typeparam name="T">目标对象类型</typeparam>
	public interface ICopier<T>
	{
		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		T Copy();
	}
}