using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 服务加载工具类，用于动态加载实现类
    /// </summary>
    public static class ServiceLoaderUtil
    {
        private static readonly Dictionary<Type, List<Type>> _cachedServices = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// 加载指定接口或抽象类的所有实现
        /// </summary>
        public static IEnumerable<Type> Load<T>()
        {
            return Load(typeof(T));
        }

        /// <summary>
        /// 加载指定接口或抽象类的所有实现
        /// </summary>
        public static IEnumerable<Type> Load(Type serviceType)
        {
            if (_cachedServices.TryGetValue(serviceType, out var cached))
            {
                return cached;
            }

            var types = new List<Type>();

            // 扫描所有程序集
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (serviceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            types.Add(type);
                        }
                    }
                }
                catch
                {
                    // 忽略无法加载的程序集
                }
            }

            _cachedServices[serviceType] = types;
            return types;
        }

        /// <summary>
        /// 加载指定接口或抽象类的第一个实现
        /// </summary>
        public static Type LoadFirst<T>()
        {
            return Load<T>().FirstOrDefault();
        }

        /// <summary>
        /// 加载指定接口或抽象类的第一个实现，如果没有则返回null
        /// </summary>
        public static Type LoadFirstOrDefault<T>()
        {
            return Load<T>().FirstOrDefault();
        }

        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        public static T NewInstance<T>() where T : class
        {
            var type = LoadFirstOrDefault<T>();
            if (type == null)
            {
                return null;
            }
            return Activator.CreateInstance(type) as T;
        }

        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        public static object NewInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 创建指定类型的实例，支持构造函数参数
        /// </summary>
        public static object NewInstance(Type type, params object[] args)
        {
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 加载并创建所有实现实例
        /// </summary>
        public static IEnumerable<T> LoadAndCreate<T>() where T : class
        {
            return Load<T>().Select(t => Activator.CreateInstance(t) as T).Where(t => t != null);
        }

        /// <summary>
        /// 按优先级加载（需要实现IPriority接口）
        /// </summary>
        public static IEnumerable<T> LoadByPriority<T>() where T : class
        {
            return LoadAndCreate<T>().OrderBy(t =>
            {
                if (t is IPriority p)
                {
                    return p.Priority;
                }
                return 0;
            });
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            _cachedServices.Clear();
        }

        /// <summary>
        /// 清空指定类型的缓存
        /// </summary>
        public static void ClearCache(Type serviceType)
        {
            _cachedServices.Remove(serviceType);
        }
    }

    /// <summary>
    /// 优先级接口
    /// </summary>
    public interface IPriority
    {
        /// <summary>
        /// 优先级，数值越小优先级越高
        /// </summary>
        int Priority { get; }
    }
}
