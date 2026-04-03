//using WellTool.Core.Text;

//namespace WellTool.Core.Text.Finder;

///// <summary>
///// 字符串查找器
///// </summary>
//public class StrFinder : TextFinder
//{
//    private readonly string _strToFind;
//    private readonly bool _caseInsensitive;

//    /// <summary>
//    /// 构造
//    /// </summary>
//    /// <param name="strToFind">被查找的字符串</param>
//    /// <param name="caseInsensitive">是否忽略大小写</param>
//    public StrFinder(string strToFind, bool caseInsensitive)
//    {
//        Assert.NotEmpty(strToFind);
//        _strToFind = strToFind;
//        _caseInsensitive = caseInsensitive;
//    }

//    public override int Start(int from)
//    {
//        Assert.NotNull(Text, "Text to find must be not null!");
//        int subLen = _strToFind.Length;

//        if (from < 0)
//        {
//            from = 0;
//        }
//        int endLimit = GetValidEndIndex();
//        if (Negative)
//        {
//            for (int i = from; i > endLimit; i--)
//            {
//                if (CharSequenceUtil.IsSubEquals(Text, i, _strToFind, 0, subLen, _caseInsensitive))
//                {
//                    return i;
//                }
//            }
//        }
//        else
//        {
//            endLimit = endLimit - subLen + 1;
//            for (int i = from; i < endLimit; i++)
//            {
//                if (CharSequenceUtil.IsSubEquals(Text, i, _strToFind, 0, subLen, _caseInsensitive))
//                {
//                    return i;
//                }
//            }
//        }

//        return INDEX_NOT_FOUND;
//    }

//    public override int End(int start)
//    {
//        if (start < 0)
//        {
//            return -1;
//        }
//        return start + _strToFind.Length;
//    }
//}
