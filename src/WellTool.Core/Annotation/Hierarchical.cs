using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// <p>描述以一个参照物为对象，存在于该参照物的层级结构中的对象。
	/// 
	/// <p>该对象可通过{@link #getVerticalDistance()}与{@link #getHorizontalDistance()}
	/// 描述其在以参照物为基点的坐标坐标系中的位置。<br>
	/// 在需要对该接口的实现类进行按优先级排序时，距离{@link #getRoot()}对象越近，则该实现类的优先级越高。
	/// 默认提供了{@link #DEFAULT_HIERARCHICAL_COMPARATOR}用于实现该比较规则。<br>
	/// 一般情况下，{@link #getRoot()}返回值相同的对象之间的比较才有意义。
	/// 
	/// <p>此外，还提供了{@link Selector}接口用于根据一定的规则从两个{@link Hierarchical}实现类中选择并返回一个最合适的对象，
	/// 默认提供了四个实现类：
	/// <ul>
	///     <li>{@link Selector#NEAREST_AND_OLDEST_PRIORITY}: 返回距离根对象更近的对象，当距离一样时优先返回旧对象；</li>
	///     <li>{@link Selector#NEAREST_AND_NEWEST_PRIORITY}: 返回距离根对象更近的对象，当距离一样时优先返回新对象；</li>
	///     <li>{@link Selector#FARTHEST_AND_OLDEST_PRIORITY}: 返回距离根对象更远的对象，当距离一样时优先返回旧对象；</li>
	///     <li>{@link Selector#FARTHEST_AND_NEWEST_PRIORITY}: 返回距离根对象更远的对象，当距离一样时优先返回新对象；</li>
	/// </ul>
	/// </summary>
	/// <author>huangchengxing</author>
	public interface Hierarchical : IComparable<Hierarchical>
	{
		// ====================== compare  ======================

		/// <summary>
		/// 默认{@link #getHorizontalDistance()}与{@link #getVerticalDistance()}排序的比较器
		/// </summary>
		static readonly Comparator<Hierarchical> DEFAULT_HIERARCHICAL_COMPARATOR = new Comparator<Hierarchical>(
			(x, y) => {
				int result = x.GetVerticalDistance().CompareTo(y.GetVerticalDistance());
				if (result == 0)
				{
					result = x.GetHorizontalDistance().CompareTo(y.GetHorizontalDistance());
				}
				return result;
			}
		);

		/// <summary>
		/// 按{@link #getVerticalDistance()}和{@link #getHorizontalDistance()}排序
		/// </summary>
		/// <param name="other">{@link Hierarchical}对象</param>
		/// <returns>比较值</returns>
		int CompareTo(Hierarchical other)
		{
			return DEFAULT_HIERARCHICAL_COMPARATOR.Compare(this, other);
		}

		// ====================== hierarchical  ======================

		/// <summary>
		/// 参照物，即坐标为{@code (0, 0)}的对象。
		/// 当对象本身即为参照物时，该方法应当返回其本身
		/// </summary>
		/// <returns>参照物</returns>
		object GetRoot();

		/// <summary>
		/// 获取该对象与参照物的垂直距离。
		/// 默认情况下，该距离即为当前对象与参照物之间相隔的层级数。
		/// </summary>
		/// <returns>合成注解与根对象的垂直距离</returns>
		int GetVerticalDistance();

		/// <summary>
		/// 获取该对象与参照物的水平距离。
		/// 默认情况下，该距离即为当前对象在与参照物{@link #getVerticalDistance()}相同的情况下条，
		/// 该对象被扫描到的顺序。
		/// </summary>
		/// <returns>合成注解与根对象的水平距离</returns>
		int GetHorizontalDistance();

		// ====================== selector  ======================

		/// <summary>
		/// {@link Hierarchical}选择器，用于根据一定的规则从两个{@link Hierarchical}实现类中选择并返回一个最合适的对象
		/// </summary>
		interface Selector
		{
			/// <summary>
			/// 返回距离根对象更近的对象，当距离一样时优先返回旧对象
			/// </summary>
			static readonly Selector NEAREST_AND_OLDEST_PRIORITY = new NearestAndOldestPrioritySelector();

			/// <summary>
			/// 返回距离根对象更近的对象，当距离一样时优先返回新对象
			/// </summary>
			static readonly Selector NEAREST_AND_NEWEST_PRIORITY = new NearestAndNewestPrioritySelector();

			/// <summary>
			/// 返回距离根对象更远的对象，当距离一样时优先返回旧对象
			/// </summary>
			static readonly Selector FARTHEST_AND_OLDEST_PRIORITY = new FarthestAndOldestPrioritySelector();

			/// <summary>
			/// 返回距离根对象更远的对象，当距离一样时优先返回新对象
			/// </summary>
			static readonly Selector FARTHEST_AND_NEWEST_PRIORITY = new FarthestAndNewestPrioritySelector();

			/// <summary>
			/// 比较两个被合成的对象，选择其中的一个并返回
			/// </summary>
			/// <typeparam name="T">复合注解类型</typeparam>
			/// <param name="prev">上一对象，该参数不允许为空</param>
			/// <param name="next">下一对象，该参数不允许为空</param>
			/// <returns>对象</returns>
			T Choose<T>(T prev, T next) where T : Hierarchical;
		}

		/// <summary>
		/// 返回距离根对象更近的注解，当距离一样时优先返回旧注解
		/// </summary>
		class NearestAndOldestPrioritySelector : Selector
		{
			public T Choose<T>(T oldAnnotation, T newAnnotation) where T : Hierarchical
			{
				return newAnnotation.GetVerticalDistance() < oldAnnotation.GetVerticalDistance() ? newAnnotation : oldAnnotation;
			}
		}

		/// <summary>
		/// 返回距离根对象更近的注解，当距离一样时优先返回新注解
		/// </summary>
		class NearestAndNewestPrioritySelector : Selector
		{
			public T Choose<T>(T oldAnnotation, T newAnnotation) where T : Hierarchical
			{
				return newAnnotation.GetVerticalDistance() <= oldAnnotation.GetVerticalDistance() ? newAnnotation : oldAnnotation;
			}
		}

		/// <summary>
		/// 返回距离根对象更远的注解，当距离一样时优先返回旧注解
		/// </summary>
		class FarthestAndOldestPrioritySelector : Selector
		{
			public T Choose<T>(T oldAnnotation, T newAnnotation) where T : Hierarchical
			{
				return newAnnotation.GetVerticalDistance() > oldAnnotation.GetVerticalDistance() ? newAnnotation : oldAnnotation;
			}
		}

		/// <summary>
		/// 返回距离根对象更远的注解，当距离一样时优先返回新注解
		/// </summary>
		class FarthestAndNewestPrioritySelector : Selector
		{
			public T Choose<T>(T oldAnnotation, T newAnnotation) where T : Hierarchical
			{
				return newAnnotation.GetVerticalDistance() >= oldAnnotation.GetVerticalDistance() ? newAnnotation : oldAnnotation;
			}
		}
	}

	/// <summary>
	/// 比较器类
	/// </summary>
	/// <typeparam name="T">比较类型</typeparam>
	public class Comparator<T>
	{
		private readonly Func<T, T, int> _compareFunc;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="compareFunc">比较函数</param>
		public Comparator(Func<T, T, int> compareFunc)
		{
			_compareFunc = compareFunc;
		}

		/// <summary>
		/// 比较两个对象
		/// </summary>
		/// <param name="x">第一个对象</param>
		/// <param name="y">第二个对象</param>
		/// <returns>比较结果</returns>
		public int Compare(T x, T y)
		{
			return _compareFunc(x, y);
		}
	}
}