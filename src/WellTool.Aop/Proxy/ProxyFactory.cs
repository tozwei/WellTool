// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using WellTool.Aop.Aspects;

namespace WellTool.Aop.Proxy
{
    /// <summary>
    /// 代理工厂
    /// 根据用户引入代理库的不同，产生不同的代理对象
    /// </summary>
    public abstract class ProxyFactory
    {
        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T">代理对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspectClass">切面实现类，自动实例化</param>
        /// <returns>代理对象</returns>
        public T Proxy<T>(T target, Type aspectClass)
        {
            if (!typeof(Aspect).IsAssignableFrom(aspectClass))
            {
                throw new ArgumentException($"{aspectClass.FullName} 必须实现 {typeof(Aspect).FullName} 接口");
            }

            var aspect = Activator.CreateInstance(aspectClass) as Aspect;
            if (aspect == null)
            {
                throw new ArgumentException($"无法实例化 {aspectClass.FullName}");
            }

            return Proxy(target, aspect);
        }

        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T">代理对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面实现</param>
        /// <returns>代理对象</returns>
        public abstract T Proxy<T>(T target, Aspect aspect);

        /// <summary>
        /// 根据用户引入Cglib与否自动创建代理对象
        /// </summary>
        /// <typeparam name="T">切面对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="aspectClass">切面对象类</param>
        /// <returns>代理对象</returns>
        public static T CreateProxy<T>(T target, Type aspectClass)
        {
            if (!typeof(Aspect).IsAssignableFrom(aspectClass))
            {
                throw new ArgumentException($"{aspectClass.FullName} 必须实现 {typeof(Aspect).FullName} 接口");
            }

            var aspect = Activator.CreateInstance(aspectClass) as Aspect;
            if (aspect == null)
            {
                throw new ArgumentException($"无法实例化 {aspectClass.FullName}");
            }

            return CreateProxy(target, aspect);
        }

        /// <summary>
        /// 根据用户引入Cglib与否自动创建代理对象
        /// </summary>
        /// <typeparam name="T">切面对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面实现</param>
        /// <returns>代理对象</returns>
        public static T CreateProxy<T>(T target, Aspect aspect)
        {
            var factory = Create();
            if (factory == null)
            {
                // 默认为 JDK 代理
                factory = JdkProxyFactory.Instance;
            }
            return factory.Proxy(target, aspect);
        }

        /// <summary>
        /// 根据用户引入Cglib与否创建代理工厂
        /// </summary>
        /// <returns>代理工厂</returns>
        public static ProxyFactory Create()
        {
            // 尝试加载 CglibProxyFactory
            try
            {
                // 检查是否有 Cglib 相关的实现
                var cglibFactory = typeof(CglibProxyFactory);
                return (ProxyFactory)Activator.CreateInstance(cglibFactory);
            }
            catch
            {
                // 如果没有 Cglib 实现，返回 JdkProxyFactory
                return JdkProxyFactory.Instance;
            }
        }
    }
}