using System.Collections.Generic;

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
            return Convert.To<T>(obj);
        }

        public static ICollection<T> CastUp<T>(ICollection<object> collection)
        {
            return (ICollection<T>)collection;
        }

        public static ICollection<T> CastDown<T>(ICollection<object> collection)
        {
            return (ICollection<T>)collection;
        }

        public static IList<T> CastUp<T>(IList<object> list)
        {
            return (IList<T>)list;
        }

        public static IList<T> CastDown<T>(IList<object> list)
        {
            return (IList<T>)list;
        }

        public static ISet<T> CastUp<T>(ISet<object> set)
        {
            return (ISet<T>)set;
        }

        public static ISet<T> CastDown<T>(ISet<object> set)
        {
            return (ISet<T>)set;
        }

        public static IDictionary<K, V> CastUp<K, V>(IDictionary<object, object> map)
        {
            return (IDictionary<K, V>)map;
        }

        public static IDictionary<K, V> CastDown<K, V>(IDictionary<object, object> map)
        {
            return (IDictionary<K, V>)map;
        }
    }
}