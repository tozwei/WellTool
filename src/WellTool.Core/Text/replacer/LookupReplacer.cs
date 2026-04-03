//using System.Collections.Generic;
//using WellTool.Core.Text;

//namespace WellTool.Core.Text.Replacer;

///// <summary>
///// 查找替换器，通过查找指定关键字，替换对应的值
///// </summary>
//public class LookupReplacer : StrReplacer
//{
//    private readonly Dictionary<string, string> _lookupMap;
//    private readonly HashSet<char> _prefixSet;
//    private readonly int _minLength;
//    private readonly int _maxLength;

//    /// <summary>
//    /// 构造
//    /// </summary>
//    /// <param name="lookup">被查找的键值对</param>
//    public LookupReplacer(params string[][] lookup)
//    {
//        _lookupMap = new Dictionary<string, string>();
//        _prefixSet = new HashSet<char>();

//        int minLength = int.MaxValue;
//        int maxLength = 0;
//        string key;
//        int keySize;
//        foreach (var pair in lookup)
//        {
//            key = pair[0];
//            _lookupMap[key] = pair[1];
//            _prefixSet.Add(key[0]);
//            keySize = key.Length;
//            if (keySize > maxLength)
//            {
//                maxLength = keySize;
//            }
//            if (keySize < minLength)
//            {
//                minLength = keySize;
//            }
//        }
//        _maxLength = maxLength;
//        _minLength = minLength;
//    }

//    protected override int Replace(string str, int pos, StrBuilder outBuilder)
//    {
//        if (_prefixSet.Contains(str[pos]))
//        {
//            int max = _maxLength;
//            if (pos + _maxLength > str.Length)
//            {
//                max = str.Length - pos;
//            }
//            string subSeq;
//            string result;
//            for (int i = max; i >= _minLength; i--)
//            {
//                subSeq = str.Substring(pos, i);
//                if (_lookupMap.TryGetValue(subSeq, out result))
//                {
//                    outBuilder.Append(result);
//                    return i;
//                }
//            }
//        }
//        return 0;
//    }
//}
