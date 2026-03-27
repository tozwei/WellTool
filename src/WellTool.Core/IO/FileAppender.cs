using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace WellTool.Core.IO
{
    public class FileAppender
    {
        private readonly FileWriter writer;
        private readonly int capacity;
        private readonly bool isNewLineMode;
        private readonly List<string> list;
        private readonly object lockObj = new object();

        public FileAppender(FileInfo destFile, int capacity, bool isNewLineMode)
        {
            this.writer = FileWriter.Create(destFile);
            this.capacity = capacity;
            this.list = new List<string>(capacity);
            this.isNewLineMode = isNewLineMode;
        }

        public FileAppender(FileInfo destFile, Encoding charset, int capacity, bool isNewLineMode)
        {
            this.writer = FileWriter.Create(destFile, charset);
            this.capacity = capacity;
            this.list = new List<string>(capacity);
            this.isNewLineMode = isNewLineMode;
        }

        public FileAppender Append(string line)
        {
            if (list.Count >= capacity)
            {
                Flush();
            }

            lock (lockObj)
            {
                list.Add(line);
            }
            return this;
        }

        public FileAppender Flush()
        {
            lock (lockObj)
            {
                using (var streamWriter = writer.GetWriter())
                {
                    foreach (var str in list)
                    {
                        streamWriter.Write(str);
                        if (isNewLineMode)
                        {
                            streamWriter.WriteLine();
                        }
                    }
                }
                list.Clear();
            }
            return this;
        }
    }
}