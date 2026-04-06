using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Extra.Spring
{
    /// <summary>
    /// Spring工具封装
    /// </summary>
    public class SpringUtil
    {
        /// <summary>
        /// Spring应用上下文环境
        /// </summary>
        private static IApplicationContext _applicationContext;

        /// <summary>
        /// 设置应用上下文
        /// </summary>
        /// <param name="applicationContext">应用上下文</param>
        public static void SetApplicationContext(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        /// <summary>
        /// 获取应用上下文
        /// </summary>
        /// <returns>应用上下文</returns>
        public static IApplicationContext GetApplicationContext()
        {
            return _applicationContext;
        }

        /// <summary>
        /// 通过名称获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="name">Bean名称</param>
        /// <returns>Bean</returns>
        public static T GetBean<T>(string name)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBean<T>(name);
        }

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <returns>Bean</returns>
        public static T GetBean<T>()
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBean<T>();
        }

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="args">创建bean需要的参数</param>
        /// <returns>Bean</returns>
        public static T GetBean<T>(params object[] args)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBean<T>(args);
        }

        /// <summary>
        /// 通过名称和类型获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="name">Bean名称</param>
        /// <returns>Bean</returns>
        public static T GetBean<T>(string name, Type type)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return (T)_applicationContext.GetBean(name, type);
        }

        /// <summary>
        /// 获取指定类型对应的所有Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <returns>类型对应的bean，key是bean名称，value是Bean</returns>
        public static IDictionary<string, T> GetBeansOfType<T>()
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBeansOfType<T>();
        }

        /// <summary>
        /// 获取指定类型对应的所有Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <returns>类型对应的bean</returns>
        public static T[] GetBeans<T>() where T : class
        {
            if (_applicationContext == null)
            {
                return Array.Empty<T>();
            }
            var beans = _applicationContext.GetBeansOfType<T>();
            return beans.Values.ToArray();
        }

        /// <summary>
        /// 检查是否包含指定名称的Bean
        /// </summary>
        /// <param name="name">Bean名称</param>
        /// <returns>是否包含</returns>
        public static bool ContainsBean(string name)
        {
            if (_applicationContext == null)
            {
                return false;
            }
            var beanNames = _applicationContext.GetBeanNamesForType(typeof(object));
            return beanNames.Contains(name);
        }

        /// <summary>
        /// 获取所有Bean名称
        /// </summary>
        /// <returns>Bean名称数组</returns>
        public static string[] GetBeanNames()
        {
            if (_applicationContext == null)
            {
                return Array.Empty<string>();
            }
            return _applicationContext.GetBeanNamesForType(typeof(object));
        }

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <param name="type">Bean类型</param>
        /// <returns>Bean</returns>
        public static object GetBean(Type type)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBean(type);
        }

        /// <summary>
        /// 设置对象属性值
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        public static void SetProperty(object obj, string name, object value)
        {
            if (obj == null) return;
            var type = obj.GetType();
            var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(obj, value);
            }
        }

        /// <summary>
        /// 调用对象方法
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="methodName">方法名称</param>
        /// <param name="args">方法参数</param>
        /// <returns>方法返回值</returns>
        public static object InvokeMethod(object obj, string methodName, params object[] args)
        {
            if (obj == null) return null;
            var type = obj.GetType();
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                return method.Invoke(obj, args);
            }
            return null;
        }

        /// <summary>
        /// 检查是否在Spring环境中
        /// </summary>
        /// <returns>是否在Spring环境中</returns>
        public static bool IsInSpring()
        {
            return _applicationContext != null;
        }

        /// <summary>
        /// 获取指定类型对应的Bean名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>Bean名称数组</returns>
        public static string[] GetBeanNamesForType(Type type)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            return _applicationContext.GetBeanNamesForType(type);
        }

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <param name="key">配置项key</param>
        /// <returns>属性值</returns>
        public static string GetProperty(string key)
        {
            if (_applicationContext == null)
            {
                return null;
            }
            return _applicationContext.GetProperty(key);
        }

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <param name="key">配置项key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        public static string GetProperty(string key, string defaultValue)
        {
            if (_applicationContext == null)
            {
                return null;
            }
            return _applicationContext.GetProperty(key, defaultValue);
        }

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="key">配置项key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        public static T GetProperty<T>(string key, T defaultValue)
        {
            if (_applicationContext == null)
            {
                return default(T);
            }
            return _applicationContext.GetProperty<T>(key, defaultValue);
        }

        /// <summary>
        /// 获取应用程序名称
        /// </summary>
        /// <returns>应用程序名称</returns>
        public static string GetApplicationName()
        {
            return GetProperty("spring.application.name");
        }

        /// <summary>
        /// 获取当前的环境配置
        /// </summary>
        /// <returns>当前的环境配置</returns>
        public static string[] GetActiveProfiles()
        {
            if (_applicationContext == null)
            {
                return null;
            }
            return _applicationContext.GetActiveProfiles();
        }

        /// <summary>
        /// 获取当前的环境配置，当有多个环境配置时，只获取第一个
        /// </summary>
        /// <returns>当前的环境配置</returns>
        public static string GetActiveProfile()
        {
            string[] activeProfiles = GetActiveProfiles();
            return activeProfiles != null && activeProfiles.Length > 0 ? activeProfiles[0] : null;
        }

        /// <summary>
        /// 动态向Spring注册Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="beanName">Bean名称</param>
        /// <param name="bean">Bean实例</param>
        public static void RegisterBean<T>(string beanName, T bean)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            _applicationContext.RegisterBean(beanName, bean);
        }

        /// <summary>
        /// 注销Bean
        /// </summary>
        /// <param name="beanName">Bean名称</param>
        public static void UnregisterBean(string beanName)
        {
            if (_applicationContext == null)
            {
                throw new Exception("No ApplicationContext injected, maybe not in the Spring environment?");
            }
            _applicationContext.UnregisterBean(beanName);
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件对象</param>
        public static void PublishEvent(object @event)
        {
            if (_applicationContext != null)
            {
                _applicationContext.PublishEvent(@event);
            }
        }
    }

    /// <summary>
    /// 应用上下文接口
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 通过名称获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="name">Bean名称</param>
        /// <returns>Bean</returns>
        T GetBean<T>(string name);

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <returns>Bean</returns>
        T GetBean<T>();

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="args">创建bean需要的参数</param>
        /// <returns>Bean</returns>
        T GetBean<T>(params object[] args);

        /// <summary>
        /// 通过名称和类型获取Bean
        /// </summary>
        /// <param name="name">Bean名称</param>
        /// <param name="type">Bean类型</param>
        /// <returns>Bean</returns>
        object GetBean(string name, Type type);

        /// <summary>
        /// 通过类型获取Bean
        /// </summary>
        /// <param name="type">Bean类型</param>
        /// <returns>Bean</returns>
        object GetBean(Type type);

        /// <summary>
        /// 获取指定类型对应的所有Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <returns>类型对应的bean，key是bean名称，value是Bean</returns>
        IDictionary<string, T> GetBeansOfType<T>();

        /// <summary>
        /// 获取指定类型对应的Bean名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>Bean名称数组</returns>
        string[] GetBeanNamesForType(Type type);

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <param name="key">配置项key</param>
        /// <returns>属性值</returns>
        string GetProperty(string key);

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <param name="key">配置项key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        string GetProperty(string key, string defaultValue);

        /// <summary>
        /// 获取配置文件配置项的值
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="key">配置项key</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性值</returns>
        T GetProperty<T>(string key, T defaultValue);

        /// <summary>
        /// 获取当前的环境配置
        /// </summary>
        /// <returns>当前的环境配置</returns>
        string[] GetActiveProfiles();

        /// <summary>
        /// 动态向Spring注册Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="beanName">Bean名称</param>
        /// <param name="bean">Bean实例</param>
        void RegisterBean<T>(string beanName, T bean);

        /// <summary>
        /// 注销Bean
        /// </summary>
        /// <param name="beanName">Bean名称</param>
        void UnregisterBean(string beanName);

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件对象</param>
        void PublishEvent(object @event);
    }
}