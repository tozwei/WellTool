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

using System;
using WellTool.Aop;
using WellTool.Aop.Aspects;
using WellTool.Aop.Proxy;
using Xunit;

namespace WellTool.Aop.Tests
{
    /// <summary>
    /// Issue I74EX7 测试
    /// Enhancer.create()默认调用无参构造，有参构造或者多个构造没有很好的兼容。
    /// </summary>
    public class IssueI74EX7Test
    {
        [Fact]
        public void ProxyTest()
        {
            // 测试 JdkProxyFactory
            var smsBlend = new SmsBlendImpl(1);
            var factory = new JdkProxyFactory();
            var proxy = factory.Proxy<SmsBlend>(smsBlend, new SimpleAspect());
            Assert.NotNull(proxy);
        }

        [Fact]
        public void CglibProxyTest()
        {
            // 测试 CglibProxyFactory
            var smsBlend = new SmsBlendImpl(1);
            var factory = new CglibProxyFactory();
            var proxy = factory.Proxy<SmsBlend>(smsBlend, new SimpleAspect());
            Assert.NotNull(proxy);
        }

        [Fact]
        public void SpringCglibProxyTest()
        {
            // 测试 SpringCglibProxyFactory 有参构造函数
            var smsBlend = new SmsBlendImpl(1);
            var factory = new SpringCglibProxyFactory();
            var proxy = factory.Proxy<SmsBlend>(smsBlend, new SimpleAspect());
            Assert.NotNull(proxy);
        }

        [Fact]
        public void SpringCglibProxyWithoutConstructorTest()
        {
            // 测试 SpringCglibProxyFactory 无参构造函数
            var smsBlend = new SmsBlendImplWithoutConstructor();
            var factory = new SpringCglibProxyFactory();
            var proxy = factory.Proxy<SmsBlendImplWithoutConstructor>(smsBlend, new SimpleAspect());
            Assert.NotNull(proxy);
        }

        public interface SmsBlend
        {
            void Send();
        }

        public class SmsBlendImpl : SmsBlend
        {
            private readonly int _status;

            public SmsBlendImpl(int status)
            {
                _status = status;
            }

            public void Send()
            {
                Console.WriteLine($"sms send.{_status}");
            }
        }

        public class SmsBlendImplWithoutConstructor : SmsBlend
        {
            public int Status { get; set; }

            public void Send()
            {
                Console.WriteLine($"sms send.{Status}");
            }
        }
    }
}
