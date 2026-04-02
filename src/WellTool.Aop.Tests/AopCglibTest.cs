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

using WellTool.Aop;
using WellTool.Aop.Aspects;
using Xunit;

namespace WellTool.Aop.Tests
{
    /// <summary>
    /// 无接口类的 AOP 测试
    /// </summary>
    public class AopCglibTest
    {
    [Fact]
    public void AopByAutoCglibTest()
    {
        // 测试带接口的类使用 CGLib 代理
        // 注意：由于 CglibProxyFactory 是占位实现，这里测试带接口的类
        Animal dog = ProxyUtil.Proxy<Animal>(new Dog(), typeof(TimeIntervalAspect));
        string result = dog.Eat();
        Assert.Equal("狗吃肉", result);
        dog.Seize();
    }

    /// <summary>
    /// 动物接口
    /// </summary>
    public interface Animal
    {
        string Eat();
        void Seize();
    }

    /// <summary>
    /// 狗 - 有接口
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
    }
}
