using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    public class FileReader
    {
        private readonly FileInfo file;
        private readonly Encoding charset;

        public static FileReader Create(FileInfo file, Encoding charset)
        {
            return new FileReader(file, charset);
        }

        public static FileReader Create(FileInfo file)
        {
            return new FileReader(file);
        }

        public FileReader(FileInfo file, Encoding charset)
        {
            this.file = file;
            this.charset = charset ?? Encoding.UTF8;
            CheckFile();
        }

        public FileReader(FileInfo file, string charset)
        {
            this.file = file;
            this.charset = Encoding.GetEncoding(charset);
            CheckFile();
        }

        public FileReader(string filePath, Encoding charset)
        {
            this.file = new FileInfo(filePath);
            this.charset = charset ?? Encoding.UTF8;
            CheckFile();
        }

        public FileReader(string filePath, string charset)
        {
            this.file = new FileInfo(filePath);
            this.charset = Encoding.GetEncoding(charset);
            CheckFile();
        }

        public FileReader(FileInfo file)
        {
            this.file = file;
            this.charset = Encoding.UTF8;
            CheckFile();
        }

        public FileReader(string filePath)
        {
            this.file = new FileInfo(filePath);
            this.charset = Encoding.UTF8;
            CheckFile();
        }

        public byte[] ReadBytes()
        {
            long len = file.Length;
            if (len >= int.MaxValue)
            {
                throw new IOException("File is larger than max array size");
            }

            byte[] bytes = new byte[(int)len];
            using (var inStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                int readLength = inStream.Read(bytes, 0, bytes.Length);
                if (readLength < len)
                {
                    throw new IOException($"File length is [{len}] but read [{readLength}]!");
                }
            }

            return bytes;
        }

        public string ReadString()
        {
            return charset.GetString(ReadBytes());
        }

        public T ReadLines<T>(T collection) where T : ICollection<string>
        {
            using (var reader = new StreamReader(file.FullName, charset))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    collection.Add(line);
                }
            }
            return collection;
        }

        public void ReadLines(Action<string> lineHandler)
        {
            using (var reader = new StreamReader(file.FullName, charset))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineHandler(line);
                }
            }
        }

        public List<string> ReadLines()
        {
            return ReadLines(new List<string>());
        }

        public T Read<T>(Func<StreamReader, T> readerHandler)
        {
            using (var reader = new StreamReader(file.FullName, charset))
            {
                return readerHandler(reader);
            }
        }

        public StreamReader GetReader()
        {
            return new StreamReader(GetInputStream(), charset);
        }

        public FileStream GetInputStream()
        {
            return new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
        }

        public long WriteToStream(Stream outStream)
        {
            return WriteToStream(outStream, false);
        }

        public long WriteToStream(Stream outStream, bool isCloseOut)
        {
            try
            {
                using (var inStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    return inStream.CopyTo(outStream);
                }
            }
            finally
            {
                if (isCloseOut)
                {
                    outStream.Dispose();
                }
            }
        }

        private void CheckFile()
        {
            if (!file.Exists)
            {
                throw new IOException($"File not exist: {file.FullName}");
            }
            if (!file.Attributes.HasFlag(FileAttributes.Normal))
            {
                throw new IOException($"Not a file: {file.FullName}");
            }
        }
    }
}