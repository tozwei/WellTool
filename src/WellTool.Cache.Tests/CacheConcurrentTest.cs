using WellTool.Cache;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace WellTool.Cache.Tests
{
    public class CacheConcurrentTest
    {
        [Fact]
        public void TestFIFOCacheConcurrent()
        {
            // 创建容量为3的FIFO缓存
            var cache = new FIFOCache<string, string>(3);
            
            // 创建100个线程，每个线程执行多次缓存操作
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                var taskIndex = i;
                tasks[i] = Task.Run(() => {
                    // 执行多次缓存操作
                    for (int j = 0; j < 100; j++)
                    {
                        cache.Put($"key{taskIndex}_{j}", $"value{taskIndex}_{j}");
                        cache.Get($"key{taskIndex}_{j}");
                    }
                });
            }
            
            // 等待所有任务完成
            Task.WaitAll(tasks);
            
            // 验证缓存大小不超过容量
            Assert.True(cache.Size() <= 3);
        }

        [Fact]
        public void TestLRUCacheConcurrent()
        {
            // 创建容量为100的LRU缓存
            var cache = new LRUCache<string, string>(100);
            
            // 创建100个线程，每个线程执行多次缓存操作
            var tasks = new Task[100];
            for (int i = 0; i < 100; i++)
            {
                var taskIndex = i;
                tasks[i] = Task.Run(() => {
                    // 执行多次缓存操作
                    for (int j = 0; j < 100; j++)
                    {
                        cache.Put($"key{taskIndex}_{j}", $"value{taskIndex}_{j}");
                        cache.Get($"key{taskIndex}_{j}");
                    }
                });
            }
            
            // 等待所有任务完成
            Task.WaitAll(tasks);
            
            // 验证缓存大小不超过容量
            Assert.True(cache.Size() <= 100);
        }

        [Fact]
        public void TestWeakCacheConcurrent()
        {
            // 创建弱引用缓存
            var cache = new WeakCache<string, int>(60000);
            
            // 创建32个线程，每个线程执行多次缓存操作
            var tasks = new Task[32];
            for (int i = 0; i < 32; i++)
            {
                var taskIndex = i;
                tasks[i] = Task.Run(() => {
                    // 执行多次缓存操作
                    for (int j = 0; j < 100; j++)
                    {
                        var key = ((taskIndex * 100 + j) % 4).ToString();
                        cache.Get(key, () => {
                            // 模拟耗时操作
                            Thread.Sleep(10);
                            return int.Parse(key);
                        });
                    }
                });
            }
            
            // 等待所有任务完成
            Task.WaitAll(tasks);
            
            // 验证缓存正常工作
            Assert.True(cache.Size() <= 4);
        }
    }
}