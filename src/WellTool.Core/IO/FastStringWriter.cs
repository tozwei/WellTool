namespace WellTool.Core.IO;

using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 基于 StringBuilder 的快速字符串写入器
/// 
/// @author looly
/// </summary>
public class FastStringWriter
{
    private readonly List<char[]> _buffers = new List<char[]>();
    private readonly int _bufferSize;
    private char[]? _currentBuffer;
    private int _position;

    /// <summary>
    /// 构造一个默认大小（1024）的写入器
    /// </summary>
    public FastStringWriter() : this(1024)
    {
    }

    /// <summary>
    /// 构造一个指定缓冲区大小的写入器
    /// </summary>
    /// <param name="bufferSize">缓冲区大小</param>
    public FastStringWriter(int bufferSize)
    {
        _bufferSize = bufferSize > 0 ? bufferSize : 1024;
        _currentBuffer = new char[_bufferSize];
        _buffers.Add(_currentBuffer);
    }

    /// <summary>
    /// 写入字符
    /// </summary>
    public void Write(char c)
    {
        if (_position >= _currentBuffer!.Length)
        {
            NewBuffer();
        }
        _currentBuffer[_position++] = c;
    }

    /// <summary>
    /// 写入字符串
    /// </summary>
    public void Write(string str)
    {
        if (string.IsNullOrEmpty(str)) return;

        for (int i = 0; i < str.Length; i++)
        {
            Write(str[i]);
        }
    }

    /// <summary>
    /// 写入字符数组
    /// </summary>
    public void Write(char[] chars)
    {
        if (chars == null || chars.Length == 0) return;

        for (int i = 0; i < chars.Length; i++)
        {
            Write(chars[i]);
        }
    }

    /// <summary>
    /// 写入字符数组的一部分
    /// </summary>
    public void Write(char[] chars, int offset, int count)
    {
        for (int i = offset; i < offset + count && i < chars.Length; i++)
        {
            Write(chars[i]);
        }
    }

    /// <summary>
    /// 写入换行符
    /// </summary>
    public void WriteLine()
    {
        Write('\n');
    }

    /// <summary>
    /// 写入一行字符串
    /// </summary>
    public void WriteLine(string str)
    {
        Write(str);
        WriteLine();
    }

    private void NewBuffer()
    {
        _currentBuffer = new char[_bufferSize];
        _buffers.Add(_currentBuffer);
        _position = 0;
    }

    /// <summary>
    /// 获取写入的字符串
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var buffer in _buffers)
        {
            int len = buffer == _currentBuffer ? _position : buffer.Length;
            sb.Append(buffer, 0, len);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 重置写入器
    /// </summary>
    public void Reset()
    {
        _buffers.Clear();
        _currentBuffer = new char[_bufferSize];
        _buffers.Add(_currentBuffer);
        _position = 0;
    }

    /// <summary>
    /// 获取 StringBuilder
    /// </summary>
    public StringBuilder ToStringBuilder()
    {
        var sb = new StringBuilder();
        foreach (var buffer in _buffers)
        {
            int len = buffer == _currentBuffer ? _position : buffer.Length;
            sb.Append(buffer, 0, len);
        }
        return sb;
    }
}
