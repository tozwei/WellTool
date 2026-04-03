using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using WellTool.Core.Lang;

namespace WellTool.Core.Builder
{
    /// <summary>
    /// <see cref="object.Equals(object)"/> 方法的构建器
    /// </summary>
    /// <remarks>
    /// 两个对象equals必须保证hashCode值相等，hashCode值相等不能保证一定equals
    /// </remarks>
    public class EqualsBuilder : Builder<bool>
    {
        /// <summary>
        /// A registry of objects used by reflection methods to detect cyclical object references and avoid infinite loops.
        /// </summary>
        private static readonly ThreadLocal<HashSet<Pair<IDKey, IDKey>>> REGISTRY = new ThreadLocal<HashSet<Pair<IDKey, IDKey>>>();

        /// <summary>
        /// Returns the registry of object pairs being traversed by the reflection
        /// methods in the current thread.
        /// </summary>
        /// <returns>Set the registry of objects being traversed</returns>
        internal static HashSet<Pair<IDKey, IDKey>> GetRegistry()
        {
            return REGISTRY.Value;
        }

        /// <summary>
        /// Converters value pair into a register pair.
        /// </summary>
        /// <param name="lhs">this object</param>
        /// <param name="rhs">the other object</param>
        /// <returns>the pair</returns>
        internal static Pair<IDKey, IDKey> GetRegisterPair(object lhs, object rhs)
        {
            IDKey left = new IDKey(lhs);
            IDKey right = new IDKey(rhs);
            return new Pair<IDKey, IDKey>(left, right);
        }

        /// <summary>
        /// Returns <code>true</code> if the registry contains the given object pair.
        /// Used by the reflection methods to avoid infinite loops.
        /// Objects might be swapped therefore a check is needed if the object pair
        /// is registered in given or swapped order.
        /// </summary>
        /// <param name="lhs">this object to lookup in registry</param>
        /// <param name="rhs">the other object to lookup on registry</param>
        /// <returns>boolean <code>true</code> if the registry contains the given object.</returns>
        internal static bool IsRegistered(object lhs, object rhs)
        {
            HashSet<Pair<IDKey, IDKey>> registry = GetRegistry();
            if (registry == null)
            {
                return false;
            }

            Pair<IDKey, IDKey> pair = GetRegisterPair(lhs, rhs);
            Pair<IDKey, IDKey> swappedPair = new Pair<IDKey, IDKey>(pair.Value, pair.Key);

            return registry.Contains(pair) || registry.Contains(swappedPair);
        }

        /// <summary>
        /// Registers the given object pair.
        /// Used by the reflection methods to avoid infinite loops.
        /// </summary>
        /// <param name="lhs">this object to register</param>
        /// <param name="rhs">the other object to register</param>
        internal static void Register(object lhs, object rhs)
        {
            lock (typeof(EqualsBuilder))
            {
                if (GetRegistry() == null)
                {
                    REGISTRY.Value = new HashSet<Pair<IDKey, IDKey>>();
                }
            }

            HashSet<Pair<IDKey, IDKey>> registry = GetRegistry();
            Pair<IDKey, IDKey> pair = GetRegisterPair(lhs, rhs);
            registry.Add(pair);
        }

        /// <summary>
        /// Unregisters the given object pair.
        /// </summary>
        /// <param name="lhs">this object to unregister</param>
        /// <param name="rhs">the other object to unregister</param>
        internal static void Unregister(object lhs, object rhs)
        {
            HashSet<Pair<IDKey, IDKey>> registry = GetRegistry();
            if (registry != null)
            {
                Pair<IDKey, IDKey> pair = GetRegisterPair(lhs, rhs);
                registry.Remove(pair);

                lock (typeof(EqualsBuilder))
                {
                    //read again
                    registry = GetRegistry();
                    if (registry != null && registry.Count == 0)
                    {
                        REGISTRY.Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// 是否equals，此值随着构建会变更，默认true
        /// </summary>
        private bool isEquals = true;

        /// <summary>
        /// 构造，初始状态值为true
        /// </summary>
        public EqualsBuilder()
        {
            // do nothing for now.
        }

        /// <summary>
        /// 反射检查两个对象是否equals，此方法检查对象及其父对象的属性（包括私有属性）是否equals
        /// </summary>
        /// <param name="lhs">此对象</param>
        /// <param name="rhs">另一个对象</param>
        /// <param name="excludeFields">排除的字段集合，如果有不参与计算equals的字段加入此集合即可</param>
        /// <returns>两个对象是否equals，是返回<code>true</code></returns>
        public static bool ReflectionEquals(object lhs, object rhs, ICollection<string> excludeFields)
        {
            string[] excludeArray = new string[excludeFields.Count];
            excludeFields.CopyTo(excludeArray, 0);
            return ReflectionEquals(lhs, rhs, excludeArray);
        }

        /// <summary>
        /// 反射检查两个对象是否equals，此方法检查对象及其父对象的属性（包括私有属性）是否equals
        /// </summary>
        /// <param name="lhs">此对象</param>
        /// <param name="rhs">另一个对象</param>
        /// <param name="excludeFields">排除的字段集合，如果有不参与计算equals的字段加入此集合即可</param>
        /// <returns>两个对象是否equals，是返回<code>true</code></returns>
        public static bool ReflectionEquals(object lhs, object rhs, params string[] excludeFields)
        {
            return ReflectionEquals(lhs, rhs, false, null, excludeFields);
        }

        /// <summary>
        /// This method uses reflection to determine if the two <code>object</code>s
        /// are equal.
        /// </summary>
        /// <param name="lhs">this object</param>
        /// <param name="rhs">the other object</param>
        /// <param name="testTransients">whether to include transient fields</param>
        /// <returns><code>true</code> if the two Objects have tested equals.</returns>
        public static bool ReflectionEquals(object lhs, object rhs, bool testTransients)
        {
            return ReflectionEquals(lhs, rhs, testTransients, null);
        }

        /// <summary>
        /// This method uses reflection to determine if the two <code>object</code>s
        /// are equal.
        /// </summary>
        /// <param name="lhs">this object</param>
        /// <param name="rhs">the other object</param>
        /// <param name="testTransients">whether to include transient fields</param>
        /// <param name="reflectUpToClass">the superclass to reflect up to (inclusive),
        /// may be <code>null</code></param>
        /// <param name="excludeFields">array of field names to exclude from testing</param>
        /// <returns><code>true</code> if the two Objects have tested equals.</returns>
        public static bool ReflectionEquals(object lhs, object rhs, bool testTransients, Type reflectUpToClass, params string[] excludeFields)
        {
            if (lhs == rhs)
            {
                return true;
            }
            if (lhs == null || rhs == null)
            {
                return false;
            }
            
            // Find the leaf class since there may be transients in the leaf
            // class or in classes between the leaf and root.
            // If we are not testing transients or a subclass has no ivars,
            // then a subclass can test equals to a superclass.
            Type lhsClass = lhs.GetType();
            Type rhsClass = rhs.GetType();
            Type testClass;
            
            if (lhsClass.IsAssignableFrom(rhsClass))
            {
                testClass = lhsClass;
                if (!rhsClass.IsAssignableFrom(lhsClass))
                {
                    // rhsClass is a subclass of lhsClass
                    testClass = rhsClass;
                }
            }
            else if (rhsClass.IsAssignableFrom(lhsClass))
            {
                testClass = rhsClass;
                if (!lhsClass.IsAssignableFrom(rhsClass))
                {
                    // lhsClass is a subclass of rhsClass
                    testClass = lhsClass;
                }
            }
            else
            {
                // The two classes are not related.
                return false;
            }
            
            EqualsBuilder equalsBuilder = new EqualsBuilder();
            try
            {
                if (testClass.IsArray)
                {
                    equalsBuilder.Append(lhs, rhs);
                }
                else
                {
                    ReflectionAppend(lhs, rhs, testClass, equalsBuilder, testTransients, excludeFields);
                    while (testClass.BaseType != null && testClass != reflectUpToClass)
                    {
                        testClass = testClass.BaseType;
                        ReflectionAppend(lhs, rhs, testClass, equalsBuilder, testTransients, excludeFields);
                    }
                }
            }
            catch (ArgumentException)
            {
                // In this case, we tried to test a subclass vs. a superclass and
                // the subclass has ivars or the ivars are transient and
                // we are testing transients.
                // If a subclass has ivars that we are trying to test them, we get an
                // exception and we know that the objects are not equal.
                return false;
            }
            return equalsBuilder.IsEquals();
        }

        /// <summary>
        /// Appends the fields and values defined by the given object of the
        /// given Class.
        /// </summary>
        /// <param name="lhs">the left hand object</param>
        /// <param name="rhs">the right hand object</param>
        /// <param name="clazz">the class to append details of</param>
        /// <param name="builder">the builder to append to</param>
        /// <param name="useTransients">whether to test transient fields</param>
        /// <param name="excludeFields">array of field names to exclude from testing</param>
        private static void ReflectionAppend(
                object lhs,
                object rhs,
                Type clazz,
                EqualsBuilder builder,
                bool useTransients,
                string[] excludeFields)
        {
            if (IsRegistered(lhs, rhs))
            {
                return;
            }

            try
            {
                Register(lhs, rhs);
                FieldInfo[] fields = clazz.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                for (int i = 0; i < fields.Length && builder.isEquals; i++)
                {
                    FieldInfo f = fields[i];
                    if (!Array.Exists(excludeFields, fieldName => fieldName == f.Name)
                            && (f.Name.IndexOf('$') == -1)
                            && (useTransients || !f.IsNotSerialized)
                            && (!f.IsStatic))
                    {
                        try
                        {
                            builder.Append(f.GetValue(lhs), f.GetValue(rhs));
                        }
                        catch (Exception e)
                        {
                            //this can't happen. Would get a Security exception instead
                            //throw a runtime exception in case the impossible happens.
                            throw new InvalidOperationException("Unexpected access exception", e);
                        }
                    }
                }
            }
            finally
            {
                Unregister(lhs, rhs);
            }
        }

        /// <summary>
        /// Adds the result of <code>super.equals()</code> to this builder.
        /// </summary>
        /// <param name="superEquals">the result of calling <code>super.equals()</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder AppendSuper(bool superEquals)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = superEquals;
            return this;
        }

        /// <summary>
        /// Test if two <code>object</code>s are equal using their
        /// <code>equals</code> method.
        /// </summary>
        /// <param name="lhs">the left hand object</param>
        /// <param name="rhs">the right hand object</param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(object lhs, object rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            if (lhs == rhs)
            {
                return this;
            }
            if (lhs == null || rhs == null)
            {
                return SetEquals(false);
            }
            if (lhs.GetType().IsArray && rhs.GetType().IsArray)
            {
                // 判断数组的equals
                return SetEquals(AreArraysEqual(lhs, rhs));
            }

            // The simple case, not an array, just test the element
            return SetEquals(lhs.Equals(rhs));
        }

        /// <summary>
        /// 比较两个数组是否相等
        /// </summary>
        /// <param name="lhs">左数组</param>
        /// <param name="rhs">右数组</param>
        /// <returns>是否相等</returns>
        private static bool AreArraysEqual(object lhs, object rhs)
        {
            if (!lhs.GetType().IsArray || !rhs.GetType().IsArray)
            {
                return false;
            }

            Array leftArray = (Array)lhs;
            Array rightArray = (Array)rhs;

            if (leftArray.Length != rightArray.Length)
            {
                return false;
            }

            if (leftArray.Rank != rightArray.Rank)
            {
                return false;
            }

            for (int i = 0; i < leftArray.Length; i++)
            {
                object leftElement = leftArray.GetValue(i);
                object rightElement = rightArray.GetValue(i);

                if (leftElement == null && rightElement == null)
                {
                    continue;
                }
                if (leftElement == null || rightElement == null)
                {
                    return false;
                }
                if (leftElement.GetType().IsArray && rightElement.GetType().IsArray)
                {
                    if (!AreArraysEqual(leftElement, rightElement))
                    {
                        return false;
                    }
                }
                else if (!leftElement.Equals(rightElement))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Test if two <code>long</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>long</code></param>
        /// <param name="rhs">the right hand <code>long</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(long lhs, long rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Test if two <code>int</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>int</code></param>
        /// <param name="rhs">the right hand <code>int</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(int lhs, int rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Test if two <code>short</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>short</code></param>
        /// <param name="rhs">the right hand <code>short</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(short lhs, short rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Test if two <code>char</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>char</code></param>
        /// <param name="rhs">the right hand <code>char</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(char lhs, char rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Test if two <code>byte</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>byte</code></param>
        /// <param name="rhs">the right hand <code>byte</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(byte lhs, byte rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Test if two <code>double</code>s are equal by testing that the
        /// pattern of bits returned by <code>doubleToLong</code> are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>double</code></param>
        /// <param name="rhs">the right hand <code>double</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(double lhs, double rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            return Append(BitConverter.DoubleToInt64Bits(lhs), BitConverter.DoubleToInt64Bits(rhs));
        }

        /// <summary>
        /// Test if two <code>float</code>s are equal by testing that the
        /// pattern of bits returned by doubleToLong are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>float</code></param>
        /// <param name="rhs">the right hand <code>float</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(float lhs, float rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            return Append(BitConverter.SingleToInt32Bits(lhs), BitConverter.SingleToInt32Bits(rhs));
        }

        /// <summary>
        /// Test if two <code>bool</code>s are equal.
        /// </summary>
        /// <param name="lhs">the left hand <code>bool</code></param>
        /// <param name="rhs">the right hand <code>bool</code></param>
        /// <returns>EqualsBuilder - used to chain calls.</returns>
        public EqualsBuilder Append(bool lhs, bool rhs)
        {
            if (isEquals == false)
            {
                return this;
            }
            isEquals = (lhs == rhs);
            return this;
        }

        /// <summary>
        /// Returns <code>true</code> if the fields that have been checked
        /// are all equal.
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsEquals()
        {
            return this.isEquals;
        }

        /// <summary>
        /// Returns <code>true</code> if the fields that have been checked
        /// are all equal.
        /// </summary>
        /// <returns><code>true</code> if all of the fields that have been checked
        /// are equal, <code>false</code> otherwise.</returns>
        public bool Build()
        {
            return IsEquals();
        }

        /// <summary>
        /// Sets the <code>isEquals</code> value.
        /// </summary>
        /// <param name="isEquals">The value to set.</param>
        /// <returns>this</returns>
        protected EqualsBuilder SetEquals(bool isEquals)
        {
            this.isEquals = isEquals;
            return this;
        }

        /// <summary>
        /// Reset the EqualsBuilder so you can use the same object again
        /// </summary>
        public void Reset()
        {
            this.isEquals = true;
        }
    }
}
