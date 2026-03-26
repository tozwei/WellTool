using System;
using System.Collections.Generic;

namespace WellTool.Core.Builder
{
    /// <summary>
    /// 通用Builder
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <remarks>
    /// 使用方法如下：
    /// <code>
    /// Box box = GenericBuilder
    ///     .Of(() => new Box())
    ///     .With(box => box.SetId(1024L))
    ///     .With(box => box.SetTitle("Hello World!"))
    ///     .With(box => box.SetLength(9))
    ///     .With(box => box.SetWidth(8))
    ///     .With(box => box.SetHeight(7))
    ///     .Build();
    /// </code>
    /// 
    /// 我们也可以对已创建的对象进行修改：
    /// <code>
    /// Box boxModified = GenericBuilder
    ///     .Of(() => box)
    ///     .With(box => box.SetTitle("Hello Friend!"))
    ///     .With(box => box.SetLength(3))
    ///     .With(box => box.SetWidth(4))
    ///     .With(box => box.SetHeight(5))
    ///     .Build();
    /// </code>
    /// 
    /// 注意：本工具类支持调用的构造方法的参数数量不超过5个，一般方法的参数数量不超过2个，更多的参数不利于阅读和维护。
    /// </remarks>
    public class GenericBuilder<T> : Builder<T>
    {
        /// <summary>
        /// 实例化器
        /// </summary>
        private readonly Func<T> _instant;

        /// <summary>
        /// 修改器列表
        /// </summary>
        private readonly List<Action<T>> _modifiers = new List<Action<T>>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="instant">实例化器</param>
        public GenericBuilder(Func<T> instant)
        {
            _instant = instant;
        }

        /// <summary>
        /// 通过无参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T>(Func<T> instant)
        {
            return new GenericBuilder<T>(instant);
        }

        /// <summary>
        /// 通过1参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <param name="p1">参数一</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T, P1>(Func<P1, T> instant, P1 p1)
        {
            return Of(() => instant(p1));
        }

        /// <summary>
        /// 通过2参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <param name="p1">参数一</param>
        /// <param name="p2">参数二</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T, P1, P2>(Func<P1, P2, T> instant, P1 p1, P2 p2)
        {
            return Of(() => instant(p1, p2));
        }

        /// <summary>
        /// 通过3参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <param name="p1">参数一</param>
        /// <param name="p2">参数二</param>
        /// <param name="p3">参数三</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <typeparam name="P3">参数三类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T, P1, P2, P3>(Func<P1, P2, P3, T> instant, P1 p1, P2 p2, P3 p3)
        {
            return Of(() => instant(p1, p2, p3));
        }

        /// <summary>
        /// 通过4参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <param name="p1">参数一</param>
        /// <param name="p2">参数二</param>
        /// <param name="p3">参数三</param>
        /// <param name="p4">参数四</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <typeparam name="P3">参数三类型</typeparam>
        /// <typeparam name="P4">参数四类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T, P1, P2, P3, P4>(Func<P1, P2, P3, P4, T> instant, P1 p1, P2 p2, P3 p3, P4 p4)
        {
            return Of(() => instant(p1, p2, p3, p4));
        }

        /// <summary>
        /// 通过5参数实例化器创建GenericBuilder
        /// </summary>
        /// <param name="instant">实例化器</param>
        /// <param name="p1">参数一</param>
        /// <param name="p2">参数二</param>
        /// <param name="p3">参数三</param>
        /// <param name="p4">参数四</param>
        /// <param name="p5">参数五</param>
        /// <typeparam name="T">目标类型</typeparam>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <typeparam name="P3">参数三类型</typeparam>
        /// <typeparam name="P4">参数四类型</typeparam>
        /// <typeparam name="P5">参数五类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public static GenericBuilder<T> Of<T, P1, P2, P3, P4, P5>(Func<P1, P2, P3, P4, P5, T> instant, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return Of(() => instant(p1, p2, p3, p4, p5));
        }

        /// <summary>
        /// 调用无参数方法
        /// </summary>
        /// <param name="action">无参数Action</param>
        /// <returns>GenericBuilder对象</returns>
        public GenericBuilder<T> With(Action<T> action)
        {
            _modifiers.Add(action);
            return this;
        }

        /// <summary>
        /// 调用1参数方法
        /// </summary>
        /// <param name="action">1参数Action</param>
        /// <param name="p1">参数一</param>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public GenericBuilder<T> With<P1>(Action<T, P1> action, P1 p1)
        {
            _modifiers.Add(instant => action(instant, p1));
            return this;
        }

        /// <summary>
        /// 调用2参数方法
        /// </summary>
        /// <param name="action">2参数Action</param>
        /// <param name="p1">参数一</param>
        /// <param name="p2">参数二</param>
        /// <typeparam name="P1">参数一类型</typeparam>
        /// <typeparam name="P2">参数二类型</typeparam>
        /// <returns>GenericBuilder对象</returns>
        public GenericBuilder<T> With<P1, P2>(Action<T, P1, P2> action, P1 p1, P2 p2)
        {
            _modifiers.Add(instant => action(instant, p1, p2));
            return this;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <returns>目标对象</returns>
        public T Build()
        {
            T value = _instant();
            _modifiers.ForEach(modifier => modifier(value));
            _modifiers.Clear();
            return value;
        }
    }
}