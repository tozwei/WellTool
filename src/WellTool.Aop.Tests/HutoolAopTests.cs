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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WellTool.Aop;
using WellTool.Aop.Aspects;
using WellTool.Aop.Proxy;
using Xunit;

namespace WellTool.Aop.Tests
{
    public class HutoolAopTests
    {
        [Fact]
        public void AopTest()
        {
            // 测试有接口的类
            Animal cat = ProxyUtil.Proxy<Animal>(new Cat(), typeof(TimeIntervalAspect));
            string result = cat.Eat();
            Assert.Equal("猫吃鱼", result);
            cat.Seize();
        }

        [Fact]
        public void TestJdkProxyFactory()
        {
            // 测试有接口的类
            Animal dog = ProxyUtil.Proxy<Animal>(new Dog(), typeof(TimeIntervalAspect));
            string result = dog.Eat();
            Assert.Equal("狗吃肉", result);
            dog.Seize();
        }

        [Fact]
        public void TestTagObjProxy()
        {
            TagObj target = new TagObj();
            // 目标类设置标记
            target.Tag = "tag";

            // 注意：TagObj 没有实现接口，所以不能使用 JdkProxyFactory
            // 这里我们创建一个接口来测试
            ITagObj proxy = ProxyUtil.Proxy<ITagObj>(target, typeof(TimeIntervalAspect));
            // 代理类获取标记tag
            Assert.Equal("tag", proxy.Tag);
        }

        [Fact]
        public void TestProxyFactoryWithConstructor()
        {
            // 测试有参构造函数的类
            SmsBlend smsBlend = new SmsBlendImpl(1);
            var factory = new JdkProxyFactory();
            var proxy = factory.Proxy<SmsBlend>(smsBlend, new SimpleAspect());
            Assert.NotNull(proxy);
        }

        [Fact]
        public async Task TestLoadFirstAvailableConcurrent()
        {
            // 创建多个任务并发测试
            int taskCount = 1000;
            var tasks = new List<Task>();
            int successCount = 0;

            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    ProxyFactory factory = ProxyFactory.Create();
                    if (factory != null)
                    {
                        Interlocked.Increment(ref successCount);
                    }
                }));
            }

            // 等待所有任务完成
            await Task.WhenAll(tasks);

            // 验证所有任务都成功加载了代理工厂
            Assert.Equal(taskCount, successCount);
        }

        #region 测试接口和类

        public interface Animal
        {
            string Eat();
            void Seize();
        }

        /// <summary>
        /// 有接口
        /// </summary>
        public class Cat : Animal
        {
            public string Eat()
            {
                return "猫吃鱼";
            }

            public void Seize()
            {
                Console.WriteLine("抓了条鱼");
            }
        }

        /// <summary>
        /// 有接口
        /// </summary>
        public class Dog : Animal
        {
            public string Eat()
            {
                return "狗吃肉";
            }

            public void Seize()
            {
                Console.WriteLine("抓了只鸡");
            }
        }

        public interface ITagObj
        {
            string Tag { get; set; }
        }

        public class TagObj : ITagObj
        {
            public string Tag { get; set; }
        }

        public interface SmsBlend
        {
            void Send();
        }

        public class SmsBlendImpl : SmsBlend
        {
            private readonly int status;

            public SmsBlendImpl(int status)
            {
                this.status = status;
            }

            public void Send()
            {
                Console.WriteLine($"sms send.{status}");
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

        #endregion
    }
}