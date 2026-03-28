using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Xunit;
using WellTool.Core.Compress;

namespace WellTool.Core.Tests
{
    public class CompressTests
    {
        [Fact]
        public void ZipWriterTest()
        {
            // 创建临时测试文件
            var tempZipFile = Path.GetTempFileName() + ".zip";
            var tempDir = Path.Combine(Path.GetTempPath(), "test_dir");
            
            try
            {
                // 创建测试目录和文件
                Directory.CreateDirectory(tempDir);
                File.WriteAllText(Path.Combine(tempDir, "test1.txt"), "Hello, World!");
                File.WriteAllText(Path.Combine(tempDir, "test2.txt"), "Hutool is great!");
                
                // 创建zip文件
                using (var writer = ZipWriter.Of(tempZipFile))
                {
                    writer.Add(true, null, tempDir);
                }
                
                // 验证zip文件是否创建成功
                Assert.True(File.Exists(tempZipFile));
                
                // 读取zip文件内容
                using (var reader = ZipReader.Of(tempZipFile))
                {
                    // 验证文件是否存在
                    var test1Stream = reader.Get("test_dir/test1.txt");
                    Assert.NotNull(test1Stream);
                    
                    var test2Stream = reader.Get("test_dir/test2.txt");
                    Assert.NotNull(test2Stream);
                    
                    // 读取文件内容
                    using (var sr = new StreamReader(test1Stream))
                    {
                        var content = sr.ReadToEnd();
                        Assert.Equal("Hello, World!", content);
                    }
                    
                    using (var sr = new StreamReader(test2Stream))
                    {
                        var content = sr.ReadToEnd();
                        Assert.Equal("Hutool is great!", content);
                    }
                }
            }
            finally
            {
                // 清理临时文件和目录
                if (File.Exists(tempZipFile))
                {
                    File.Delete(tempZipFile);
                }
                
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void ZipReaderReadToTest()
        {
            // 创建临时测试文件
            var tempZipFile = Path.GetTempFileName() + ".zip";
            var tempDir = Path.Combine(Path.GetTempPath(), "test_dir");
            var extractDir = Path.Combine(Path.GetTempPath(), "extract_dir");
            
            try
            {
                // 创建测试目录和文件
                Directory.CreateDirectory(tempDir);
                File.WriteAllText(Path.Combine(tempDir, "test1.txt"), "Hello, World!");
                File.WriteAllText(Path.Combine(tempDir, "test2.txt"), "Hutool is great!");
                
                // 创建zip文件
                using (var writer = ZipWriter.Of(tempZipFile))
                {
                    writer.Add(true, null, tempDir);
                }
                
                // 解压zip文件
                using (var reader = ZipReader.Of(tempZipFile))
                {
                    reader.ReadTo(extractDir);
                }
                
                // 验证解压是否成功
                var test1Path = Path.Combine(extractDir, "test_dir", "test1.txt");
                var test2Path = Path.Combine(extractDir, "test_dir", "test2.txt");
                
                Assert.True(File.Exists(test1Path));
                Assert.True(File.Exists(test2Path));
                
                var test1Content = File.ReadAllText(test1Path);
                var test2Content = File.ReadAllText(test2Path);
                
                Assert.Equal("Hello, World!", test1Content);
                Assert.Equal("Hutool is great!", test2Content);
            }
            finally
            {
                // 清理临时文件和目录
                if (File.Exists(tempZipFile))
                {
                    File.Delete(tempZipFile);
                }
                
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
                
                if (Directory.Exists(extractDir))
                {
                    Directory.Delete(extractDir, true);
                }
            }
        }
    }
}