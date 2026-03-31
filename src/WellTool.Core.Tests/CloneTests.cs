using System;
using System.Collections.Generic;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Clone;

namespace WellTool.Core.Tests
{
    public class CloneTests
    {
        [Fact]
        public void CloneTest()
        {
            //实现Cloneable接口
            var cat = new Cat();
            var cat2 = (Cat)cat.Clone();
            XAssert.Equal(cat, cat2);
        }

        [Fact]
        public void CloneTest2()
        {
            //继承CloneSupport类
            var dog = new Dog();
            var dog2 = dog.Clone();
            XAssert.Equal(dog, dog2);
        }

        [Fact]
        public void Clone0Test()
        {
            var oldCar = new Car();
            oldCar.Id = 1;
            oldCar.WheelList = new List<Wheel> { new Wheel("h") };

            var newCar = oldCar.Clone();
            XAssert.Equal(oldCar.Id, newCar.Id);
            XAssert.Equal(oldCar.WheelList, newCar.WheelList);

            newCar.Id = 2;
            XAssert.NotEqual(oldCar.Id, newCar.Id);
            newCar.WheelList.Add(new Wheel("s"));

            XAssert.NotSame(oldCar, newCar);
        }

        //------------------------------------------------------------------------------- private Class for test
        /**
         * 猫猫类，使用实现Cloneable方式
         */
        private class Cat : ICloneable
        {
            public string Name { get; set; } = "miaomiao";
            public int Age { get; set; } = 2;

            public object Clone()
            {
                return MemberwiseClone();
            }

            public override bool Equals(object obj)
            {
                if (obj is Cat other)
                {
                    return Name == other.Name && Age == other.Age;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }

        /**
         * 狗狗类，用于继承CloneSupport类
         */
        private class Dog : CloneSupport<Dog>
        {
            public string Name { get; set; } = "wangwang";
            public int Age { get; set; } = 3;

            public override bool Equals(object obj)
            {
                if (obj is Dog other)
                {
                    return Name == other.Name && Age == other.Age;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name, Age);
            }
        }

        private class Car : DefaultCloneable<Car>
        {
            public int? Id { get; set; }
            public List<Wheel> WheelList { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is Car other)
                {
                    return Id == other.Id && Equals(WheelList, other.WheelList);
                }
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, WheelList);
            }
        }

        private class Wheel
        {
            public string Direction { get; set; }

            public Wheel(string direction)
            {
                Direction = direction;
            }

            public override bool Equals(object obj)
            {
                if (obj is Wheel other)
                {
                    return Direction == other.Direction;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Direction?.GetHashCode() ?? 0;
            }
        }
    }
}