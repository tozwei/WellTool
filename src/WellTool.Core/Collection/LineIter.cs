using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 将 Reader 包装为一个按照行读取的迭代器
    /// </summary>
    public class LineIter : IEnumerable<string>, IDisposable
    {
        private readonly TextReader _reader;
        private List<string> _cachedLines;
        private int _currentIndex = -1;
        private bool _finished = false;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream">输入流</param>
        /// <param name="charset">编码</param>
        public LineIter(Stream stream, Encoding charset)
            : this(new StreamReader(stream, charset))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="reader">Reader对象，不能为null</param>
        public LineIter(TextReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary>
        /// 获取迭代器
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return new LineEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 读取下一行
        /// </summary>
        internal string ReadNextLine()
        {
            string line = _reader.ReadLine();
            if (line == null)
            {
                _finished = true;
                return null;
            }
            if (!IsValidLine(line))
            {
                return ReadNextLine();
            }
            return line;
        }

        /// <summary>
        /// 是否已结束
        /// </summary>
        internal bool IsFinished => _finished;

        /// <summary>
        /// 重写此方法来判断是否每一行都被返回，默认全部为 true
        /// </summary>
        /// <param name="line">需要验证的行</param>
        /// <returns>是否通过验证</returns>
        protected virtual bool IsValidLine(string line)
        {
            return true;
        }

        /// <summary>
        /// 关闭 Reader
        /// </summary>
        public void Dispose()
        {
            _reader?.Dispose();
        }

        /// <summary>
        /// 行迭代器
        /// </summary>
        private class LineEnumerator : IEnumerator<string>
        {
            private readonly LineIter _iter;
            private string _current;
            private bool _movedOnce = false;

            public LineEnumerator(LineIter iter)
            {
                _iter = iter;
            }

            public string Current => _current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_iter.IsFinished)
                {
                    return false;
                }

                if (!_movedOnce)
                {
                    _movedOnce = true;
                }

                _current = _iter.ReadNextLine();
                return !_iter.IsFinished;
            }

            public void Reset()
            {
                throw new NotSupportedException("Reset is not supported");
            }
        }
    }
}
