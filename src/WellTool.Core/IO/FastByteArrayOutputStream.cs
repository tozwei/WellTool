namespace WellTool.Core.IO;

using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 基于 FastByteBuffer 的字节数组输出流
/// 
/// @author looly
/// </summary>
public class FastByteArrayOutputStream : Stream
{
    private readonly FastByteBuffer _buffer;

    /// <summary>
    /// 构造
    /// </summary>
    public FastByteArrayOutputStream()
    {
        _buffer = new FastByteBuffer();
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="capacity">初始容量</param>
    public FastByteArrayOutputStream(int capacity)
    {
        _buffer = new FastByteBuffer(capacity);
    }

    /// <summary>
    /// 写入一个字节
    /// </summary>
    public override void WriteByte(byte b)
    {
        _buffer.Append(b);
    }

    /// <summary>
    /// 写入字节数组
    /// </summary>
    public override void Write(byte[] b, int off, int len)
    {
        if (len > 0)
        {
            _buffer.Append(b, off, len);
        }
    }

    /// <summary>
    /// 获取当前输出流中的字节数组
    /// </summary>
    /// <returns>字节数组</returns>
    public byte[] ToByteArray()
    {
        return _buffer.ToArray();
    }

    /// <summary>
    /// 获取流内容
    /// </summary>
    public ReadOnlySpan<byte> ToArray()
    {
        return _buffer.ToArray().AsSpan();
    }

    /// <summary>
    /// 获取流大小
    /// </summary>
    public int Size => _buffer.Size;

    /// <summary>
    /// 重置流
    /// </summary>
    public void Reset()
    {
        _buffer.Clear();
    }

    /// <summary>
    /// 转换为输入流
    /// </summary>
    public MemoryStream ToMemoryStream()
    {
        return new MemoryStream(_buffer.ToArray());
    }

    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;

    public override long Length => _buffer.Size;

    public override long Position
    {
        get => _buffer.Size;
        set => throw new NotSupportedException();
    }

    public override void Flush() { }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }
}
