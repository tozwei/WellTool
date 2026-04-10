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
            var tempRoot = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var sourceDir = Path.Combine(tempRoot, "source");
            var zipPath = Path.Combine(tempRoot, "test.zip");
            var extractPath = Path.Combine(tempRoot, "extract");

            try
            {
                // 创建临时目录
                Directory.CreateDirectory(sourceDir);
                Directory.CreateDirectory(extractPath);

                // 创建测试文件
                var testFilePath = Path.Combine(sourceDir, "test.txt");
                File.WriteAllText(testFilePath, "Test content for zip file");

                // 创建zip文件（zip文件在源目录外）
                ZipFile.CreateFromDirectory(sourceDir, zipPath);

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
                if (Directory.Exists(tempRoot))
                {
                    Directory.Delete(tempRoot, true);
                }
            }
        }

        [Fact]
        public void UnzipTest2()
        {
            // 创建临时zip文件
            var tempRoot = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var sourceDir = Path.Combine(tempRoot, "source");
            var zipPath = Path.Combine(tempRoot, "test2.zip");
            var extractPath = Path.Combine(tempRoot, "extract2");

            try
            {
                // 创建临时目录
                Directory.CreateDirectory(sourceDir);
                Directory.CreateDirectory(extractPath);

                // 创建多个测试文件
                for (int i = 1; i <= 3; i++)
                {
                    var testFilePath = Path.Combine(sourceDir, $"test{i}.txt");
                    File.WriteAllText(testFilePath, $"Test content {i} for zip file");
                }

                // 创建zip文件（zip文件在源目录外）
                ZipFile.CreateFromDirectory(sourceDir, zipPath);

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
                if (Directory.Exists(tempRoot))
                {
                    Directory.Delete(tempRoot, true);
                }
            }
        }
    }
}
