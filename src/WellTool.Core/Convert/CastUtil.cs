using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Converter
{
    public static class CastUtil
    {
        public static T Cast<T>(object obj)
        {
            return (T)obj;
        }

        public static T CastTo<T>(object obj)
        {
            return Converter.To<T>(obj);
        }

        public static ICollection<T> CastUp<T>(ICollection<object> collection)
        {
            return collection.Select(x => Converter.To<T>(x)).ToList();
        }

        public static ICollection<T> CastDown<T>(ICollection<object> collection)
        {
            return collection.Select(x => (T)x).ToList();
        }

        public static IList<T> CastUp<T>(IList<object> list)
        {
            return list.Select(x => Converter.To<T>(x)).ToList();
        }

        public static IList<T> CastDown<T>(IList<object> list)
        {
            return list.Select(x => (T)x).ToList();
        }

        public static ISet<T> CastUp<T>(ISet<object> set)
        {
            return set.Select(x => Converter.To<T>(x)).ToHashSet();
        }

        public static ISet<T> CastDown<T>(ISet<object> set)
        {
            return set.Select(x => (T)x).ToHashSet();
        }

        public static IDictionary<K, V> CastUp<K, V>(IDictionary<object, object> map)
        {
            return map.ToDictionary(kvp => Converter.To<K>(kvp.Key), kvp => Converter.To<V>(kvp.Value));
        }

        public static IDictionary<K, V> CastDown<K, V>(IDictionary<object, object> map)
        {
            return map.ToDictionary(kvp => (K)kvp.Key, kvp => (V)kvp.Value);
        }
    }
}