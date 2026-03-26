using System;
using System.Runtime.CompilerServices;

namespace WellTool.Core.Builder
{
    /// <summary>
    /// 包装唯一键（RuntimeHelpers.GetHashCode()）使对象只有和自己 equals
    /// </summary>
    /// <remarks>
    /// 此对象用于消除小概率下RuntimeHelpers.GetHashCode()产生的ID重复问题。
    /// </remarks>
    internal class IDKey : IEquatable<IDKey>
    {
        private readonly object _value;
        private readonly int _id;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="obj">计算唯一ID的对象</param>
        public IDKey(object obj)
        {
            _id = RuntimeHelpers.GetHashCode(obj);
            // There have been some cases that return the
            // same identity hash code for different objects. So
            // the value is also added to disambiguate these cases.
            _value = obj;
        }

        /// <summary>
        /// returns hashcode - i.e. the system identity hashcode.
        /// </summary>
        /// <returns>the hashcode</returns>
        public override int GetHashCode()
        {
            return _id;
        }

        /// <summary>
        /// checks if instances are equal
        /// </summary>
        /// <param name="other">The other object to compare to</param>
        /// <returns>if the instances are for the same object</returns>
        public override bool Equals(object other)
        {
            return Equals(other as IDKey);
        }

        /// <summary>
        /// checks if instances are equal
        /// </summary>
        /// <param name="other">The other IDKey to compare to</param>
        /// <returns>if the instances are for the same object</returns>
        public bool Equals(IDKey other)
        {
            if (other == null)
            {
                return false;
            }
            if (_id != other._id)
            {
                return false;
            }
            // Note that identity equals is used.
            return object.ReferenceEquals(_value, other._value);
        }
    }
}