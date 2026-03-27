using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Lang
{
    public static class Singleton
    {
        private static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();
        private static readonly object lockObj = new object();

        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            if (!instances.TryGetValue(type, out var instance))
            {
                lock (lockObj)
                {
                    if (!instances.TryGetValue(type, out instance))
                    {
                        instance = CreateInstance<T>();
                        instances[type] = instance;
                    }
                }
            }
            return (T)instance;
        }

        public static T Get<T>(Func<T> creator) where T : class
        {
            var type = typeof(T);
            if (!instances.TryGetValue(type, out var instance))
            {
                lock (lockObj)
                {
                    if (!instances.TryGetValue(type, out instance))
                    {
                        instance = creator();
                        instances[type] = instance;
                    }
                }
            }
            return (T)instance;
        }

        public static void Remove<T>() where T : class
        {
            var type = typeof(T);
            lock (lockObj)
            {
                instances.Remove(type);
            }
        }

        public static void Clear()
        {
            lock (lockObj)
            {
                instances.Clear();
            }
        }

        private static T CreateInstance<T>() where T : class
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (constructor == null)
            {
                throw new InvalidOperationException($"Type {type.Name} does not have a private parameterless constructor");
            }
            return (T)constructor.Invoke(null);
        }
    }
}