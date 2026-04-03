namespace WellTool.Core.Math;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

/// <summary>
/// 组合，即 C(n, m)
/// 排列组合相关类
/// </summary>
public class Combination
{
    private static readonly long SerialVersionUID = 1L;

    private readonly string[] _datas;

    /// <summary>
    /// 组合，即 C(n, m)
    /// </summary>
    /// <param name="datas">用于组合的数据</param>
    public Combination(string[] datas)
    {
        _datas = datas;
    }

    /// <summary>
    /// 计算组合数，即 C(n, m) = n!/((n-m)! * m!)
    /// </summary>
    /// <param name="n">总数</param>
    /// <param name="m">选择的个数</param>
    /// <returns>组合数</returns>
    [Obsolete("Use CountBig instead for accurate results")]
    public static long Count(int n, int m)
    {
        BigInteger big = CountBig(n, m);
        return (long)big;
    }

    /// <summary>
    /// 计算组合数 C(n, m) 的 BigInteger 精确版本
    /// </summary>
    /// <param name="n">总数 n（必须大于等于0）</param>
    /// <param name="m">取出 m（必须大于等于0）</param>
    /// <returns>C(n, m) 的精确值</returns>
    public static BigInteger CountBig(int n, int m)
    {
        if (n < 0 || m < 0)
        {
            throw new ArgumentException($"n and m must be non-negative. got n={n}, m={m}");
        }
        if (m > n)
        {
            return BigInteger.Zero;
        }
        if (m == 0 || n == m)
        {
            return BigInteger.One;
        }
        m = Math.Min(m, n - m);
        BigInteger result = BigInteger.One;

        for (int i = 1; i <= m; i++)
        {
            int numerator = n - m + i;
            result = result * numerator / i;
        }

        return result;
    }

    /// <summary>
    /// 安全组合数 long 版本
    /// </summary>
    /// <param name="n">总数</param>
    /// <param name="m">取出数</param>
    /// <returns>组合数</returns>
    public static long CountSafe(int n, int m)
    {
        BigInteger big = CountBig(n, m);
        return (long)big;
    }

    /// <summary>
    /// 计算组合总数，即 C(n, 1) + C(n, 2) + C(n, 3)...
    /// </summary>
    /// <param name="n">总数</param>
    /// <returns>组合数</returns>
    public static long CountAll(int n)
    {
        if (n < 0 || n > 63)
        {
            throw new ArgumentException($"countAll must have n >= 0 and n <= 63, but got n={n}");
        }
        return n == 63 ? long.MaxValue : (1L << n) - 1;
    }

    /// <summary>
    /// 组合选择（从列表中选择m个组合）
    /// </summary>
    /// <param name="m">选择个数</param>
    /// <returns>组合结果</returns>
    public List<string[]> Select(int m)
    {
        List<string[]> result = new List<string[]>((int)Count(_datas.Length, m));
        Select(0, new string[m], 0, result);
        return result;
    }

    /// <summary>
    /// 全组合
    /// </summary>
    /// <returns>全排列结果</returns>
    public List<string[]> SelectAll()
    {
        List<string[]> result = new List<string[]>((int)CountAll(_datas.Length));
        for (int i = 1; i <= _datas.Length; i++)
        {
            result.AddRange(Select(i));
        }
        return result;
    }

    /// <summary>
    /// 组合选择
    /// </summary>
    private void Select(int dataIndex, string[] resultList, int resultIndex, List<string[]> result)
    {
        int resultLen = resultList.Length;
        int resultCount = resultIndex + 1;
        if (resultCount > resultLen)
        {
            result.Add(resultList.ToArray());
            return;
        }

        for (int i = dataIndex; i < _datas.Length + resultCount - resultLen; i++)
        {
            resultList[resultIndex] = _datas[i];
            Select(i + 1, resultList, resultIndex + 1, result);
        }
    }
}
