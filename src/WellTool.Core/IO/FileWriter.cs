using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellTool.Core.IO
{
    public class FileWriter
    {
        private readonly FileInfo file;
        private readonly Encoding charset;
        private readonly FileMode fileMode;

        public static FileWriter Create(FileInfo file, Encoding charset)
        {
            return new FileWriter(file, charset);
        }

        public static FileWriter Create(FileInfo file)
        {
            return new FileWriter(file);
        }

        public FileWriter(FileInfo file, Encoding charset)
        {
            this.file = file;
            this.charset = charset ?? Encoding.UTF8;
            this.fileMode = FileMode.Create;
        }

        public FileWriter(FileInfo file, string charset)
        {
            this.file = file;
            this.charset = Encoding.GetEncoding(charset);
            this.fileMode = FileMode.Create;
        }

        public FileWriter(string filePath, Encoding charset)
        {
            this.file = new FileInfo(filePath);
            this.charset = charset ?? Encoding.UTF8;
            this.fileMode = FileMode.Create;
        }

        public FileWriter(string filePath, string charset)
        {
            this.file = new FileInfo(filePath);
            this.charset = Encoding.GetEncoding(charset);
            this.fileMode = FileMode.Create;
        }

        public FileWriter(FileInfo file)
        {
            this.file = file;
            this.charset = Encoding.UTF8;
            this.fileMode = FileMode.Create;
        }

        public FileWriter(string filePath)
        {
            this.file = new FileInfo(filePath);
            this.charset = Encoding.UTF8;
            this.fileMode = FileMode.Create;
        }

        public FileWriter(FileInfo file, Encoding charset, FileMode fileMode)
        {
            this.file = file;
            this.charset = charset ?? Encoding.UTF8;
            this.fileMode = fileMode;
        }

        public void Write(byte[] bytes)
        {
            using (var stream = new FileStream(file.FullName, fileMode, FileAccess.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void Write(string content)
        {
            using (var writer = new StreamWriter(file.FullName, fileMode == FileMode.Append, charset))
            {
                writer.Write(content);
            }
        }

        public void WriteLine(string content)
        {
            using (var writer = new StreamWriter(file.FullName, fileMode == FileMode.Append, charset))
            {
                writer.WriteLine(content);
            }
        }

        public void WriteLines(ICollection<string> lines)
        {
            using (var writer = new StreamWriter(file.FullName, fileMode == FileMode.Append, charset))
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        public StreamWriter GetWriter()
        {
            return new StreamWriter(file.FullName, fileMode == FileMode.Append, charset);
        }

        public FileStream GetOutputStream()
        {
            return new FileStream(file.FullName, fileMode, FileAccess.Write);
        }
    }
}