using System.Collections.Concurrent;
using System.Text;
using WellTool.Core.Lang.Hash;

namespace WellTool.Core.Text;

/// <summary>
/// Simhash是一种局部敏感hash，用于海量文本去重
/// </summary>
public class Simhash
{
    private readonly int _bitNum = 64;
    private readonly int _fracCount;
    private readonly int _fracBitNum;
    private readonly int _hammingThresh;
    private readonly ConcurrentDictionary<string, List<long>>[] _storage;
    private readonly object _lock = new object();

    /// <summary>
    /// 构造
    /// </summary>
    public Simhash() : this(4, 3)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="fracCount">存储段数</param>
    /// <param name="hammingThresh">汉明距离的衡量标准</param>
    public Simhash(int fracCount, int hammingThresh)
    {
        _fracCount = fracCount;
        _fracBitNum = _bitNum / fracCount;
        _hammingThresh = hammingThresh;
        _storage = new ConcurrentDictionary<string, List<long>>[fracCount];
        for (int i = 0; i < fracCount; i++)
        {
            _storage[i] = new ConcurrentDictionary<string, List<long>>();
        }
    }

    /// <summary>
    /// 指定文本计算simhash值
    /// </summary>
    /// <param name="segList">分词的词列表</param>
    /// <returns>Hash值</returns>
    public long Hash(IEnumerable<string> segList)
    {
        int[] weight = new int[_bitNum];
        foreach (var seg in segList)
        {
            long wordHash = MurmurHash.Hash64(seg);
            for (int i = 0; i < _bitNum; i++)
            {
                if (((wordHash >> i) & 1) == 1)
                    weight[i]++;
                else
                    weight[i]--;
            }
        }

        var sb = new StringBuilder();
        for (int i = 0; i < _bitNum; i++)
        {
            sb.Append(weight[i] > 0 ? '1' : '0');
        }

        return Convert.ToInt64(sb.ToString(), 2);
    }

    /// <summary>
    /// 判断文本是否与已存储的数据重复
    /// </summary>
    /// <param name="segList">文本分词后的结果</param>
    /// <returns>是否重复</returns>
    public bool Equals(IEnumerable<string> segList)
    {
        long simhash = Hash(segList);
        List<string> fracList = SplitSimhash(simhash);

        for (int i = 0; i < _fracCount; i++)
        {
            string frac = fracList[i];
            if (_storage[i].TryGetValue(frac, out var list))
            {
                foreach (long simhash2 in list)
                {
                    if (Hamming(simhash, simhash2) < _hammingThresh)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 存储simhash
    /// </summary>
    /// <param name="simhash">Simhash值</param>
    public void Store(long simhash)
    {
        List<string> lFrac = SplitSimhash(simhash);

        for (int i = 0; i < _fracCount; i++)
        {
            string frac = lFrac[i];
            if (_storage[i].TryGetValue(frac, out var list))
            {
                list.Add(simhash);
            }
            else
            {
                _storage[i][frac] = new List<long> { simhash };
            }
        }
    }

    /// <summary>
    /// 计算汉明距离
    /// </summary>
    private int Hamming(long s1, long s2)
    {
        int dis = 0;
        for (int i = 0; i < _bitNum; i++)
        {
            if (((s1 >> i) & 1) != ((s2 >> i) & 1))
                dis++;
        }
        return dis;
    }

    /// <summary>
    /// 将simhash分成n段
    /// </summary>
    private List<string> SplitSimhash(long simhash)
    {
        var ls = new List<string>();
        var sb = new StringBuilder();
        for (int i = 0; i < _bitNum; i++)
        {
            sb.Append((simhash >> i) & 1);
            if ((i + 1) % _fracBitNum == 0)
            {
                ls.Add(sb.ToString());
                sb.Clear();
            }
        }
        return ls;
    }
}
