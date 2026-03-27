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
using WellTool.Aop.Interceptor;

namespace WellTool.Aop.Proxy
{
    /// <summary>
    /// Cglib代理工厂
    /// </summary>
    public class CglibProxyFactory : ProxyFactory
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static readonly CglibProxyFactory Instance = new CglibProxyFactory();

        /// <summary>
        /// 创建代理
        /// </summary>
        /// <typeparam name="T">代理对象类型</typeparam>
        /// <param name="target">被代理对象</param>
        /// <param name="aspect">切面实现</param>
        /// <returns>代理对象</returns>
        public override T Proxy<T>(T target, Aspect aspect)
        {
            // 注意：C#中没有原生的Cglib库，这里使用占位实现
            // 实际项目中可以使用Castle DynamicProxy等库来实现类似功能
            // 暂时返回目标对象本身
            return target;
        }
    }
}