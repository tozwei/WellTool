using System;

namespace WellTool.Core.Math
{
    public static class MathUtil
    {
        public static int Min(params int[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            int min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }
            return min;
        }

        public static long Min(params long[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            long min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }
            return min;
        }

        public static double Min(params double[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            double min = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                }
            }
            return min;
        }

        public static int Max(params int[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            int max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }
            return max;
        }

        public static long Max(params long[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            long max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }
            return max;
        }

        public static double Max(params double[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }

            double max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                }
            }
            return max;
        }

        public static double Round(double value, int digits)
        {
            return System.Math.Round(value, digits);
        }

        public static double Ceil(double value)
        {
            return System.Math.Ceiling(value);
        }

        public static double Floor(double value)
        {
            return System.Math.Floor(value);
        }

        public static double Abs(double value)
        {
            return System.Math.Abs(value);
        }

        public static int Abs(int value)
        {
            return System.Math.Abs(value);
        }

        public static long Abs(long value)
        {
            return System.Math.Abs(value);
        }

        public static double Pow(double x, double y)
        {
            return System.Math.Pow(x, y);
        }

        public static double Sqrt(double value)
        {
            return System.Math.Sqrt(value);
        }

        public static double Sin(double value)
        {
            return System.Math.Sin(value);
        }

        public static double Cos(double value)
        {
            return System.Math.Cos(value);
        }

        public static double Tan(double value)
        {
            return System.Math.Tan(value);
        }
    }
}