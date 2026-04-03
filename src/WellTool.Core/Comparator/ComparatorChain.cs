using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 比较器链。此链包装了多个比较器，最终比较结果按照比较器顺序综合多个比较器结果。
    /// 按照比较器链的顺序分别比较，如果比较出相等则转向下一个比较器，否则直接返回
    /// </summary>
    /// <typeparam name="E">被比较对象类型</typeparam>
    [Serializable]
    public class ComparatorChain<E> : IComparer<E>
    {
        private static readonly long SerialVersionUID = -2426725788913962429L;

        private readonly List<IComparer<E>> _chain;
        private readonly List<bool> _orderingBits;
        private bool _lock = false;

        /// <summary>
        /// 构造空的比较器链，必须至少有一个比较器
        /// </summary>
        public ComparatorChain()
        {
            _chain = new List<IComparer<E>>();
            _orderingBits = new List<bool>();
        }

        /// <summary>
        /// 构造，初始化单一比较器。比较器为正序
        /// </summary>
        /// <param name="comparator">在比较器链中的第一个比较器</param>
        public ComparatorChain(IComparer<E> comparator)
            : this(comparator, false)
        {
        }

        /// <summary>
        /// 构造，初始化单一比较器。自定义正序还是反序
        /// </summary>
        /// <param name="comparator">在比较器链中的第一个比较器</param>
        /// <param name="reverse">是否反序，true表示反序，false正序</param>
        public ComparatorChain(IComparer<E> comparator, bool reverse)
        {
            _chain = new List<IComparer<E>> { comparator };
            _orderingBits = new List<bool> { reverse };
        }

        /// <summary>
        /// 构造，使用已有的比较器列表
        /// </summary>
        /// <param name="comparators">比较器列表</param>
        public ComparatorChain(IEnumerable<IComparer<E>> comparators)
        {
            _chain = comparators.ToList();
            _orderingBits = Enumerable.Repeat(false, _chain.Count).ToList();
        }

        /// <summary>
        /// 在链的尾部添加比较器，使用正向排序
        /// </summary>
        /// <param name="comparator">比较器，正向</param>
        /// <returns>this</returns>
        public ComparatorChain<E> AddComparator(IComparer<E> comparator)
        {
            return AddComparator(comparator, false);
        }

        /// <summary>
        /// 在链的尾部添加比较器，使用给定排序方式
        /// </summary>
        /// <param name="comparator">比较器</param>
        /// <param name="reverse">是否反序，true 表示正序，false 反序</param>
        /// <returns>this</returns>
        public ComparatorChain<E> AddComparator(IComparer<E> comparator, bool reverse)
        {
            CheckLocked();

            _chain.Add(comparator);
            _orderingBits.Add(reverse);
            return this;
        }

        /// <summary>
        /// 比较器链中比较器个数
        /// </summary>
        public int Size => _chain.Count;

        /// <summary>
        /// 是否已经被锁定。当开始比较时（调用 Compare 方法）此值为 true
        /// </summary>
        public bool IsLocked => _lock;

        /// <summary>
        /// 执行比较。按照比较器链的顺序分别比较，如果比较出相等则转向下一个比较器，否则直接返回
        /// </summary>
        /// <param name="x">第一个对象</param>
        /// <param name="y">第二个对象</param>
        /// <returns>-1, 0, or 1</returns>
        public int Compare(E x, E y)
        {
            if (!_lock)
            {
                CheckChainIntegrity();
                _lock = true;
            }

            for (int comparatorIndex = 0; comparatorIndex < _chain.Count; comparatorIndex++)
            {
                var comparator = _chain[comparatorIndex];
                int retval = comparator.Compare(x, y);
                if (retval != 0)
                {
                    // 如果是反向排序，则反转结果
                    if (comparatorIndex < _orderingBits.Count && _orderingBits[comparatorIndex])
                    {
                        retval = retval > 0 ? -1 : 1;
                    }
                    return retval;
                }
            }

            // 如果比较器都已用完，返回0
            return 0;
        }

        /// <summary>
        /// 被锁定时抛出异常
        /// </summary>
        private void CheckLocked()
        {
            if (_lock)
            {
                throw new NotSupportedException("Comparator ordering cannot be changed after the first comparison is performed");
            }
        }

        /// <summary>
        /// 检查比较器链是否为空，为空抛出异常
        /// </summary>
        private void CheckChainIntegrity()
        {
            if (_chain.Count == 0)
            {
                throw new NotSupportedException("ComparatorChains must contain at least one Comparator");
            }
        }
    }
}
