using WellTool.Cache;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace WellTool.Cache.Tests
{
    /// <summary>
    /// LRU缓存专项测试
    /// 参考: https://github.com/chinabugotech/hutool/issues/1895
    /// 并发问题测试
    /// </summary>
    public class LRUCacheTest
    {
        [Fact]
        public void PutTest()
        {
            // 创建容量为100，超时为10毫秒的LRU缓存
            var cache = CacheUtil.NewLRUCache<string, string>(100, 10);
            
            // 并发测试
            Parallel.For(0, 10000, i =>
            {
                cache.Get($"key-{i % 100}", () => $"value-{i}");
            });
            
            Thread.Sleep(100);
        }

        [Fact]
        public void ReadWriteTest()
        {
            // 创建容量为10的LRU缓存
            var cache = CacheUtil.NewLRUCache<int, int>(10);
            
            // 添加0-9
            for (int i = 0; i < 10; i++)
            {
                cache.Put(i, i);
            }

            // 创建10个线程，每个线程读取对应索引的元素10000次
            var countDownLatch = new CountdownEvent(10);
            
            for (int i = 0; i < 10; i++)
            {
                var finalI = i;
                new Thread(() =>
                {
                    for (int j = 0; j < 10000; j++)
                    {
                        cache.Get(finalI);
                    }
                    countDownLatch.Signal();
                }).Start();
            }

            // 等待所有线程完成
            countDownLatch.Wait();

            // 按顺序读取0-9
            var sb1 = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                var value = cache.Get(i);
                sb1.Append(value.ToString());
            }
            Assert.Equal("0123456789", sb1.ToString());

            // 添加第11个元素，此时0最久未使用，应该淘汰0
            cache.Put(11, 11);

            // 再次按顺序读取0-9
            var sb2 = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                var value = cache.Get(i);
                if (value == null)
                {
                    sb2.Append("null");
                }
                else
                {
                    sb2.Append(value.ToString());
                }
            }
            Assert.Equal("null123456789", sb2.ToString());
        }

        [Fact]
        public void Issue2647Test()
        {
            // Issue #2647 测试
            int removeCount = 0;

            // 创建容量为3，超时为1毫秒的LRU缓存
            var cache = CacheUtil.NewLRUCache<string, int>(3, 1);
            cache.SetListener(new SimpleCacheListener<string, int>(
                (key, value) =>
                {
                    // 共移除7次
                    removeCount++;
                }
            ));

            for (int i = 0; i < 10; i++)
            {
                cache.Put($"key-{i}", i);
            }

            // 等待所有元素超时
            Thread.Sleep(100);

            Assert.Equal(7, removeCount);
            Assert.Equal(3, cache.Size());
        }

        /// <summary>
        /// 简单的CacheListener实现
        /// </summary>
        private class SimpleCacheListener<K, V> : CacheListener<K, V>
        {
            private readonly Action<K, V> _onRemove;

            public SimpleCacheListener(Action<K, V> onRemove)
            {
                _onRemove = onRemove;
            }

            public void OnAdd(K key, V value) { }

            public void OnUpdate(K key, V oldValue, V newValue) { }

            public void OnRemove(K key, V value)
            {
                _onRemove?.Invoke(key, value);
            }

            public void OnExpire(K key, V value) { }
        }
    }
}
