using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 加权随机列表
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class WeightListRandom<T>
    {
        private readonly List<WeightedItem<T>> _items = new List<WeightedItem<T>>();
        private double _totalWeight;

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item">元素</param>
        /// <param name="weight">权重</param>
        public void Add(T item, double weight)
        {
            if (weight < 0)
            {
                throw new ArgumentException("Weight cannot be negative", nameof(weight));
            }

            _items.Add(new WeightedItem<T>(item, weight));
            _totalWeight += weight;
        }

        /// <summary>
        /// 获取随机元素
        /// </summary>
        /// <returns>随机元素</returns>
        public T Next()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("No items in the list");
            }

            if (_items.Count == 1)
            {
                return _items[0].Item;
            }

            double random = new Random().NextDouble() * _totalWeight;
            double cumulative = 0;

            foreach (var weightedItem in _items)
            {
                cumulative += weightedItem.Weight;
                if (random < cumulative)
                {
                    return weightedItem.Item;
                }
            }

            // 浮点数精度问题，返回最后一个
            return _items[_items.Count - 1].Item;
        }

        /// <summary>
        /// 获取多个不重复的随机元素
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>随机元素列表</returns>
        public List<T> Next(int count)
        {
            if (count <= 0)
            {
                return new List<T>();
            }

            count = Math.Min(count, _items.Count);
            var result = new List<T>();
            var remaining = new List<WeightedItem<T>>(_items);

            for (int i = 0; i < count; i++)
            {
                double random = new Random().NextDouble() * remaining.Sum(x => x.Weight);
                double cumulative = 0;

                foreach (var item in remaining.ToArray())
                {
                    cumulative += item.Weight;
                    if (random < cumulative)
                    {
                        result.Add(item.Item);
                        remaining.Remove(item);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 清空所有元素
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            _totalWeight = 0;
        }

        /// <summary>
        /// 获取元素数量
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// 获取总权重
        /// </summary>
        public double TotalWeight => _totalWeight;

        /// <summary>
        /// 加权项
        /// </summary>
        private class WeightedItem<TItem>
        {
            public TItem Item { get; }
            public double Weight { get; }

            public WeightedItem(TItem item, double weight)
            {
                Item = item;
                Weight = weight;
            }
        }
    }
}
