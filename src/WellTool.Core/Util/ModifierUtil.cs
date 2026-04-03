using System;
using System.Reflection;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 修饰符工具类
    /// </summary>
    public static class ModifierUtil
    {
        /// <summary>
        /// 检查是否为public修饰符
        /// </summary>
        public static bool IsPublic(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Public) != 0;
        }

        /// <summary>
        /// 检查是否为private修饰符
        /// </summary>
        public static bool IsPrivate(int modifiers)
        {
            return (modifiers & (int)BindingFlags.NonPublic) != 0;
        }

        /// <summary>
        /// 检查是否为static修饰符
        /// </summary>
        public static bool IsStatic(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Static) != 0;
        }

        /// <summary>
        /// 检查是否为final修饰符
        /// </summary>
        public static bool IsFinal(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Final) != 0;
        }

        /// <summary>
        /// 检查是否为abstract修饰符
        /// </summary>
        public static bool IsAbstract(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Abstract) != 0;
        }

        /// <summary>
        /// 检查是否为virtual修饰符
        /// </summary>
        public static bool IsVirtual(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Virtual) != 0;
        }

        /// <summary>
        /// 检查是否为override修饰符
        /// </summary>
        public static bool IsOverride(int modifiers)
        {
            return (modifiers & (int)BindingFlags.Repeatable) != 0;
        }

        /// <summary>
        /// 获取修饰符字符串
        /// </summary>
        public static string ToString(int modifiers)
        {
            var parts = new System.Collections.Generic.List<string>();

            if (IsStatic(modifiers))
            {
                parts.Add("static");
            }

            if (IsFinal(modifiers))
            {
                parts.Add("final");
            }

            if (IsAbstract(modifiers))
            {
                parts.Add("abstract");
            }

            if (IsPublic(modifiers))
            {
                parts.Add("public");
            }
            else if (IsPrivate(modifiers))
            {
                parts.Add("private");
            }
            else
            {
                parts.Add("protected");
            }

            if (IsVirtual(modifiers))
            {
                parts.Add("virtual");
            }

            if (IsOverride(modifiers))
            {
                parts.Add("override");
            }

            return string.Join(" ", parts);
        }
    }
}
