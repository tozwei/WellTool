using System.Diagnostics.CodeAnalysis;

namespace WellTool.Core.Lang;

/// <summary>
/// 复制自 JDK 16 中的 Optional，并进行了调整和扩展
/// </summary>
/// <typeparam name="T">包裹里元素的类型</typeparam>
public class Opt<T>
{
	private static readonly Opt<T?> EmptyInstance = new();
	private readonly T? _value;
	private System.Exception? _exception;

	/// <summary>
	/// 返回一个空的Opt
	/// </summary>
	/// <returns>Opt</returns>
	public static Opt<T?> Empty() => EmptyInstance;

	/// <summary>
	/// 返回一个空的Opt
	/// </summary>
	/// <returns>Opt</returns>
	public static Opt<U?> Empty<U>() => new Opt<U?>();

	/// <summary>
	/// 返回一个包裹里元素不可能为空的Opt
	/// </summary>
	/// <param name="value">包裹里的元素</param>
	/// <returns>Opt</returns>
	public static Opt<T> Of(T value) => new(RequireNonNull(value));

	/// <summary>
	/// 返回一个包裹里元素可能为空的Opt
	/// </summary>
	/// <param name="value">传入需要包裹的元素</param>
	/// <returns>Opt</returns>
	public static Opt<T?> OfNullable(T? value) => value == null ? Empty() : new Opt<T?>(value);

	private static T RequireNonNull(T value)
	{
		if (value == null) throw new System.ArgumentNullException(nameof(value));
		return value;
	}

	/// <summary>
	/// Opt无参构造函数
	/// </summary>
	internal Opt()
	{
		_value = default;
	}

	/// <summary>
	/// Opt构造函数
	/// </summary>
	/// <param name="value">包裹里的元素</param>
	internal Opt(T? value)
	{
		_value = value;
	}

	/// <summary>
	/// 设置异常
	/// </summary>
	/// <param name="exception">异常</param>
	internal void SetException(System.Exception exception)
	{
		_exception = exception;
	}

	/// <summary>
	/// 返回包裹里的元素，取不到则为null
	/// </summary>
	public T? Get() => _value;

	/// <summary>
	/// 判断包裹里元素的值是否不存在
	/// </summary>
	public bool IsEmpty => _value == null;

	/// <summary>
	/// 判断包裹里元素的值是否存在
	/// </summary>
	public bool IsPresent => _value != null;

	/// <summary>
	/// 获取异常（当调用OfTry时）
	/// </summary>
	public System.Exception? Exception => _exception;

	/// <summary>
	/// 是否失败
	/// </summary>
	public bool IsFail => _exception != null;

	/// <summary>
	/// 如果包裹里的值存在，就执行传入的操作
	/// </summary>
	/// <param name="action">你想要执行的操作</param>
	/// <returns>this</returns>
	public Opt<T> IfPresent(System.Action<T> action)
	{
		if (IsPresent)
			action(_value!);
		return this;
	}

	/// <summary>
	/// 如果包裹里的值存在，就执行传入的值存在时的操作，否则执行传入的值不存在时的操作
	/// </summary>
	/// <param name="action">值存在时的操作</param>
	/// <param name="emptyAction">值不存在时的操作</param>
	/// <returns>this</returns>
	public Opt<T> IfPresentOrElse(System.Action<T> action, System.Action emptyAction)
	{
		if (IsPresent)
			action(_value!);
		else
			emptyAction();
		return this;
	}

	/// <summary>
	/// 如果包裹里的值存在并且与给定的条件满足，则返回本身，否则返回空Opt
	/// </summary>
	/// <param name="predicate">给定的条件</param>
	/// <returns>Opt</returns>
	public Opt<T> Filter(System.Predicate<T> predicate)
	{
		if (IsEmpty)
			return this;
		return predicate(_value!) ? this : Empty();
	}

	/// <summary>
	/// 如果包裹里的值存在，就执行传入的操作并返回一个包裹了该操作返回值的Opt，否则返回空Opt
	/// </summary>
	/// <typeparam name="U">操作返回值的类型</typeparam>
	/// <param name="mapper">值存在时执行的操作</param>
	/// <returns>Opt</returns>
	public Opt<U> Map<U>(System.Func<T, U> mapper)
	{
		if (IsEmpty)
			return Empty<U>();
		return Opt<U>.OfNullable(mapper(_value!));
	}

	/// <summary>
	/// 如果包裹里元素的值存在，就返回本身，如果不存在，则使用传入的操作执行后获得的Opt
	/// </summary>
	/// <param name="supplier">不存在时的操作</param>
	/// <returns>Opt</returns>
	public Opt<T> Or(System.Func<Opt<T>> supplier)
	{
		if (IsPresent)
			return this;
		return supplier();
	}

	/// <summary>
	/// 如果包裹里元素的值存在，则返回该值，否则返回传入的other
	/// </summary>
	/// <param name="other">元素为空时返回的值</param>
	/// <returns>值</returns>
	public T? OrElse(T? other) => IsPresent ? _value : other;

	/// <summary>
	/// 异常则返回另一个可选值
	/// </summary>
	/// <param name="other">可选值</param>
	/// <returns>值</returns>
	public T? ExceptionOrElse(T? other) => IsFail ? other : _value;

	/// <summary>
	/// 如果包裹里元素的值存在，则返回该值，否则返回传入的操作执行后的返回值
	/// </summary>
	/// <param name="supplier">值不存在时需要执行的操作</param>
	/// <returns>值</returns>
	public T? OrElseGet(System.Func<T?> supplier) => IsPresent ? _value : supplier();

	/// <summary>
	/// 如果包裹里的值存在，则返回该值，否则抛出NoSuchElementException
	/// </summary>
	/// <returns>值</returns>
	public T GetOrThrow() => IsPresent ? _value! : throw new InvalidOperationException("No value present");

	/// <summary>
	/// 如果包裹里的值存在，则返回该值，否则执行传入的操作，获取异常类型的返回值并抛出
	/// </summary>
	/// <typeparam name="X">异常类型</typeparam>
	/// <param name="exceptionSupplier">值不存在时执行的操作</param>
	/// <returns>值</returns>
	public T GetOrThrow<X>(System.Func<X> exceptionSupplier) where X : System.Exception
	{
		if (IsPresent)
			return _value!;
		throw exceptionSupplier();
	}

	/// <summary>
	/// 判断传入参数是否与Opt相等
	/// </summary>
	/// <param name="obj">要判断是否相等的参数</param>
	/// <returns>是否相等</returns>
	public override bool Equals(object? obj)
	{
		if (this == obj)
			return true;
		if (obj is not Opt<T>)
			return false;
		var other = (Opt<T>)obj;
		return Equals(_value, other._value);
	}

	/// <summary>
	/// 获取哈希码
	/// </summary>
	public override int GetHashCode() => _value?.GetHashCode() ?? 0;

	/// <summary>
	/// 返回包裹内元素调用toString()的结果
	/// </summary>
	public override string? ToString() => IsPresent ? _value?.ToString() : "null";
}

/// <summary>
/// Opt静态扩展方法
/// </summary>
public static class Opt
{
	/// <summary>
	/// 返回一个包裹里元素可能为空的Opt，额外判断了空字符串的情况
	/// </summary>
	public static Opt<T?> OfBlankAble<T>(T? value)
	{
		if (value == null)
			return Opt<T?>.Empty();
		if (value is string str && string.IsNullOrWhiteSpace(str))
			return Opt<T?>.Empty();
		return new Opt<T?>(value);
	}

	/// <summary>
	/// 尝试执行操作并返回结果
	/// </summary>
	public static Opt<T> OfTry<T>(System.Func<T> supplier)
	{
		try
		{
			return Opt<T>.OfNullable(supplier());
		}
		catch (System.Exception e)	
		{
			var opt = new Opt<T>(default);
			opt.SetException(e);
			return opt;
		}
	}

	/// <summary>
	/// 如果包裹内容失败了，则执行传入的操作
	/// </summary>
	public static Opt<T> IfFail<T>(this Opt<T> opt, System.Action<System.Exception> action)
	{
		if (opt.IsFail && opt.Exception != null)
			action(opt.Exception);
		return opt;
	}

	/// <summary>
	/// 如果包裹里元素的值存在，就执行对应的操作，并返回本身
	/// </summary>
	public static Opt<T> Peek<T>(this Opt<T> opt, System.Action<T> action)
	{
		if (opt.IsPresent)
			action(opt.Get()!);
		return opt;
	}

	/// <summary>
	/// 如果包裹里元素的值存在，则返回该值的枚举，否则返回空枚举
	/// </summary>
	public static IEnumerable<T> ToEnumerable<T>(this Opt<T> opt)
	{
		if (opt.IsPresent)
			yield return opt.Get()!;
	}
}
