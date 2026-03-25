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
using WellTool.Aop.Proxy;

namespace WellTool.Aop
{
    /// <summary>
    /// 代理工具类
    /// </summary>
    public static class ProxyUtil
    {
        /// <summary>
        /// 使用切面代理对象
        /// </summary>
        /// <typeparam name="T">切面对象类型</typeparam>
        /// <param name="target">目标对象</param>
        /// <param name="aspectClass">切面对象类</param>
        /// <returns>代理对象</returns>
        public static T Proxy<T>(T target, Type aspectClass)
        {
            return ProxyFactory.CreateProxy(target, aspectClass);
        }

        /// <summary>
        /// 使用切面代理对象
        /// </summary>
        /// <typeparam name="T">被代理对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面对象</param>
        /// <returns>代理对象</returns>
        public static T Proxy<T>(T target, Aspect aspect)
        {
            return ProxyFactory.CreateProxy(target, aspect);
        }
    }
}