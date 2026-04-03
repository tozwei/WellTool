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
        public object GetValue(string name)
        {
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
        public bool ContainsKey(string name)
        {
            return _beanDesc.GetProp(name) != null;
        }
    }
}
