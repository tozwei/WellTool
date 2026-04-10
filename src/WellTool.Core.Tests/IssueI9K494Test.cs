using Xunit;
using WellTool.Core;
using WellTool.Core.Util;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I9K494 测试
    /// </summary>
    public class IssueI9K494Test
    {
        [Fact]
        public void UnzipTest()
        {
            // 创建临时zip文件
            var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var zipPath = Path.Combine(tempDir, "test.zip");
            var extractPath = Path.Combine(tempDir, "extract");

            try
            {
                // 创建临时目录
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(extractPath);

                // 创建测试文件
                var testFilePath = Path.Combine(tempDir, "test.txt");
                File.WriteAllText(testFilePath, "Test content for zip file");

                // 创建zip文件
                ZipFile.CreateFromDirectory(tempDir, zipPath);

                // 测试Unzip方法
                using var zipStream = File.OpenRead(zipPath);
                ZipUtil.Unzip(zipStream, extractPath, Encoding.UTF8);

                // 验证解压结果
                var extractedFilePath = Path.Combine(extractPath, "test.txt");
                Assert.True(File.Exists(extractedFilePath));
                var content = File.ReadAllText(extractedFilePath);
                Assert.Equal("Test content for zip file", content);
            }
            finally
            {
                // 清理临时文件
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        public void UnzipTest2()
        {
            // 创建临时zip文件
            var tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var zipPath = Path.Combine(tempDir, "test2.zip");
            var extractPath = Path.Combine(tempDir, "extract2");

            try
            {
                // 创建临时目录
                Directory.CreateDirectory(tempDir);
                Directory.CreateDirectory(extractPath);

                // 创建多个测试文件
                for (int i = 1; i <= 3; i++)
                {
                    var testFilePath = Path.Combine(tempDir, $"test{i}.txt");
                    File.WriteAllText(testFilePath, $"Test content {i} for zip file");
                }

                // 创建zip文件
                ZipFile.CreateFromDirectory(tempDir, zipPath);

                // 测试Unzip方法
                using var zipStream = File.OpenRead(zipPath);
                ZipUtil.Unzip(zipStream, extractPath, Encoding.UTF8);

                // 验证解压结果
                for (int i = 1; i <= 3; i++)
                {
                    var extractedFilePath = Path.Combine(extractPath, $"test{i}.txt");
                    Assert.True(File.Exists(extractedFilePath));
                    var content = File.ReadAllText(extractedFilePath);
                    Assert.Equal($"Test content {i} for zip file", content);
                }
            }
            finally
            {
                // 清理临时文件
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }
    }
}
