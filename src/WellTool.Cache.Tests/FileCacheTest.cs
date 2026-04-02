using WellTool.Cache.File;
using System.IO;
using System.Text;

namespace WellTool.Cache.Tests
{
    /// <summary>
    /// 文件缓存单元测试
    /// </summary>
    public class FileCacheTest
    {
        private readonly string _testDir;

        public FileCacheTest()
        {
            _testDir = Path.Combine(Path.GetTempPath(), "WellTool_FileCache_Test");
            if (Directory.Exists(_testDir))
            {
                Directory.Delete(_testDir, true);
            }
            Directory.CreateDirectory(_testDir);
        }

        [Fact]
        public void LFUFileCacheTest()
        {
            // 创建LFU文件缓存
            var cache = new LFUFileCache(1000, 500, 2000);
            Assert.NotNull(cache);
            Assert.Equal(1000, cache.Capacity());
            Assert.Equal(500, cache.MaxFileSize());
        }

        [Fact]
        public void LRUFileCacheTest()
        {
            // 创建LRU文件缓存
            var cache = new LRUFileCache(1000, 500, 2000);
            Assert.NotNull(cache);
            Assert.Equal(1000, cache.Capacity());
            Assert.Equal(500, cache.MaxFileSize());
        }

        [Fact]
        public void GetFileBytesTest()
        {
            // 创建文件
            var testFile = Path.Combine(_testDir, "test.txt");
            var testContent = "Hello, World!";
            File.WriteAllText(testFile, testContent, Encoding.UTF8);

            // 创建缓存
            var cache = new LFUFileCache(1024 * 1024); // 1MB缓存

            // 读取文件
            var bytes = cache.GetFileBytes(testFile);
            Assert.NotNull(bytes);

            var content = Encoding.UTF8.GetString(bytes);
            Assert.Equal(testContent, content);

            // 验证缓存计数
            Assert.Equal(1, cache.GetCachedFilesCount());

            // 再次读取（应该从缓存获取）
            var bytes2 = cache.GetFileBytes(testFile);
            Assert.Equal(bytes.Length, bytes2.Length);

            // 清理
            File.Delete(testFile);
        }

        [Fact]
        public void GetFileBytesWithSmallCacheTest()
        {
            // 创建小文件
            var testFile = Path.Combine(_testDir, "test_small.txt");
            var testContent = "Hello";
            File.WriteAllText(testFile, testContent, Encoding.UTF8);

            // 创建小于文件的缓存
            var cache = new LFUFileCache(1, 1, 0); // 很小的缓存

            // 读取文件（不应该被缓存，因为文件大于maxFileSize）
            var bytes = cache.GetFileBytes(testFile);
            Assert.NotNull(bytes);

            // 缓存计数应该为0，因为文件大于最大缓存大小
            Assert.Equal(0, cache.GetCachedFilesCount());

            // 清理
            File.Delete(testFile);
        }

        [Fact]
        public void ClearCacheTest()
        {
            // 创建文件
            var testFile = Path.Combine(_testDir, "test_clear.txt");
            var testContent = "Test content for clear";
            File.WriteAllText(testFile, testContent, Encoding.UTF8);

            // 创建缓存
            var cache = new LRUFileCache(1024 * 1024);

            // 读取文件
            cache.GetFileBytes(testFile);
            Assert.Equal(1, cache.GetCachedFilesCount());

            // 清空缓存
            cache.Clear();
            Assert.Equal(0, cache.GetCachedFilesCount());

            // 清理
            File.Delete(testFile);
        }

        [Fact]
        public void NonExistentFileTest()
        {
            // 创建缓存
            var cache = new LFUFileCache(1024);

            // 读取不存在的文件
            var bytes = cache.GetFileBytes(Path.Combine(_testDir, "non_existent.txt"));
            Assert.Null(bytes);
        }

        [Fact]
        public void MultipleFilesTest()
        {
            // 创建多个文件
            var files = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                var file = Path.Combine(_testDir, $"file{i}.txt");
                File.WriteAllText(file, $"Content of file {i}");
                files.Add(file);
            }

            // 创建缓存
            var cache = new LFUFileCache(1024 * 100); // 足够大的缓存

            // 读取所有文件
            foreach (var file in files)
            {
                var bytes = cache.GetFileBytes(file);
                Assert.NotNull(bytes);
            }

            Assert.Equal(5, cache.GetCachedFilesCount());

            // 清理
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
