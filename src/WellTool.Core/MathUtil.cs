using WellTool.Core.Math;

namespace WellTool.Core
{
    /// <summary>
    /// MathUtil数学工具类（别名）
    /// </summary>
    public static class MathUtil
    {
        /// <summary>
        /// 圆周率
        /// </summary>
        public const double PI = WellTool.Core.Math.MathUtil.PI;

        /// <summary>
        /// 最小值
        /// </summary>
        public static T Min<T>(T a, T b) where T : System.IComparable<T>
        {
            return WellTool.Core.Math.MathUtil.Min(a, b);
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static T Max<T>(T a, T b) where T : System.IComparable<T>
        {
            return WellTool.Core.Math.MathUtil.Max(a, b);
        }

        /// <summary>
        /// 绝对值
        /// </summary>
        public static int Abs(int value)
        {
            return WellTool.Core.Math.MathUtil.Abs(value);
        }

        /// <summary>
        /// 绝对值
        /// </summary>
        public static long Abs(long value)
        {
            return WellTool.Core.Math.MathUtil.Abs(value);
        }

        /// <summary>
        /// 绝对值
        /// </summary>
        public static double Abs(double value)
        {
            return WellTool.Core.Math.MathUtil.Abs(value);
        }

        /// <summary>
        /// 幂运算
        /// </summary>
        public static double Pow(double x, double y)
        {
            return WellTool.Core.Math.MathUtil.Pow(x, y);
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        public static double Ceiling(double x)
        {
            return WellTool.Core.Math.MathUtil.Ceiling(x);
        }

        /// <summary>
        /// 对数
        /// </summary>
        public static double Log(double x)
        {
            return WellTool.Core.Math.MathUtil.Log(x);
        }

        /// <summary>
        /// 对数
        /// </summary>
        public static double Log(double x, double y)
        {
            return WellTool.Core.Math.MathUtil.Log(x, y);
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        public static decimal Mul(string a, string b)
        {
            return WellTool.Core.Math.MathUtil.Mul(a, b);
        }

        /// <summary>
        /// 将字符串转换为decimal
        /// </summary>
        public static decimal ToDecimal(string value)
        {
            return WellTool.Core.Math.MathUtil.ToDecimal(value);
        }

        /// <summary>
        /// 平方根
        /// </summary>
        public static double Sqrt(double x)
        {
            return WellTool.Core.Math.MathUtil.Sqrt(x);
        }
    }
}