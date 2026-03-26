using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace WellTool.Core.Builder
{
    /// <summary>
    /// Assists in implementing <see cref="object.GetHashCode()"/> methods.
    /// </summary>
    /// <remarks>
    /// This class enables a good <code>GetHashCode</code> method to be built for any class. 
    /// It follows the rules laid out in the book <a href="http://www.oracle.com/technetwork/java/effectivejava-136174.html">Effective Java</a> by Joshua Bloch.
    /// </remarks>
    public class HashCodeBuilder : Builder<int>
    {
        /// <summary>
        /// The default initial value to use in reflection hash code building.
        /// </summary>
        private static readonly int DEFAULT_INITIAL_VALUE = 17;

        /// <summary>
        /// The default multipler value to use in reflection hash code building.
        /// </summary>
        private static readonly int DEFAULT_MULTIPLIER_VALUE = 37;

        /// <summary>
        /// A registry of objects used by reflection methods to detect cyclical object references and avoid infinite loops.
        /// </summary>
        private static readonly ThreadLocal<HashSet<IDKey>> REGISTRY = new ThreadLocal<HashSet<IDKey>>();

        /// <summary>
        /// Returns the registry of objects being traversed by the reflection methods in the current thread.
        /// </summary>
        /// <returns>Set the registry of objects being traversed</returns>
        private static HashSet<IDKey> GetRegistry()
        {
            return REGISTRY.Value;
        }

        /// <summary>
        /// Returns <code>true</code> if the registry contains the given object. Used by the reflection methods to avoid
        /// infinite loops.
        /// </summary>
        /// <param name="value">The object to lookup in the registry.</param>
        /// <returns>boolean <code>true</code> if the registry contains the given object.</returns>
        private static bool IsRegistered(object value)
        {
            HashSet<IDKey> registry = GetRegistry();
            return registry != null && registry.Contains(new IDKey(value));
        }

        /// <summary>
        /// Appends the fields and values defined by the given object of the given <code>Type</code>.
        /// </summary>
        /// <param name="obj">the object to append details of</param>
        /// <param name="clazz">the class to append details of</param>
        /// <param name="builder">the builder to append to</param>
        /// <param name="useTransients">whether to use transient fields</param>
        /// <param name="excludeFields">Collection of String field names to exclude from use in calculation of hash code</param>
        private static void ReflectionAppend(object obj, Type clazz, HashCodeBuilder builder, bool useTransients, string[] excludeFields)
        {
            if (IsRegistered(obj))
            {
                return;
            }
            try
            {
                Register(obj);
                FieldInfo[] fields = clazz.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (FieldInfo field in fields)
                {
                    if (!Array.Exists(excludeFields, fieldName => fieldName == field.Name)
                        && (field.Name.IndexOf('$') == -1)
                        && (useTransients || !field.IsNotSerialized)
                        && (!field.IsStatic))
                    {
                        try
                        {
                            object fieldValue = field.GetValue(obj);
                            builder.Append(fieldValue);
                        }
                        catch (Exception e)
                        {
                            // this can't happen. Would get a Security exception instead
                            // throw a runtime exception in case the impossible happens.
                            throw new InvalidOperationException("Unexpected access exception", e);
                        }
                    }
                }
            }
            finally
            {
                Unregister(obj);
            }
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <param name="initialNonZeroOddNumber">a non-zero, odd number used as the initial value. This will be the returned
        /// value if no fields are found to include in the hash code</param>
        /// <param name="multiplierNonZeroOddNumber">a non-zero, odd number used as the multiplier</param>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode(int initialNonZeroOddNumber, int multiplierNonZeroOddNumber, object obj)
        {
            return ReflectionHashCode(initialNonZeroOddNumber, multiplierNonZeroOddNumber, obj, false, null);
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <param name="initialNonZeroOddNumber">a non-zero, odd number used as the initial value. This will be the returned
        /// value if no fields are found to include in the hash code</param>
        /// <param name="multiplierNonZeroOddNumber">a non-zero, odd number used as the multiplier</param>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <param name="testTransients">whether to include transient fields</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode(int initialNonZeroOddNumber, int multiplierNonZeroOddNumber, object obj, bool testTransients)
        {
            return ReflectionHashCode(initialNonZeroOddNumber, multiplierNonZeroOddNumber, obj, testTransients, null);
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <typeparam name="T">the type of the object involved</typeparam>
        /// <param name="initialNonZeroOddNumber">a non-zero, odd number used as the initial value. This will be the returned
        /// value if no fields are found to include in the hash code</param>
        /// <param name="multiplierNonZeroOddNumber">a non-zero, odd number used as the multiplier</param>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <param name="testTransients">whether to include transient fields</param>
        /// <param name="reflectUpToClass">the superclass to reflect up to (inclusive), may be <code>null</code></param>
        /// <param name="excludeFields">array of field names to exclude from use in calculation of hash code</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode<T>(int initialNonZeroOddNumber, int multiplierNonZeroOddNumber, T obj, bool testTransients, Type reflectUpToClass, params string[] excludeFields)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "The object to build a hash code for must not be null");
            }
            HashCodeBuilder builder = new HashCodeBuilder(initialNonZeroOddNumber, multiplierNonZeroOddNumber);
            Type clazz = obj.GetType();
            ReflectionAppend(obj, clazz, builder, testTransients, excludeFields);
            while (clazz.BaseType != null && clazz != reflectUpToClass)
            {
                clazz = clazz.BaseType;
                ReflectionAppend(obj, clazz, builder, testTransients, excludeFields);
            }
            return builder.ToHashCode();
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <param name="testTransients">whether to include transient fields</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode(object obj, bool testTransients)
        {
            return ReflectionHashCode(DEFAULT_INITIAL_VALUE, DEFAULT_MULTIPLIER_VALUE, obj, testTransients, null);
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <param name="excludeFields">Collection of String field names to exclude from use in calculation of hash code</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode(object obj, ICollection<string> excludeFields)
        {
            string[] excludeArray = new string[excludeFields.Count];
            excludeFields.CopyTo(excludeArray, 0);
            return ReflectionHashCode(obj, excludeArray);
        }

        /// <summary>
        /// Uses reflection to build a valid hash code from the fields of <code>object</code>.
        /// </summary>
        /// <param name="obj">the Object to create a <code>GetHashCode</code> for</param>
        /// <param name="excludeFields">array of field names to exclude from use in calculation of hash code</param>
        /// <returns>int hash code</returns>
        public static int ReflectionHashCode(object obj, params string[] excludeFields)
        {
            return ReflectionHashCode(DEFAULT_INITIAL_VALUE, DEFAULT_MULTIPLIER_VALUE, obj, false, null, excludeFields);
        }

        /// <summary>
        /// Registers the given object. Used by the reflection methods to avoid infinite loops.
        /// </summary>
        /// <param name="value">The object to register.</param>
        internal static void Register(object value)
        {
            lock (typeof(HashCodeBuilder))
            {
                if (GetRegistry() == null)
                {
                    REGISTRY.Value = new HashSet<IDKey>();
                }
            }
            GetRegistry().Add(new IDKey(value));
        }

        /// <summary>
        /// Unregisters the given object.
        /// </summary>
        /// <param name="value">The object to unregister.</param>
        internal static void Unregister(object value)
        {
            HashSet<IDKey> registry = GetRegistry();
            if (registry != null)
            {
                registry.Remove(new IDKey(value));
                lock (typeof(HashCodeBuilder))
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
        /// Constant to use in building the hashCode.
        /// </summary>
        private readonly int _constant;

        /// <summary>
        /// Running total of the hashCode.
        /// </summary>
        private int _total;

        /// <summary>
        /// Uses two hard coded choices for the constants needed to build a <code>GetHashCode</code>.
        /// </summary>
        public HashCodeBuilder()
        {
            _constant = 37;
            _total = 17;
        }

        /// <summary>
        /// Two randomly chosen, odd numbers must be passed in. Ideally these should be different for each class,
        /// however this is not vital.
        /// </summary>
        /// <param name="initialOddNumber">an odd number used as the initial value</param>
        /// <param name="multiplierOddNumber">an odd number used as the multiplier</param>
        public HashCodeBuilder(int initialOddNumber, int multiplierOddNumber)
        {
            if (initialOddNumber % 2 == 0)
            {
                throw new ArgumentException("HashCodeBuilder requires an odd initial value");
            }
            if (multiplierOddNumber % 2 == 0)
            {
                throw new ArgumentException("HashCodeBuilder requires an odd multiplier");
            }
            _constant = multiplierOddNumber;
            _total = initialOddNumber;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>bool</code>.
        /// </summary>
        /// <param name="value">the boolean to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(bool value)
        {
            _total = _total * _constant + (value ? 0 : 1);
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>bool</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(bool[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (bool element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>byte</code>.
        /// </summary>
        /// <param name="value">the byte to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(byte value)
        {
            _total = _total * _constant + value;
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>byte</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(byte[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (byte element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>char</code>.
        /// </summary>
        /// <param name="value">the char to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(char value)
        {
            _total = _total * _constant + value;
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>char</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(char[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (char element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>double</code>.
        /// </summary>
        /// <param name="value">the double to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(double value)
        {
            return Append(BitConverter.DoubleToInt64Bits(value));
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>double</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(double[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (double element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>float</code>.
        /// </summary>
        /// <param name="value">the float to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(float value)
        {
            _total = _total * _constant + BitConverter.SingleToInt32Bits(value);
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>float</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(float[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (float element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for an <code>int</code>.
        /// </summary>
        /// <param name="value">the int to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(int value)
        {
            _total = _total * _constant + value;
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for an <code>int</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(int[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (int element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>long</code>.
        /// </summary>
        /// <param name="value">the long to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(long value)
        {
            _total = _total * _constant + (int)(value ^ (value >> 32));
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>long</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(long[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (long element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for an <code>object</code>.
        /// </summary>
        /// <param name="obj">the Object to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(object obj)
        {
            if (obj == null)
            {
                _total = _total * _constant;
            }
            else
            {
                if (obj.GetType().IsArray)
                {
                    // 'Switch' on type of array, to dispatch to the correct handler
                    // This handles multi dimensional arrays
                    if (obj is long[])
                    {
                        Append((long[])obj);
                    }
                    else if (obj is int[])
                    {
                        Append((int[])obj);
                    }
                    else if (obj is short[])
                    {
                        Append((short[])obj);
                    }
                    else if (obj is char[])
                    {
                        Append((char[])obj);
                    }
                    else if (obj is byte[])
                    {
                        Append((byte[])obj);
                    }
                    else if (obj is double[])
                    {
                        Append((double[])obj);
                    }
                    else if (obj is float[])
                    {
                        Append((float[])obj);
                    }
                    else if (obj is bool[])
                    {
                        Append((bool[])obj);
                    }
                    else
                    {
                        // Not an array of primitives
                        Append((object[])obj);
                    }
                }
                else
                {
                    _total = _total * _constant + obj.GetHashCode();
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for an <code>object</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(object[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (object element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>short</code>.
        /// </summary>
        /// <param name="value">the short to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(short value)
        {
            _total = _total * _constant + value;
            return this;
        }

        /// <summary>
        /// Append a <code>GetHashCode</code> for a <code>short</code> array.
        /// </summary>
        /// <param name="array">the array to add to the <code>GetHashCode</code></param>
        /// <returns>this</returns>
        public HashCodeBuilder Append(short[] array)
        {
            if (array == null)
            {
                _total = _total * _constant;
            }
            else
            {
                foreach (short element in array)
                {
                    Append(element);
                }
            }
            return this;
        }

        /// <summary>
        /// Adds the result of super.GetHashCode() to this builder.
        /// </summary>
        /// <param name="superHashCode">the result of calling <code>super.GetHashCode()</code></param>
        /// <returns>this HashCodeBuilder, used to chain calls.</returns>
        public HashCodeBuilder AppendSuper(int superHashCode)
        {
            _total = _total * _constant + superHashCode;
            return this;
        }

        /// <summary>
        /// Return the computed <code>GetHashCode</code>.
        /// </summary>
        /// <returns><code>GetHashCode</code> based on the fields appended</returns>
        public int ToHashCode()
        {
            return _total;
        }

        /// <summary>
        /// Returns the computed <code>GetHashCode</code>.
        /// </summary>
        /// <returns><code>GetHashCode</code> based on the fields appended</returns>
        public int Build()
        {
            return ToHashCode();
        }

        /// <summary>
        /// The computed <code>GetHashCode</code> from ToHashCode() is returned due to the likelihood
        /// of bugs in mis-calling ToHashCode() and the unlikeliness of it mattering what the hashCode for
        /// HashCodeBuilder itself is.
        /// </summary>
        /// <returns><code>GetHashCode</code> based on the fields appended</returns>
        public override int GetHashCode()
        {
            return ToHashCode();
        }
    }
}