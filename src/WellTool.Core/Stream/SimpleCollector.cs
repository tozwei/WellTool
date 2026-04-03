using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Streamss
{
    /// <summary>
    /// 简单 Collector 接口实现
    /// </summary>
    /// <typeparam name="T">输入数据类型</typeparam>
    /// <typeparam name="A">累积结果的容器类型</typeparam>
    /// <typeparam name="R">数据结果类型</typeparam>
    public class SimpleCollector<T, A, R> : IEnumerable<R>
    {
        private readonly Func<A> _supplier;
        private readonly Action<A, T> _accumulator;
        private readonly Func<A, A, A> _combiner;
        private readonly Func<A, R> _finisher;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="supplier">创建新的结果容器函数</param>
        /// <param name="accumulator">将输入元素合并到结果容器中函数</param>
        /// <param name="combiner">合并两个结果容器函数（并行流使用）</param>
        /// <param name="finisher">将结果容器转换成最终的表示函数</param>
        public SimpleCollector(
            Func<A> supplier,
            Action<A, T> accumulator,
            Func<A, A, A> combiner,
            Func<A, R> finisher)
        {
            _supplier = supplier;
            _accumulator = accumulator;
            _combiner = combiner;
            _finisher = finisher;
        }

        /// <summary>
        /// 执行汇聚操作
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>结果</returns>
        public R Execute(IEnumerable<T> stream)
        {
            var container = _supplier();
            foreach (var item in stream)
            {
                _accumulator(container, item);
            }
            return _finisher(container);
        }

        /// <summary>
        /// 返回迭代器
        /// </summary>
        public IEnumerator<R> GetEnumerator()
        {
            throw new NotSupportedException("SimpleCollector does not support enumeration directly");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
