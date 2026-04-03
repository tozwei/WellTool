namespace WellTool.Core.Math
{
    public static class MathUtil
    {
        public static T Min<T>(T a, T b) where T : System.IComparable<T>
        {
            return a.CompareTo(b) < 0 ? a : b;
        }

        public static T Max<T>(T a, T b) where T : System.IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public static int Abs(int value)
        {
            return System.Math.Abs(value);
        }

        public static long Abs(long value)
        {
            return System.Math.Abs(value);
        }

        public static double Abs(double value)
        {
            return System.Math.Abs(value);
        }

        public static double Pow(double x, double y)
        {
            return System.Math.Pow(x, y);
        }
    }
}