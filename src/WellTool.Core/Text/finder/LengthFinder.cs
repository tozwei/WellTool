//namespace WellTool.Core.Text.Finder;

///// <summary>
///// 固定长度查找器，给定一个长度，查找的位置为from + length，一般用于分段截取
///// </summary>
//public class LengthFinder : TextFinder
//{
//    private readonly int _length;

//    /// <summary>
//    /// 构造
//    /// </summary>
//    /// <param name="length">长度</param>
//    public LengthFinder(int length)
//    {
//        Assert.IsTrue(length > 0, "Length must be great than 0");
//        _length = length;
//    }

//    public override int Start(int from)
//    {
//        Assert.NotNull(Text, "Text to find must be not null!");
//        int limit = GetValidEndIndex();
//        int result;
//        if (Negative)
//        {
//            result = from - _length;
//            if (result > limit)
//            {
//                return result;
//            }
//        }
//        else
//        {
//            result = from + _length;
//            if (result < limit)
//            {
//                return result;
//            }
//        }
//        return -1;
//    }

//    public override int End(int start)
//    {
//        return start;
//    }
//}
