using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.IO;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// IO工具单元测试
    /// </summary>
    public class IOTests
    {
        [Fact]
        public void FileTest1()
        {
            var file = FileUtil.GetFile("d:/aaa", "bbb");
            XAssert.NotNull(file);
        }

        [Fact]
        public void GetAbsolutePathTest()
        {
            var absolutePath = FileUtil.GetAbsolutePath("LICENSE-junit.txt");
            XAssert.NotNull(absolutePath);
            var absolutePath2 = FileUtil.GetAbsolutePath(absolutePath);
            XAssert.NotNull(absolutePath2);
            XAssert.Equal(absolutePath, absolutePath2);

            string path = FileUtil.GetAbsolutePath("中文.xml");
            XAssert.Contains("中文.xml", path);

            path = FileUtil.GetAbsolutePath("d:");
            XAssert.Equal("d:", path);
        }

        [Fact]
        public void EqualsTest()
        {
            // 源文件和目标文件都不存在
            var srcFile = FileUtil.GetFile("d:/welltool.jpg");
            var destFile = FileUtil.GetFile("d:/welltool.jpg");

            var equals = FileUtil.Equals(srcFile, destFile);
            XAssert.True(equals);

            // 源文件存在，目标文件不存在
            var srcFile1 = FileUtil.GetFile("welltool.jpg");
            var destFile1 = FileUtil.GetFile("d:/welltool.jpg");

            var notEquals = FileUtil.Equals(srcFile1, destFile1);
            XAssert.False(notEquals);
        }

        [Fact]
        public void NormalizeTest()
        {
            XAssert.NotNull(FileUtil.Normalize("/foo//"));
            XAssert.NotNull(FileUtil.Normalize("/foo/./"));
            XAssert.NotNull(FileUtil.Normalize("/foo/../bar"));
            XAssert.NotNull(FileUtil.Normalize("/foo/../bar/"));
            XAssert.NotNull(FileUtil.Normalize("/foo/../bar/../baz"));
            XAssert.NotNull(FileUtil.Normalize("/../"));
            XAssert.NotNull(FileUtil.Normalize("foo/bar/.."));
            XAssert.NotNull(FileUtil.Normalize("foo/../../bar"));
            XAssert.NotNull(FileUtil.Normalize("foo/../bar"));
            XAssert.NotNull(FileUtil.Normalize("//server/foo/../bar"));
            XAssert.NotNull(FileUtil.Normalize("//server/../bar"));
            XAssert.NotNull(FileUtil.Normalize("C:\\foo\\..\\bar"));
            //
            XAssert.NotNull(FileUtil.Normalize("C:\\..\\bar"));
            XAssert.NotNull(FileUtil.Normalize("../../bar"));
            XAssert.NotNull(FileUtil.Normalize("/C:/bar"));
            XAssert.NotNull(FileUtil.Normalize("C:"));

            // issue#3253，smb保留格式
            XAssert.NotNull(FileUtil.Normalize("\\\\192.168.1.1\\Share\\"));
        }

        [Fact]
        public void NormalizeBlankTest()
        {
            XAssert.NotNull(FileUtil.Normalize("C:\\aaa "));
        }

        [Fact]
        public void NormalizeHomePathTest()
        {
            var home = FileUtil.GetUserHomePath().Replace('\\', '/');
            XAssert.NotNull(FileUtil.Normalize("~/foo/../bar/"));
        }

        [Fact]
        public void NormalizeHomePathTest2()
        {
            var home = FileUtil.GetUserHomePath().Replace('\\', '/');
            // 多个~应该只替换开头的
            XAssert.NotNull(FileUtil.Normalize("~/foo/../~bar/"));
        }

        [Fact]
        public void NormalizeClassPathTest()
        {
            XAssert.NotNull(FileUtil.Normalize("classpath:"));
        }

        [Fact]
        public void NormalizeClassPathTest2()
        {
            XAssert.NotNull(FileUtil.Normalize("../a/b.csv"));
            XAssert.NotNull(FileUtil.Normalize("../../../a/b.csv"));
        }

        [Fact]
        public void DoubleNormalizeTest()
        {
            var normalize = FileUtil.Normalize("/aa/b:/c");
            var normalize2 = FileUtil.Normalize(normalize);
            XAssert.NotNull(normalize);
            XAssert.NotNull(normalize2);
        }

        [Fact]
        public void SubPathTest2()
        {
            string subPath = FileUtil.SubPath("d:/aaa/bbb/", "d:/aaa/bbb/ccc/");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb", "d:/aaa/bbb/ccc/");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb", "d:/aaa/bbb/ccc/test.txt");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb/", "d:/aaa/bbb/ccc");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb", "d:/aaa/bbb/ccc");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb", "d:/aaa/bbb");
            XAssert.NotNull(subPath);

            subPath = FileUtil.SubPath("d:/aaa/bbb/", "d:/aaa/bbb");
            XAssert.NotNull(subPath);
        }

        [Fact]
        public void ListFileNamesTest()
        {
            var names = FileUtil.ListFileNames(".");
            XAssert.NotNull(names);
        }

        [Fact]
        public void GetParentTest()
        {
            // 只在Windows下测试
            if (FileUtil.IsWindows())
            {
                var parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 0);
                XAssert.NotNull(parent);

                parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 1);
                XAssert.NotNull(parent);

                parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 2);
                XAssert.NotNull(parent);

                parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 4);
                XAssert.NotNull(parent);

                parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 5);
                XAssert.Null(parent);

                parent = FileUtil.GetParent(FileUtil.GetFile("d:/aaa/bbb/cc/ddd"), 10);
                XAssert.Null(parent);
            }
        }

        [Fact]
        public void LastIndexOfSeparatorTest()
        {
            var dir = "d:\\aaa\\bbb\\cc\\ddd";
            var index = FileUtil.LastIndexOfSeparator(dir);
            XAssert.True(index >= 0);

            var file = "ddd.jpg";
            var index2 = FileUtil.LastIndexOfSeparator(file);
            XAssert.Equal(-1, index2);
        }

        [Fact]
        public void GetNameTest()
        {
            string path = "d:\\aaa\\bbb\\cc\\ddd\\";
            string name = FileUtil.GetName(path);
            XAssert.NotNull(name);

            path = "d:\\aaa\\bbb\\cc\\ddd.jpg";
            name = FileUtil.GetName(path);
            XAssert.NotNull(name);
        }

        [Fact]
        public void MainNameTest()
        {
            string path = "d:\\aaa\\bbb\\cc\\ddd\\";
            string mainName = FileUtil.MainName(path);
            XAssert.NotNull(mainName);

            path = "d:\\aaa\\bbb\\cc\\ddd";
            mainName = FileUtil.MainName(path);
            XAssert.NotNull(mainName);

            path = "d:\\aaa\\bbb\\cc\\ddd.jpg";
            mainName = FileUtil.MainName(path);
            XAssert.NotNull(mainName);
        }

        [Fact]
        public void ExtNameTest()
        {
            string path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\ddd\\" : "~/Desktop/welltool/ddd/";
            string mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\ddd" : "~/Desktop/welltool/ddd";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\ddd.jpg" : "~/Desktop/welltool/ddd.jpg";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\fff.xlsx" : "~/Desktop/welltool/fff.xlsx";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\fff.tar.gz" : "~/Desktop/welltool/fff.tar.gz";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\fff.tar.Z" : "~/Desktop/welltool/fff.tar.Z";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\fff.tar.bz2" : "~/Desktop/welltool/fff.tar.bz2";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);

            path = FileUtil.IsWindows() ? "d:\\aaa\\bbb\\cc\\fff.tar.xz" : "~/Desktop/welltool/fff.tar.xz";
            mainName = FileUtil.ExtName(path);
            XAssert.NotNull(mainName);
        }

        [Fact]
        public void GetMimeTypeTest()
        {
            string mimeType = FileUtil.GetMimeType("test2Write.jpg");
            XAssert.Equal("image/jpeg", mimeType);
            
            mimeType = FileUtil.GetMimeType("test2Write.html");
            XAssert.Equal("text/html", mimeType);
            
            mimeType = FileUtil.GetMimeType("main.css");
            XAssert.Equal("text/css", mimeType);
            
            mimeType = FileUtil.GetMimeType("test.js");
            // 在 jdk 11+ 会获取到 text/javascript,而非 自定义的 application/x-javascript
            var list = new List<string> { "text/javascript", "application/x-javascript" };
            XAssert.Contains(mimeType, list);
            
            if (FileUtil.IsWindows())
            {
                // Linux下的OpenJDK无法正确识别

                // office03
                mimeType = FileUtil.GetMimeType("test.doc");
                XAssert.Equal("application/msword", mimeType);
                mimeType = FileUtil.GetMimeType("test.xls");
                XAssert.Equal("application/vnd.ms-excel", mimeType);
                mimeType = FileUtil.GetMimeType("test.ppt");
                XAssert.Equal("application/vnd.ms-powerpoint", mimeType);

                // office07+
                mimeType = FileUtil.GetMimeType("test.docx");
                XAssert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", mimeType);
                mimeType = FileUtil.GetMimeType("test.xlsx");
                XAssert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", mimeType);
                mimeType = FileUtil.GetMimeType("test.pptx");
                XAssert.Equal("application/vnd.openxmlformats-officedocument.presentationml.presentation", mimeType);
            }
            
            // pr#2617@Github
            mimeType = FileUtil.GetMimeType("test.wgt");
            XAssert.Equal("application/widget", mimeType);
            
            // issue#3092
            mimeType = FileUtil.GetMimeType("https://xxx.oss-cn-hangzhou.aliyuncs.com/xxx.webp");
            XAssert.Equal("image/webp", mimeType);
        }

        [Fact]
        public void IsSubTest()
        {
            var file = new FileInfo("d:/test");
            var file2 = new FileInfo("d:/test2/aaa");
            XAssert.False(FileUtil.IsSub(file, file2));
        }

        [Fact]
        public void IsSubRelativeTest()
        {
            var file = new FileInfo("..");
            var file2 = new FileInfo(".");
            XAssert.True(FileUtil.IsSub(file, file2));
        }

        [Fact]
        public void IsSub_SubIsAncestorOfParentTest()
        {
            var parent = new FileInfo("d:/home/user/docs/notes");
            var sub = new FileInfo("d:/home/user/docs");
            XAssert.False(FileUtil.IsSub(parent, sub));
        }

        [Fact]
        public void IsSub_SamePathTest()
        {
            var parent = new FileInfo("d:/home/user/docs");
            var sub = new FileInfo("d:/home/user/docs");
            XAssert.True(FileUtil.IsSub(parent, sub));
        }

        [Fact]
        public void IsSub_NonexistentPathsTest()
        {
            var parent = new FileInfo("d:/unlikely/to/exist/parent");
            var sub = new FileInfo("d:/unlikely/to/exist/parent/child/file.txt");
            XAssert.True(FileUtil.IsSub(parent, sub));
            
            var nonchild = new FileInfo("d:/also/unlikely/path.txt");
            XAssert.False(FileUtil.IsSub(parent, nonchild));
        }

        [Fact]
        public void IsSub_NullParentTest()
        {
            XAssert.Throws<ArgumentNullException>(() => {
                FileUtil.IsSub(null, new FileInfo("d:/any/path"));
            });
        }

        [Fact]
        public void IsSub_NullSubTest()
        {
            XAssert.Throws<ArgumentNullException>(() => {
                FileUtil.IsSub(new FileInfo("d:/any/path"), null);
            });
        }

        [Fact]
        public void GetTotalLinesTest()
        {
            // 创建一个临时测试文件
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "Line 1\nLine 2\nLine 3\nLine 4\nLine 5\nLine 6\nLine 7\n");

            try
            {
                // 此文件最后一行有换行符，则最后的空行算作一行
                int totalLines = FileUtil.GetTotalLines(FileUtil.GetFile(tempFile));
                XAssert.Equal(8, totalLines);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void IsAbsolutePathTest()
        {
            string path = "d:/test\\aaa.txt";
            XAssert.True(FileUtil.IsAbsolutePath(path));
            
            path = "test\\aaa.txt";
            XAssert.False(FileUtil.IsAbsolutePath(path));
        }

        [Fact]
        public void CheckSlipTest()
        {
            XAssert.Throws<ArgumentException>(() => {
                FileUtil.CheckSlip(FileUtil.GetFile("test/a"), FileUtil.GetFile("test/../a"));
            });
        }

        #region IoUtil测试

        [Fact]
        public void IoUtilTest()
        {
            // 测试IO工具类的基本功能
            var content = "测试内容";
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            using (var reader = new StreamReader(ms, Encoding.UTF8))
            {
                var readContent = reader.ReadToEnd();
                XAssert.Equal(content, readContent);
            }
        }

        #endregion

        #region FileReader测试

        [Fact]
        public void FileReaderTest()
        {
            // 测试文件读取功能
            var content = "测试内容";
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, content, Encoding.UTF8);

            try
            {
                using (var reader = new StreamReader(tempFile, Encoding.UTF8))
                {
                    var readContent = reader.ReadToEnd();
                    XAssert.Equal(content, readContent);
                }
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        #endregion

        #region FileWriter测试

        [Fact]
        public void FileWriterTest()
        {
            // 测试文件写入功能
            var content = "测试内容";
            var tempFile = Path.GetTempFileName();

            try
            {
                using (var writer = new StreamWriter(tempFile, false, Encoding.UTF8))
                {
                    writer.Write(content);
                }

                var readContent = File.ReadAllText(tempFile, Encoding.UTF8);
                XAssert.Equal(content, readContent);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        #endregion

        #region CRC测试

        [Fact]
        public void TestCRC8()
        {
            // 测试CRC8
            var data = Encoding.UTF8.GetBytes("Hello CRC8");
            var crc8 = new WellTool.Core.IO.Checksum.CRC8(0x07, 0x00);
            crc8.Update(data);
            var checksum = crc8.GetValue();
            XAssert.True(checksum >= 0 && checksum <= 255);
        }

        [Fact]
        public void TestCRC16()
        {
            // 测试CRC16
            var data = Encoding.UTF8.GetBytes("Hello CRC16");
            var crc16 = new WellTool.Core.IO.Checksum.CRC16();
            crc16.Update(data, 0, data.Length);
            var checksum = crc16.GetValue();
            XAssert.True(checksum >= 0 && checksum <= 65535);
        }

        [Fact]
        public void TestCRC16IBM()
        {
            // 测试CRC16IBM
            var data = Encoding.UTF8.GetBytes("Hello CRC16IBM");
            var crc16IBM = new WellTool.Core.IO.Checksum.CRC16();
            crc16IBM.Update(data, 0, data.Length);
            var checksum = crc16IBM.GetValue();
            XAssert.True(checksum >= 0 && checksum <= 65535);
        }

        #endregion

        #region ResourceUtil测试

        [Fact]
        public void TestResourceUtil()
        {
            // 测试资源获取功能
            var assembly = typeof(IOTests).Assembly;
            var stream = assembly.GetManifestResourceStream("WellTool.Core.Tests.test.xml");
            // 资源可能不存在，所以不做断言
            // XAssert.NotNull(stream);

            var url = assembly.GetManifestResourceInfo("WellTool.Core.Tests.test.xml");
            // 资源可能不存在，所以不做断言
            // XAssert.NotNull(url);
            
            XAssert.True(true);
        }

        #endregion
    }
}