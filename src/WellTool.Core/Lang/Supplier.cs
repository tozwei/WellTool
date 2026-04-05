using System;

namespace WellTool.Core.Lang
{
	/// <summary>
	/// Supplier接口
	/// </summary>
	public interface ISupplier<T>
	{
		T Get();
	}

	/// <summary>
	/// Supplier实现
	/// </summary>
	public class Supplier<T> : ISupplier<T>
	{
		private readonly System.Func<T> _func;

		public Supplier(System.Func<T> func)
		{
			_func = func;
		}

		public T Get()
		{
			return _func.Invoke();
		}
	}
}
