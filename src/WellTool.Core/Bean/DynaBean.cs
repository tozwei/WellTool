using System;
using System.Collections;
using System.Collections.Generic;
using WellTool.Core.Clone;
using WellTool.Core.Lang;
using WellTool.Core.Util;

namespace WellTool.Core.Bean
{
    /// <summary>
    /// 动态Bean，通过反射对Bean的相关方法做操作
    /// 支持字典和普通对象
    /// </summary>
    public class DynaBean : CloneSupport<DynaBean>, IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Type _beanClass;
        private readonly object _bean;
        private readonly bool _isMap;

        /// <summary>
        /// 创建DynaBean
        /// </summary>
        /// <param name="bean">普通对象</param>
        public static DynaBean Create(object bean)
        {
            return new DynaBean(bean);
        }

        /// <summary>
        /// 创建DynaBean
        /// </summary>
        /// <param name="beanClass">对象类型</param>
        public static DynaBean Create(Type beanClass)
        {
            return new DynaBean(beanClass);
        }

        /// <summary>
        /// 创建DynaBean
        /// </summary>
        /// <param name="beanClass">对象类型</param>
        /// <param name="args">构造函数参数</param>
        public static DynaBean Create(Type beanClass, params object[] args)
        {
            return new DynaBean(beanClass, args);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="beanClass">对象类型</param>
        /// <param name="args">构造函数参数</param>
        public DynaBean(Type beanClass, params object[] args)
        {
            _bean = ReflectUtil.CreateInstance(beanClass, args);
            _beanClass = _bean.GetType();
            _isMap = typeof(IDictionary).IsAssignableFrom(_beanClass);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bean">原始对象</param>
        public DynaBean(object bean)
        {
            if (bean == null)
            {
                throw new ArgumentNullException(nameof(bean));
            }

            if (bean is DynaBean dynaBean)
            {
                bean = dynaBean._bean;
            }

            _bean = bean;
            _beanClass = _bean.GetType();
            _isMap = typeof(IDictionary).IsAssignableFrom(_beanClass);
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="fieldName">字段名</param>
        public T Get<T>(string fieldName)
        {
            if (_isMap)
            {
                var dict = (IDictionary)_bean;
                if (dict.Contains(fieldName))
                {
                    return (T)dict[fieldName];
                }
                return default;
            }
            else
            {
                var beanDesc = BeanDesc.GetBeanDesc(_beanClass);
                var prop = beanDesc?.GetPropDesc(fieldName);
                if (prop == null)
                {
                    throw new BeanException($"No public field or get method for '{fieldName}'");
                }
                return (T)prop.GetValue(_bean);
            }
        }

        /// <summary>
        /// 获取字段值，安全版本
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="fieldName">字段名</param>
        public T SafeGet<T>(string fieldName)
        {
            try
            {
                return Get<T>(fieldName);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        public void Set(string fieldName, object value)
        {
            if (_isMap)
            {
                ((IDictionary)_bean)[fieldName] = value;
            }
            else
            {
                var beanDesc = BeanDesc.GetBeanDesc(_beanClass);
                var prop = beanDesc?.GetPropDesc(fieldName);
                if (prop == null)
                {
                    throw new BeanException($"No public field or set method for '{fieldName}'");
                }
                prop.SetValue(_bean, value);
            }
        }

        /// <summary>
        /// 检查是否有指定属性
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public bool ContainsProp(string fieldName)
        {
            if (_isMap)
            {
                return ((IDictionary)_bean).Contains(fieldName);
            }
            else
            {
                var beanDesc = BeanDesc.GetBeanDesc(_beanClass);
                return beanDesc?.GetPropDesc(fieldName) != null;
            }
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        public object Invoke(string methodName, params object[] args)
        {
            return ReflectUtil.Invoke(_bean, methodName, args);
        }

        /// <summary>
        /// 获取原始对象
        /// </summary>
        public T GetBean<T>()
        {
            return (T)_bean;
        }

        /// <summary>
        /// 获取对象类型
        /// </summary>
        public Type GetBeanClass()
        {
            return _beanClass;
        }

        /// <summary>
        /// 获取原始对象
        /// </summary>
        public object GetOriginBean()
        {
            return _bean;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null) return false;
            if (obj is DynaBean other)
            {
                return _bean?.Equals(other._bean) ?? other._bean == null;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _bean?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _bean?.ToString() ?? string.Empty;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if (_isMap)
            {
                foreach (var key in (IDictionary)_bean)
                {
                    yield return new KeyValuePair<string, object>(key.ToString(), ((IDictionary)_bean)[key]);
                }
            }
            else
            {
                var beanDesc = BeanDesc.GetBeanDesc(_beanClass);
                foreach (var prop in beanDesc.GetProps())
                {
                    yield return new KeyValuePair<string, object>(prop.Key, prop.Value.GetValue(_bean));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
