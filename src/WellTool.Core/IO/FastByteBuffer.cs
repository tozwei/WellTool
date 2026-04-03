// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 快速缓冲，将数据存放在缓冲集中，取代以往的单一数组
    /// </summary>
    public class FastByteBuffer
    {
        /// <summary>
        /// 缓冲集
        /// </summary>
        private byte[][] _buffers = new byte[16][];
        /// <summary>
        /// 缓冲数
        /// </summary>
        private int _buffersCount;
        /// <summary>
        /// 当前缓冲索引
        /// </summary>
        private int _currentBufferIndex = -1;
        /// <summary>
        /// 当前缓冲
        /// </summary>
        private byte[] _currentBuffer;
        /// <summary>
        /// 当前缓冲偏移量
        /// </summary>
        private int _offset;
        /// <summary>
        /// 缓冲字节数
        /// </summary>
        private int _size;

        /// <summary>
        /// 一个缓冲区的最小字节数
        /// </summary>
        private readonly int _minChunkLen;

        public FastByteBuffer() : this(1024)
        { }

        public FastByteBuffer(int size)
        {
            if (size <= 0)
            {
                size = 1024;
            }
            _minChunkLen = Math.Abs(size);
        }

        /// <summary>
        /// 分配下一个缓冲区，不会小于1024
        /// </summary>
        /// <param name="newSize">理想缓冲区字节数</param>
        private void NeedNewBuffer(int newSize)
        {
            int delta = newSize - _size;
            int newBufferSize = Math.Max(_minChunkLen, delta);

            _currentBufferIndex++;
            _currentBuffer = new byte[newBufferSize];
            _offset = 0;

            // add buffer
            if (_currentBufferIndex >= _buffers.Length)
            {
                int newLen = _buffers.Length << 1;
                byte[][] newBuffers = new byte[newLen][];
                Array.Copy(_buffers, 0, newBuffers, 0, _buffers.Length);
                _buffers = newBuffers;
            }
            _buffers[_currentBufferIndex] = _currentBuffer;
            _buffersCount++;
        }

        /// <summary>
        /// 向快速缓冲加入数据
        /// </summary>
        /// <param name="array">数据</param>
        /// <param name="off">偏移量</param>
        /// <param name="len">字节数</param>
        /// <returns>快速缓冲自身</returns>
        public FastByteBuffer Append(byte[] array, int off, int len)
        {
            int end = off + len;
            if ((off < 0) || (len < 0) || (end > array.Length))
            {
                throw new IndexOutOfRangeException();
            }
            if (len == 0)
            {
                return this;
            }
            int newSize = _size + len;
            int remaining = len;

            if (_currentBuffer != null)
            {
                // first try to fill current buffer
                int part = Math.Min(remaining, _currentBuffer.Length - _offset);
                Array.Copy(array, end - remaining, _currentBuffer, _offset, part);
                remaining -= part;
                _offset += part;
                _size += part;
            }

            if (remaining > 0)
            {
                // still some data left
                // ask for new buffer
                NeedNewBuffer(newSize);

                // then copy remaining
                // but this time we are sure that it will fit
                int part = Math.Min(remaining, _currentBuffer.Length - _offset);
                Array.Copy(array, end - remaining, _currentBuffer, _offset, part);
                _offset += part;
                _size += part;
            }

            return this;
        }

        /// <summary>
        /// 向快速缓冲加入数据
        /// </summary>
        /// <param name="array">数据</param>
        /// <returns>快速缓冲自身</returns>
        public FastByteBuffer Append(byte[] array)
        {
            return Append(array, 0, array.Length);
        }

        /// <summary>
        /// 向快速缓冲加入一个字节
        /// </summary>
        /// <param name="element">一个字节的数据</param>
        /// <returns>快速缓冲自身</returns>
        public FastByteBuffer Append(byte element)
        {
            if ((_currentBuffer == null) || (_offset == _currentBuffer.Length))
            {
                NeedNewBuffer(_size + 1);
            }

            _currentBuffer[_offset] = element;
            _offset++;
            _size++;

            return this;
        }

        /// <summary>
        /// 将另一个快速缓冲加入到自身
        /// </summary>
        /// <param name="buff">快速缓冲</param>
        /// <returns>快速缓冲自身</returns>
        public FastByteBuffer Append(FastByteBuffer buff)
        {
            if (buff._size == 0)
            {
                return this;
            }
            for (int i = 0; i < buff._currentBufferIndex; i++)
            {
                Append(buff._buffers[i]);
            }
            Append(buff._currentBuffer, 0, buff._offset);
            return this;
        }

        public int Size()
        {
            return _size;
        }

        public bool IsEmpty()
        {
            return _size == 0;
        }

        /// <summary>
        /// 当前缓冲位于缓冲区的索引位
        /// </summary>
        /// <returns>当前缓冲索引</returns>
        public int Index()
        {
            return _currentBufferIndex;
        }

        public int Offset()
        {
            return _offset;
        }

        /// <summary>
        /// 根据索引位返回缓冲集中的缓冲
        /// </summary>
        /// <param name="index">索引位</param>
        /// <returns>缓冲</returns>
        public byte[] GetArray(int index)
        {
            return _buffers[index];
        }

        public void Reset()
        {
            _size = 0;
            _offset = 0;
            _currentBufferIndex = -1;
            _currentBuffer = null;
            _buffersCount = 0;
        }

        /// <summary>
        /// 返回快速缓冲中的数据
        /// </summary>
        /// <returns>快速缓冲中的数据</returns>
        public byte[] ToArray()
        {
            int pos = 0;
            byte[] array = new byte[_size];

            if (_currentBufferIndex == -1)
            {
                return array;
            }

            for (int i = 0; i < _currentBufferIndex; i++)
            {
                int len = _buffers[i].Length;
                Array.Copy(_buffers[i], 0, array, pos, len);
                pos += len;
            }

            Array.Copy(_buffers[_currentBufferIndex], 0, array, pos, _offset);

            return array;
        }

        /// <summary>
        /// 返回快速缓冲中的数据
        /// </summary>
        /// <param name="start">逻辑起始位置</param>
        /// <param name="len">逻辑字节长</param>
        /// <returns>快速缓冲中的数据</returns>
        public byte[] ToArray(int start, int len)
        {
            int remaining = len;
            int pos = 0;
            byte[] array = new byte[len];

            if (len == 0)
            {
                return array;
            }

            int i = 0;
            while (start >= _buffers[i].Length)
            {
                start -= _buffers[i].Length;
                i++;
            }

            while (i < _buffersCount)
            {
                byte[] buf = _buffers[i];
                int c = Math.Min(buf.Length - start, remaining);
                Array.Copy(buf, start, array, pos, c);
                pos += c;
                remaining -= c;
                if (remaining == 0)
                {
                    break;
                }
                start = 0;
                i++;
            }
            return array;
        }

        /// <summary>
        /// 根据索引位返回一个字节
        /// </summary>
        /// <param name="index">索引位</param>
        /// <returns>一个字节</returns>
        public byte Get(int index)
        {
            if ((index >= _size) || (index < 0))
            {
                throw new IndexOutOfRangeException();
            }
            int ndx = 0;
            while (true)
            {
                byte[] b = _buffers[ndx];
                if (index < b.Length)
                {
                    return b[index];
                }
                ndx++;
                index -= b.Length;
            }
        }
    }
}