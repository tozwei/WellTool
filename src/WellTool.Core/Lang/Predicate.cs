using System;

namespace WellTool.Core.Lang
{
	/// <summary>
	/// Predicate接口
	/// </summary>
	public interface IPredicate<T>
	{
		bool Test(T t);
	}

	/// <summary>
	/// Predicate实现
	/// </summary>
	public class PredicateImpl<T> : IPredicate<T>
	{
		private readonly System.Func<T, bool> _func;

		public PredicateImpl(System.Func<T, bool> func)
		{
			_func = func;
		}

		public bool Test(T t)
		{
			return _func(t);
		}

		/// <summary>
		/// 与运算
		/// </summary>
		public PredicateImpl<T> And(PredicateImpl<T> other)
		{
			return new PredicateImpl<T>(new System.Func<T, bool>(t => Test(t) && other.Test(t)));
		}

		/// <summary>
		/// 或运算
		/// </summary>
		public PredicateImpl<T> Or(PredicateImpl<T> other)
		{
			return new PredicateImpl<T>(new System.Func<T, bool>(t => Test(t) || other.Test(t)));
		}

		/// <summary>
		/// 取反
		/// </summary>
		public PredicateImpl<T> Negate()
		{
			return new PredicateImpl<T>(new System.Func<T, bool>(t => !Test(t)));
		}
	}
}
