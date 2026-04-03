namespace WellTool.Core.Math;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 排列 A(n, m)
/// 排列组合相关类
/// </summary>
public class Arrangement
{
    private static readonly long SerialVersionUID = 1L;

    private readonly string[] _datas;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="datas">用于排列的数据</param>
    public Arrangement(string[] datas)
    {
        _datas = datas;
    }

    /// <summary>
    /// 计算排列数，即 A(n, n) = n!
    /// </summary>
    /// <param name="n">总数</param>
    /// <returns>排列数</returns>
    public static long Count(int n)
    {
        return Count(n, n);
    }

    /// <summary>
    /// 计算排列数，即 A(n, m) = n!/(n-m)!
    /// </summary>
    /// <param name="n">总数</param>
    /// <param name="m">选择的个数</param>
    /// <returns>排列数</returns>
    public static long Count(int n, int m)
    {
        if (m < 0 || m > n)
        {
            throw new ArgumentException("n >= 0 && m >= 0 && m <= n required");
        }
        if (m == 0)
        {
            return 1;
        }
        long result = 1;
        for (int i = 0; i < m; i++)
        {
            long next = result * (n - i);
            if (next < result)
            {
                throw new ArithmeticException($"Overflow computing A({n},{m})");
            }
            result = next;
        }
        return result;
    }

    /// <summary>
    /// 计算排列总数，即 A(n, 1) + A(n, 2) + A(n, 3)...
    /// </summary>
    /// <param name="n">总数</param>
    /// <returns>排列数</returns>
    public static long CountAll(int n)
    {
        long total = 0;
        for (int i = 1; i <= n; i++)
        {
            total += Count(n, i);
        }
        return total;
    }

    /// <summary>
    /// 全排列选择（列表全部参与排列）
    /// </summary>
    /// <returns>所有排列列表</returns>
    public List<string[]> Select()
    {
        return Select(_datas.Length);
    }

    /// <summary>
    /// 从当前数据中选择 m 个元素，生成所有「不重复」的排列（Permutation）
    /// </summary>
    /// <param name="m">选择的元素个数</param>
    /// <returns>所有长度为 m 的不重复排列列表</returns>
    public List<string[]> Select(int m)
    {
        if (m < 0 || m > _datas.Length)
        {
            return new List<string[]>();
        }
        if (m == 0)
        {
            return new List<string[]> { Array.Empty<string>() };
        }

        long estimated = Count(_datas.Length, m);
        int capacity = estimated > int.MaxValue ? int.MaxValue : (int)estimated;

        List<string[]> result = new List<string[]>(capacity);
        bool[] visited = new bool[_datas.Length];
        Dfs(new string[m], 0, visited, result);
        return result;
    }

    /// <summary>
    /// 生成当前数据的全部不重复排列（长度为 1 至 n 的所有排列）
    /// </summary>
    /// <returns>所有不重复排列列表</returns>
    public List<string[]> SelectAll()
    {
        List<string[]> result = new List<string[]>();
        for (int m = 1; m <= _datas.Length; m++)
        {
            result.AddRange(Select(m));
        }
        return result;
    }

    /// <summary>
    /// 核心递归方法（回溯算法）
    /// </summary>
    /// <param name="current">当前构建的排列数组</param>
    /// <param name="depth">当前递归深度</param>
    /// <param name="visited">标记数组</param>
    /// <param name="result">结果集</param>
    private void Dfs(string[] current, int depth, bool[] visited, List<string[]> result)
    {
        if (depth == current.Length)
        {
            result.Add(current.ToArray());
            return;
        }

        for (int i = 0; i < _datas.Length; i++)
        {
            if (!visited[i])
            {
                visited[i] = true;
                current[depth] = _datas[i];

                Dfs(current, depth + 1, visited, result);
                visited[i] = false;
            }
        }
    }

    /// <summary>
    /// 返回一个排列的迭代器
    /// </summary>
    /// <param name="m">选择的元素个数</param>
    /// <returns>排列迭代器</returns>
    public IEnumerable<string[]> Iterate(int m)
    {
        return new ArrangementIterator(_datas, m);
    }

    /// <summary>
    /// 排列迭代器
    /// </summary>
    private class ArrangementIterator : IEnumerable<string[]>
    {
        private readonly string[] _datas;
        private readonly int _n;
        private readonly int _m;
        private readonly bool[] _visited;
        private readonly string[] _buffer;

        public ArrangementIterator(string[] datas, int m)
        {
            _datas = datas;
            _n = datas.Length;
            _m = m;
            _visited = new bool[_n];
            _buffer = new string[m];
        }

        public IEnumerator<string[]> GetEnumerator()
        {
            return new Iterator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Iterator : IEnumerator<string[]>
        {
            private readonly ArrangementIterator _outer;
            private readonly int[] _indices;
            private int _depth;
            private bool _end;
            private string[]? _nextItem;
            private bool _nextPrepared;

            public Iterator(ArrangementIterator outer)
            {
                _outer = outer;
                _indices = new int[outer._m];
                _depth = 0;
                _end = false;
                _nextPrepared = false;

                if (outer._m == 0)
                {
                    _nextItem = Array.Empty<string>();
                    _nextPrepared = true;
                }
            }

            public string[] Current => _nextItem ?? Array.Empty<string>();

            object System.Collections.IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_end && !_nextPrepared)
                {
                    return false;
                }

                if (!_nextPrepared)
                {
                    PrepareNext();
                }

                if (_nextItem == null)
                {
                    _end = true;
                    return false;
                }

                if (_outer._m == 0)
                {
                    _end = true;
                }

                _nextPrepared = false;
                return true;
            }

            public void Reset()
            {
                _depth = 0;
                _end = false;
                _nextPrepared = false;
                _nextItem = null;
                Array.Clear(_outer._visited, 0, _outer._visited.Length);
            }

            public void Dispose() { }

            private void PrepareNext()
            {
                if (_nextPrepared || _end)
                {
                    _nextPrepared = true;
                    return;
                }

                if (_outer._m == 0)
                {
                    _nextItem = Array.Empty<string>();
                    _nextPrepared = true;
                    return;
                }

                while (_depth >= 0)
                {
                    int start = _indices[_depth] + 1;
                    bool found = false;

                    for (int i = start; i < _outer._n; i++)
                    {
                        if (!_outer._visited[i])
                        {
                            if (_indices[_depth] != -1)
                            {
                                _outer._visited[_indices[_depth]] = false;
                            }
                            _indices[_depth] = i;
                            _outer._visited[i] = true;
                            _outer._buffer[_depth] = _outer._datas[i];
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        if (_indices[_depth] != -1)
                        {
                            _outer._visited[_indices[_depth]] = false;
                            _indices[_depth] = -1;
                        }
                        _depth--;
                        continue;
                    }

                    if (_depth == _outer._m - 1)
                    {
                        _nextItem = _outer._buffer.Take(_outer._m).ToArray();
                        _outer._visited[_indices[_depth]] = false;
                        _nextPrepared = true;
                        return;
                    }
                    else
                    {
                        _depth++;
                        if (_depth < _outer._m)
                        {
                            _indices[_depth] = -1;
                        }
                    }
                }

                _end = true;
                _nextItem = null;
                _nextPrepared = true;
            }
        }
    }
}
