using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Bean.Copier
{
    /// <summary>
    /// Bean值提供者
    /// </summary>
    public class BeanValueProvider : IValueProvider<object>
    {
        private readonly object _bean;
        private readonly BeanDesc _beanDesc;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bean">Bean对象</param>
        public BeanValueProvider(object bean)
        {
            _bean = bean;
            _beanDesc = BeanDesc.GetBeanDesc(bean.GetType());
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">Bean对象中参数名</param>
        /// <param name="valueType">被注入的值的类型</param>
        /// <returns>对应参数名的值</returns>
        public object Value(object key, Type valueType)
        {
            var name = key as string;
            if (name == null)
            {
                return null;
            }
            
            var prop = _beanDesc.GetProp(name);
            if (prop != null)
            {
                return prop.GetValue(_bean);
            }
            return null;
        }

        /// <summary>
        /// 是否包含指定属性
        /// </summary>
        /// <param name="key">Bean对象中参数名</param>
        /// <returns>是否包含指定KEY</returns>
        public bool ContainsKey(object key)
        {
            var name = key as string;
            if (name == null)
            {
                return false;
            }
            
            return _beanDesc.GetProp(name) != null;
        }
    }
}
