using System;

namespace WellTool.Poi.Excel.Cell
{
    /// <summary>
    /// 单元格位置
    /// </summary>
    [Serializable]
    public class CellLocation
    {
        /// <summary>
        /// 列号，从0开始
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 行号，从0开始
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="x">列号，从0开始</param>
        /// <param name="y">行号，从0开始</param>
        public CellLocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 重写等于方法
        /// </summary>
        /// <param name="obj">比较对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var that = (CellLocation)obj;
            return X == that.X && Y == that.Y;
        }

        /// <summary>
        /// 重写哈希方法
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// 重写字符串方法
        /// </summary>
        /// <returns>字符串表示</returns>
        public override string ToString()
        {
            return $"CellLocation{{x={X}, y={Y}}}";
        }
    }
}