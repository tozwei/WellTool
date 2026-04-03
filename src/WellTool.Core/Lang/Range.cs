using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Lang;

/// <summary>
/// 范围生成器。根据给定的初始值、结束值和步进生成一个步进列表生成器
/// </summary>
/// <typeparam name="T">生成范围对象的类型</typeparam>
public class Range<T> : IEnumerable<T>, IEnumerator<T>
{
	private readonly T _start;
	private readonly T _end;
	private T? _next;
	private readonly IStepper<T> _stepper;
	private int _index;
	private readonly bool _includeStart;
	private readonly bool _includeEnd;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="start">起始对象（包括）</param>
	/// <param name="stepper">步进</param>
	public Range(T start, IStepper<T> stepper)
		: this(start, default!, stepper, true, true)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="start">起始对象（包含）</param>
	/// <param name="end">结束对象（包含）</param>
	/// <param name="stepper">步进</param>
	public Range(T start, T end, IStepper<T> stepper)
		: this(start, end, stepper, true, true)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="start">起始对象</param>
	/// <param name="end">结束对象</param>
	/// <param name="stepper">步进</param>
	/// <param name="isIncludeStart">是否包含第一个元素</param>
	/// <param name="isIncludeEnd">是否包含最后一个元素</param>
	public Range(T start, T end, IStepper<T> stepper, bool isIncludeStart, bool isIncludeEnd)
	{
		_start = start!;
		_end = end!;
		_stepper = stepper;
		_next = SafeStep(_start);
		_includeStart = isIncludeStart;
		_includeEnd = isIncludeEnd;
		_index = 0;
	}

	/// <summary>
	/// 重置Range
	/// </summary>
	/// <returns>this</returns>
	public Range<T> Reset()
	{
		_index = 0;
		_next = SafeStep(_start);
		return this;
	}

	/// <summary>
	/// 步进接口，此接口用于实现如何对一个对象按照指定步进增加步进
	/// </summary>
	public interface IStepper<TR>
	{
		/// <summary>
		/// 增加步进
		/// </summary>
		/// <param name="current">上一次增加步进后的基础对象</param>
		/// <param name="end">结束对象</param>
		/// <param name="index">当前索引</param>
		/// <returns>增加步进后的对象</returns>
		TR Step(TR current, TR end, int index);
	}

	/// <summary>
	/// 获取当前元素
	/// </summary>
	public T Current
	{
		get
		{
			if (_index == 0)
				return _start;
			return _next!;
		}
	}

	object? IEnumerator.Current => Current;

	/// <summary>
	/// 移动到下一个元素
	/// </summary>
	public bool MoveNext()
	{
		if (_index == 0)
		{
			if (!_includeStart)
			{
				_index++;
				return MoveNext();
			}
			_index++;
			return true;
		}

		try
		{
			_next = SafeStep(_next!);
		}
		catch
		{
			return false;
		}

		if (!_includeEnd && EqualityComparer<T>.Default.Equals(_next, _end))
			return false;

		_index++;
		return true;
	}

	/// <summary>
	/// 不抛异常的获取下一步进的元素
	/// </summary>
	private T SafeStep(T baseObj)
	{
		try
		{
			return _stepper.Step(baseObj, _end, _index);
		}
		catch
		{
			return default!;
		}
	}

	void IEnumerator.Reset()
	{
		Reset();
	}

	void IDisposable.Dispose()
	{
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<T> GetEnumerator() => this;
}

/// <summary>
/// Range的静态工厂方法
/// </summary>
public static class Range
{
	/// <summary>
	/// 创建整数范围
	/// </summary>
	/// <param name="start">起始值（包含）</param>
	/// <param name="end">结束值（包含）</param>
	/// <param name="step">步进</param>
	/// <returns>Range</returns>
	public static Range<int> OfInt(int start, int end, int step = 1)
	{
		return new Range<int>(start, end, new IntStepper(step), true, true);
	}

	/// <summary>
	/// 创建整数范围
	/// </summary>
	/// <param name="start">起始值（包含）</param>
	/// <param name="end">结束值（包含）</param>
	/// <param name="includeEnd">是否包含结束值</param>
	/// <returns>Range</returns>
	public static Range<int> OfInt(int start, int end, bool includeEnd)
	{
		return new Range<int>(start, end, new IntStepper(1), true, includeEnd);
	}

	private class IntStepper : Range<int>.IStepper<int>
	{
		private readonly int _step;

		public IntStepper(int step)
		{
			_step = step;
		}

		int Range<int>.IStepper<int>.Step(int current, int end, int index)
		{
			var next = current + _step;
			if ((_step > 0 && next > end) || (_step < 0 && next < end))
				throw new InvalidOperationException("Step out of range");
			return next;
		}
	}
}
