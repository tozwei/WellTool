using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Builder
{
	/// <summary>
	/// 用于构建 {@link IComparable#CompareTo(Object)} 方法的辅助工具
	/// </summary>
	/// <author>looly，Apache-Commons</author>
	/// <since>4.2.2</since>
	public class CompareToBuilder : Builder<int>
	{
		/** 当前比较状态 */
		private int _comparison;

		/// <summary>
		/// 构造，构造后调用append方法增加比较项，然后调用{@link #ToComparison()}获取结果
		/// </summary>
		public CompareToBuilder()
		{
			_comparison = 0;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// 通过反射比较两个Bean对象，对象字段可以为private。比较规则如下：
		/// 
		/// <ul>
		/// <li>static字段不比较</li>
		/// <li>Transient字段不参与比较</li>
		/// <li>父类字段参与比较</li>
		/// </ul>
		/// 
		///<p>
		///如果被比较的两个对象都为<code>null</code>，被认为相同。
		/// </summary>
		/// <param name="lhs">第一个对象</param>
		/// <param name="rhs">第二个对象</param>
		/// <returns>a negative integer, zero, or a positive integer as <code>lhs</code>
		///  is less than, equal to, or greater than <code>rhs</code></returns>
		/// <exception cref="NullReferenceException">if either (but not both) parameters are <code>null</code></exception>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public static int ReflectionCompare(object lhs, object rhs)
		{
			return ReflectionCompare(lhs, rhs, false, null);
		}

		/// <summary>
		/// <p>Compares two <code>Object</code>s via reflection.</p>
		/// 
		/// <p>Fields can be private, thus <code>FieldInfo.SetValue</code>
		/// is used to bypass normal access control checks. This will fail under a
		/// security manager unless the appropriate permissions are set.</p>
		/// 
		/// <ul>
		/// <li>Static fields will not be compared</li>
		/// <li>If <code>compareTransients</code> is <code>true</code>,
		///     compares transient members.  Otherwise ignores them, as they
		///     are likely derived fields.</li>
		/// <li>Superclass fields will be compared</li>
		/// </ul>
		/// 
		/// <p>If both <code>lhs</code> and <code>rhs</code> are <code>null</code>,
		/// they are considered equal.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="compareTransients">whether to compare transient fields</param>
		/// <returns>a negative integer, zero, or a positive integer as <code>lhs</code>
		///  is less than, equal to, or greater than <code>rhs</code></returns>
		/// <exception cref="NullReferenceException">if either <code>lhs</code> or <code>rhs</code>
		///  (but not both) is <code>null</code></exception>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public static int ReflectionCompare(object lhs, object rhs, bool compareTransients)
		{
			return ReflectionCompare(lhs, rhs, compareTransients, null);
		}

		/// <summary>
		/// <p>Compares two <code>Object</code>s via reflection.</p>
		/// 
		/// <p>Fields can be private, thus <code>FieldInfo.SetValue</code>
		/// is used to bypass normal access control checks. This will fail under a
		/// security manager unless the appropriate permissions are set.</p>
		/// 
		/// <ul>
		/// <li>Static fields will not be compared</li>
		/// <li>If <code>compareTransients</code> is <code>true</code>,
		///     compares transient members.  Otherwise ignores them, as they
		///     are likely derived fields.</li>
		/// <li>Superclass fields will be compared</li>
		/// </ul>
		/// 
		/// <p>If both <code>lhs</code> and <code>rhs</code> are <code>null</code>,
		/// they are considered equal.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="excludeFields">Collection of String fields to exclude</param>
		/// <returns>a negative integer, zero, or a positive integer as <code>lhs</code>
		///  is less than, equal to, or greater than <code>rhs</code></returns>
		/// <exception cref="NullReferenceException">if either <code>lhs</code> or <code>rhs</code>
		///  (but not both) is <code>null</code></exception>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public static int ReflectionCompare(object lhs, object rhs, ICollection<string> excludeFields)
		{
			return ReflectionCompare(lhs, rhs, excludeFields.ToArray());
		}

		/// <summary>
		/// <p>Compares two <code>Object</code>s via reflection.</p>
		/// 
		/// <p>Fields can be private, thus <code>FieldInfo.SetValue</code>
		/// is used to bypass normal access control checks. This will fail under a
		/// security manager unless the appropriate permissions are set.</p>
		/// 
		/// <ul>
		/// <li>Static fields will not be compared</li>
		/// <li>If <code>compareTransients</code> is <code>true</code>,
		///     compares transient members.  Otherwise ignores them, as they
		///     are likely derived fields.</li>
		/// <li>Superclass fields will be compared</li>
		/// </ul>
		/// 
		/// <p>If both <code>lhs</code> and <code>rhs</code> are <code>null</code>,
		/// they are considered equal.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="excludeFields">array of fields to exclude</param>
		/// <returns>a negative integer, zero, or a positive integer as <code>lhs</code>
		///  is less than, equal to, or greater than <code>rhs</code></returns>
		/// <exception cref="NullReferenceException">if either <code>lhs</code> or <code>rhs</code>
		///  (but not both) is <code>null</code></exception>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public static int ReflectionCompare(object lhs, object rhs, params string[] excludeFields)
		{
			return ReflectionCompare(lhs, rhs, false, null, excludeFields);
		}

		/// <summary>
		/// <p>Compares two <code>Object</code>s via reflection.</p>
		/// 
		/// <p>Fields can be private, thus <code>FieldInfo.SetValue</code>
		/// is used to bypass normal access control checks. This will fail under a
		/// security manager unless the appropriate permissions are set.</p>
		/// 
		/// <ul>
		/// <li>Static fields will not be compared</li>
		/// <li>If the <code>compareTransients</code> is <code>true</code>,
		///     compares transient members.  Otherwise ignores them, as they
		///     are likely derived fields.</li>
		/// <li>Compares superclass fields up to and including <code>reflectUpToClass</code>.
		///     If <code>reflectUpToClass</code> is <code>null</code>, compares all superclass fields.</li>
		/// </ul>
		/// 
		/// <p>If both <code>lhs</code> and <code>rhs</code> are <code>null</code>,
		/// they are considered equal.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="compareTransients">whether to compare transient fields</param>
		/// <param name="reflectUpToClass">last superclass for which fields are compared</param>
		/// <param name="excludeFields">fields to exclude</param>
		/// <returns>a negative integer, zero, or a positive integer as <code>lhs</code>
		///  is less than, equal to, or greater than <code>rhs</code></returns>
		/// <exception cref="NullReferenceException">if either <code>lhs</code> or <code>rhs</code>
		///  (but not both) is <code>null</code></exception>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public static int ReflectionCompare(
			object lhs,
			object rhs,
			bool compareTransients,
			Type reflectUpToClass,
			params string[] excludeFields)
		{
			if (lhs == rhs)
			{
				return 0;
			}
			if (lhs == null || rhs == null)
			{
				throw new NullReferenceException();
			}
			Type lhsClazz = lhs.GetType();
			if (!lhsClazz.IsInstanceOfType(rhs))
			{
				throw new InvalidCastException();
			}
			var compareToBuilder = new CompareToBuilder();
			ReflectionAppend(lhs, rhs, lhsClazz, compareToBuilder, compareTransients, excludeFields);
			while (lhsClazz.BaseType != null && lhsClazz != reflectUpToClass)
			{
				lhsClazz = lhsClazz.BaseType;
				ReflectionAppend(lhs, rhs, lhsClazz, compareToBuilder, compareTransients, excludeFields);
			}
			return compareToBuilder.ToComparison();
		}

		/// <summary>
		/// <p>Appends to <code>builder</code> the comparison of <code>lhs</code>
		/// to <code>rhs</code> using the fields defined in <code>clazz</code>.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="clazz"><code>Type</code> that defines fields to be compared</param>
		/// <param name="builder"><code>CompareToBuilder</code> to append to</param>
		/// <param name="useTransients">whether to compare transient fields</param>
		/// <param name="excludeFields">fields to exclude</param>
		private static void ReflectionAppend(
			object lhs,
			object rhs,
			Type clazz,
			CompareToBuilder builder,
			bool useTransients,
			string[] excludeFields)
		{
			var fields = clazz.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (var field in fields)
			{
				if (builder._comparison != 0)
				{
					break;
				}
				if (ArrayContains(excludeFields, field.Name) ||
					field.Name.Contains("$") ||
					(!useTransients && field.IsNotSerialized) ||
					field.IsStatic)
				{
					continue;
				}
				try
				{
					builder.Append(field.GetValue(lhs), field.GetValue(rhs));
				}
				catch (System.Exception e)
				{
					// This can't happen. Would get a Security exception instead.
					// Throw a runtime exception in case the impossible happens.
					throw new InvalidOperationException("Unexpected Exception", e);
				}
			}
		}

		/// <summary>
		/// 检查数组是否包含指定元素
		/// </summary>
		/// <param name="array">数组</param>
		/// <param name="element">元素</param>
		/// <returns>是否包含</returns>
		private static bool ArrayContains(string[] array, string element)
		{
			if (array == null)
			{
				return false;
			}
			foreach (var item in array)
			{
				if (item == element)
				{
					return true;
				}
			}
			return false;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// <p>Appends to the <code>builder</code> the <code>CompareTo(Object)</code>
		/// result of the superclass.</p>
		/// </summary>
		/// <param name="superCompareTo">result of calling <code>super.CompareTo(Object)</code></param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder AppendSuper(int superCompareTo)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = superCompareTo;
			return this;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// <p>Appends to the <code>builder</code> the comparison of
		/// two <code>Object</code>s.</p>
		/// 
		/// <ol>
		/// <li>Check if <code>lhs == rhs</code></li>
		/// <li>Check if either <code>lhs</code> or <code>rhs</code> is <code>null</code>,
		///     a <code>null</code> object is less than a non-<code>null</code> object</li>
		/// <li>Check the object contents</li>
		/// </ol>
		/// 
		/// <p><code>lhs</code> must either be an array or implement {@link IComparable}.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <returns>this - used to chain append calls</returns>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public CompareToBuilder Append(object lhs, object rhs)
		{
			return Append(lhs, rhs, null);
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the comparison of
		/// two <code>Object</code>s.</p>
		/// 
		/// <ol>
		/// <li>Check if <code>lhs == rhs</code></li>
		/// <li>Check if either <code>lhs</code> or <code>rhs</code> is <code>null</code>,
		///     a <code>null</code> object is less than a non-<code>null</code> object</li>
		/// <li>Check the object contents</li>
		/// </ol>
		/// 
		/// <p>If <code>lhs</code> is an array, array comparison methods will be used.
		/// Otherwise <code>comparator</code> will be used to compare the objects.
		/// If <code>comparator</code> is <code>null</code>, <code>lhs</code> must
		/// implement {@link IComparable} instead.</p>
		/// </summary>
		/// <param name="lhs">left-hand object</param>
		/// <param name="rhs">right-hand object</param>
		/// <param name="comparator"><code>IComparer</code> used to compare the objects,
		///  <code>null</code> means treat lhs as <code>IComparable</code></param>
		/// <returns>this - used to chain append calls</returns>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public CompareToBuilder Append(object lhs, object rhs, IComparer<object> comparator)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.GetType().IsArray)
			{
				// switch on type of array, to dispatch to the correct handler
				// handles multi dimensional arrays
				// throws a InvalidCastException if rhs is not the correct array type
				if (lhs is long[])
				{
					Append((long[])lhs, (long[])rhs);
				}
				else if (lhs is int[])
				{
					Append((int[])lhs, (int[])rhs);
				}
				else if (lhs is short[])
				{
					Append((short[])lhs, (short[])rhs);
				}
				else if (lhs is char[])
				{
					Append((char[])lhs, (char[])rhs);
				}
				else if (lhs is byte[])
				{
					Append((byte[])lhs, (byte[])rhs);
				}
				else if (lhs is double[])
				{
					Append((double[])lhs, (double[])rhs);
				}
				else if (lhs is float[])
				{
					Append((float[])lhs, (float[])rhs);
				}
				else if (lhs is bool[])
				{
					Append((bool[])lhs, (bool[])rhs);
				}
				else
				{
					// not an array of primitives
					// throws a InvalidCastException if rhs is not an array
					Append((object[])lhs, (object[])rhs, comparator);
				}
			}
			else
			{
				// the simple case, not an array, just test the element
				if (comparator == null)
				{
					if (lhs is IComparable comparable)
					{
						_comparison = comparable.CompareTo(rhs);
					}
					else
					{
						throw new InvalidCastException($"{lhs.GetType()} does not implement IComparable");
					}
				}
				else
				{
					_comparison = comparator.Compare(lhs, rhs);
				}
			}
			return this;
		}

		//-------------------------------------------------------------------------
		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>long</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(long lhs, long rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>int</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(int lhs, int rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>short</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(short lhs, short rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>char</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(char lhs, char rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>byte</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(byte lhs, byte rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the comparison of
		/// two <code>double</code>s.</p>
		/// 
		/// <p>This handles NaNs, Infinities, and <code>-0.0</code>.</p>
		/// 
		/// <p>It is compatible with the hash code generated by
		/// <code>HashCodeBuilder</code>.</p>
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(double lhs, double rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the comparison of
		/// two <code>float</code>s.</p>
		/// 
		/// <p>This handles NaNs, Infinities, and <code>-0.0</code>.</p>
		/// 
		/// <p>It is compatible with the hash code generated by
		/// <code>HashCodeBuilder</code>.</p>
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(float lhs, float rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			_comparison = lhs.CompareTo(rhs);
			return this;
		}

		/// <summary>
		/// Appends to the <code>builder</code> the comparison of
		/// two <code>bool</code>s.
		/// </summary>
		/// <param name="lhs">left-hand value</param>
		/// <param name="rhs">right-hand value</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(bool lhs, bool rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == false)
			{
				_comparison = -1;
			}
			else
			{
				_comparison = +1;
			}
			return this;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>Object</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a short length array is less than a long length array</li>
		///  <li>Check array contents element by element using {@link #Append(Object, Object, IComparer)}</li>
		/// </ol>
		/// 
		/// <p>This method will also will be called for the top level of multi-dimensional,
		/// ragged, and multi-typed arrays.</p>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public CompareToBuilder Append(object[] lhs, object[] rhs)
		{
			return Append(lhs, rhs, null);
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>Object</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a short length array is less than a long length array</li>
		///  <li>Check array contents element by element using {@link #Append(Object, Object, IComparer)}</li>
		/// </ol>
		/// 
		/// <p>This method will also will be called for the top level of multi-dimensional,
		/// ragged, and multi-typed arrays.</p>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <param name="comparator"><code>IComparer</code> to use to compare the array elements,
		///  <code>null</code> means to treat <code>lhs</code> elements as <code>IComparable</code>.</param>
		/// <returns>this - used to chain append calls</returns>
		/// <exception cref="InvalidCastException">if <code>rhs</code> is not assignment-compatible with <code>lhs</code></exception>
		public CompareToBuilder Append(object[] lhs, object[] rhs, IComparer<object> comparator)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i], comparator);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>long</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(long, long)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(long[] lhs, long[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>int</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(int, int)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(int[] lhs, int[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>short</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(short, short)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(short[] lhs, short[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>char</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(char, char)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(char[] lhs, char[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>byte</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(byte, byte)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(byte[] lhs, byte[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>double</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(double, double)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(double[] lhs, double[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>float</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(float, float)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(float[] lhs, float[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		/// <summary>
		/// <p>Appends to the <code>builder</code> the deep comparison of
		/// two <code>bool</code> arrays.</p>
		/// 
		/// <ol>
		///  <li>Check if arrays are the same using <code>==</code></li>
		///  <li>Check if for <code>null</code>, <code>null</code> is less than non-<code>null</code></li>
		///  <li>Check array length, a shorter length array is less than a longer length array</li>
		///  <li>Check array contents element by element using {@link #Append(bool, bool)}</li>
		/// </ol>
		/// </summary>
		/// <param name="lhs">left-hand array</param>
		/// <param name="rhs">right-hand array</param>
		/// <returns>this - used to chain append calls</returns>
		public CompareToBuilder Append(bool[] lhs, bool[] rhs)
		{
			if (_comparison != 0)
			{
				return this;
			}
			if (lhs == rhs)
			{
				return this;
			}
			if (lhs == null)
			{
				_comparison = -1;
				return this;
			}
			if (rhs == null)
			{
				_comparison = +1;
				return this;
			}
			if (lhs.Length != rhs.Length)
			{
				_comparison = (lhs.Length < rhs.Length) ? -1 : +1;
				return this;
			}
			for (int i = 0; i < lhs.Length && _comparison == 0; i++)
			{
				Append(lhs[i], rhs[i]);
			}
			return this;
		}

		//-----------------------------------------------------------------------
		/// <summary>
		/// Returns a negative integer, a positive integer, or zero as
		/// the <code>builder</code> has judged the "left-hand" side
		/// as less than, greater than, or equal to the "right-hand"
		/// side.
		/// </summary>
		/// <returns>final comparison result</returns>
		/// <seealso cref="Build"/>
		public int ToComparison()
		{
			return _comparison;
		}

		/// <summary>
		/// Returns a negative Integer, a positive Integer, or zero as
		/// the <code>builder</code> has judged the "left-hand" side
		/// as less than, greater than, or equal to the "right-hand"
		/// side.
		/// </summary>
		/// <returns>final comparison result as an Integer</returns>
		/// <seealso cref="ToComparison"/>
		public int Build()
		{
			return ToComparison();
		}
	}
}