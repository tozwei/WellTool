namespace WellTool.Core.IO;

using System;

/// <summary>
/// 快速字节缓冲区，用于高效拼接字节数组
/// 
/// @author looly
/// </summary>
public class FastByteBuffer
{
    private const int DEFAULT_CAPACITY = 256;
    private const int MAX_ARRAY_SIZE = int.MaxValue - 8;

    private byte[][] _buffers = Array.Empty<byte[]>();
    private int _bufferCount = 0;
    private int _currentBufferIndex = -1;
    private int _offset;
    private byte[]? _currentBuffer;

    /// <summary>
    /// 当前缓冲区大小
    /// </summary>
    public int Size => _offset;

    /// <summary>
    /// 构造一个初始容量为256的缓冲区
    /// </summary>
    public FastByteBuffer()
    {
        AllocateBuffer(DEFAULT_CAPACITY);
    }

    /// <summary>
    /// 构造一个指定初始容量的缓冲区
    /// </summary>
    /// <param name="capacity">初始容量</param>
    public FastByteBuffer(int capacity)
    {
        AllocateBuffer(capacity);
    }

    private void AllocateBuffer(int capacity)
    {
        _currentBuffer = new byte[capacity];
        _currentBufferIndex = 0;
        _buffers = new byte[][] { _currentBuffer };
        _bufferCount = 1;
        _offset = 0;
    }

    /// <summary>
    /// 添加一个字节
    /// </summary>
    public FastByteBuffer Append(byte b)
    {
        if (_offset >= _currentBuffer!.Length)
        {
            AllocateBuffer(_currentBuffer.Length << 1);
        }
        _currentBuffer[_offset++] = b;
        return this;
    }

    /// <summary>
    /// 添加字节数组
    /// </summary>
    public FastByteBuffer Append(byte[] b, int off, int len)
    {
        if (len <= 0) return this;

        int newSize = _offset + len;
        int neededCapacity = newSize;

        while (neededCapacity > (_currentBuffer?.Length ?? 0))
        {
            int newCapacity = (_currentBuffer?.Length ?? DEFAULT_CAPACITY) << 1;
            if (newCapacity < 0) newCapacity = MAX_ARRAY_SIZE;
            if (newCapacity < neededCapacity) newCapacity = neededCapacity;
            AllocateBuffer(newCapacity);
        }

        Array.Copy(b, off, _currentBuffer!, _offset, len);
        _offset += len;
        return this;
    }

    /// <summary>
    /// 添加字节数组
    /// </summary>
    public FastByteBuffer Append(byte[] b)
    {
        return Append(b, 0, b.Length);
    }

    /// <summary>
    /// 清空缓冲区
    /// </summary>
    public FastByteBuffer Clear()
    {
        _buffers = Array.Empty<byte[]>();
        _bufferCount = 0;
        _currentBufferIndex = -1;
        _currentBuffer = null;
        _offset = 0;
        AllocateBuffer(DEFAULT_CAPACITY);
        return this;
    }

    /// <summary>
    /// 获取所有字节
    /// </summary>
    public byte[] ToArray()
    {
        if (_bufferCount == 1)
        {
            var result = new byte[_offset];
            Array.Copy(_buffers[0], 0, result, 0, _offset);
            return result;
        }

        long size = 0;
        for (int i = 0; i < _bufferCount; i++)
        {
            size += i == _bufferCount - 1 ? _offset : _buffers[i].Length;
        }

        if (size > int.MaxValue)
        {
            throw new OutOfMemoryException("Size exceeds maximum array size");
        }

        var resultArray = new byte[size];
        int pos = 0;
        for (int i = 0; i < _bufferCount; i++)
        {
            int len = i == _bufferCount - 1 ? _offset : _buffers[i].Length;
            Array.Copy(_buffers[i], 0, resultArray, pos, len);
            pos += len;
        }
        return resultArray;
    }

    /// <summary>
    /// 写入到流
    /// </summary>
    public void WriteTo(System.IO.Stream outStream)
    {
        for (int i = 0; i < _bufferCount; i++)
        {
            int len = i == _bufferCount - 1 ? _offset : _buffers[i].Length;
            outStream.Write(_buffers[i], 0, len);
        }
    }

    /// <summary>
    /// 获取缓冲区数组的引用（不复制）
    /// </summary>
    public byte[][] GetBuffers()
    {
        return _buffers;
    }
}
