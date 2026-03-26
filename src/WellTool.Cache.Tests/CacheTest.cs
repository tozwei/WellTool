using WellTool.Cache;
using System.Threading;

namespace WellTool.Cache.Tests
{
    public class CacheTest
    {
        [Fact]
        public void TestFIFOCache()
        {
            // 创建容量为 2 的 FIFO 缓存
            var cache = new FIFOCache<string, string>(2);
            
            // 添加元素
            cache.Put("key1", "value1");
            cache.Put("key2", "value2");
            
            // 验证元素存在
            Assert.Equal("value1", cache.Get("key1"));
            Assert.Equal("value2", cache.Get("key2"));
            
            // 添加第三个元素，应该移除第一个元素
            cache.Put("key3", "value3");
            
            // 验证第一个元素被移除
            Assert.Null(cache.Get("key1"));
            Assert.Equal("value2", cache.Get("key2"));
            Assert.Equal("value3", cache.Get("key3"));
        }

        [Fact]
        public void TestFIFOCacheCapacity()
        {
            // 创建容量为 100 的 FIFO 缓存
            var cache = new FIFOCache<string, string>(100);
            
            // 添加超过容量的元素
            for (int i = 0; i < 200; i++)
            {
                cache.Put($"key{i}", $"value{i}");
            }
            
            // 验证缓存大小不超过容量
            Assert.Equal(100, cache.Size());
        }

        [Fact]
        public void TestLRUCache()
        {
            // 创建容量为 2 的 LRU 缓存
            var cache = new LRUCache<string, string>(2);
            
            // 添加元素
            cache.Put("key1", "value1");
            cache.Put("key2", "value2");
            
            // 访问 key1，使其成为最近使用的元素
            Assert.Equal("value1", cache.Get("key1"));
            
            // 添加第三个元素，应该移除 key2
            cache.Put("key3", "value3");
            
            // 验证 key2 被移除
            Assert.Equal("value1", cache.Get("key1"));
            Assert.Null(cache.Get("key2"));
            Assert.Equal("value3", cache.Get("key3"));
        }

        [Fact]
        public void TestLFUCache()
        {
            // 创建容量为 2 的 LFU 缓存
            var cache = new LFUCache<string, string>(2);
            
            // 添加元素
            cache.Put("key1", "value1");
            cache.Put("key2", "value2");
            
            // 访问 key1 多次，增加其使用频率
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal("value1", cache.Get("key1"));
            }
            
            // 添加第三个元素，应该移除使用频率较低的 key2
            cache.Put("key3", "value3");
            
            // 验证 key2 被移除
            Assert.Equal("value1", cache.Get("key1"));
            Assert.Null(cache.Get("key2"));
            Assert.Equal("value3", cache.Get("key3"));
        }

        [Fact]
        public void TestLFUCacheWithNullKey()
        {
            // 创建 LFU 缓存
            var cache = new LFUCache<string, string>(3);
            
            // 测试 null 键
            var result = cache.Get(null);
            Assert.Null(result);
        }

        [Fact]
        public void TestLFUCacheListener()
        {
            // 创建容量为3的LFU缓存
            var cache = new LFUCache<string, string>(3);
            
            // 验证监听器是否被正确调用
            int removeCount = 0;
            
            cache.SetListener(new SimpleCacheListener<string, string>(
                (key, value) => { removeCount++; }
            ));
            
            // 添加元素
            cache.Put("key1", "value1");
            // 访问key1，增加其使用频率
            cache.Get("key1");
            cache.Put("key2", "value2");
            cache.Put("key3", "value3");
            
            // 添加第四个元素，应该移除使用频率较低的key2和key3
            cache.Put("key4", "value4");
            
            // 验证key1存在，key2和key3被移除
            Assert.Equal("value1", cache.Get("key1"));
            Assert.Null(cache.Get("key2"));
            Assert.Null(cache.Get("key3"));
            Assert.Equal("value4", cache.Get("key4"));
            // 验证监听器被调用了2次
            Assert.Equal(2, removeCount);
        }

        [Fact]
        public void TestTimedCache()
        {
            // 创建过期时间为 500 毫秒的定时缓存
            var cache = new TimedCache<string, string>(500);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素存在
            Assert.Equal("value1", cache.Get("key1"));
            
            // 等待过期
            Thread.Sleep(600);
            
            // 验证元素过期
            Assert.Null(cache.Get("key1"));
        }

        [Fact]
        public void TestTimedCacheWithDifferentTimeouts()
        {
            // 创建默认过期时间为 4 毫秒的定时缓存
            var cache = new TimedCache<string, string>(4);
            
            // 添加不同过期时间的元素
            cache.Put("key1", "value1", 1); // 1毫秒过期
            cache.Put("key2", "value2", 5000); // 5秒过期
            cache.Put("key3", "value3"); // 默认过期(4毫秒)
            cache.Put("key4", "value4", long.MaxValue); // 永不过期
            
            // 启动定时任务，每5毫秒检查一次过期
            cache.SchedulePrune(5);
            
            // 等待5毫秒
            Thread.Sleep(5);
            
            // 验证过期情况
            Assert.Null(cache.Get("key1")); // 应该过期
            Assert.Equal("value2", cache.Get("key2")); // 应该保留
            Assert.Null(cache.Get("key3")); // 应该过期
            Assert.Equal("value4", cache.Get("key4")); // 应该保留
            
            // 测试获取不存在的键时使用默认值
            var value3Supplier = cache.Get("key3", () => "Default supplier");
            Assert.Equal("Default supplier", value3Supplier);
            
            // 取消定时清理
            cache.CancelPruneSchedule();
        }

        [Fact]
        public void TestTimedCacheExpireWithListener()
        {
            // 创建定时缓存
            int timeout = 50;
            var cache = new TimedCache<int, string>(timeout);
            
            // 计数器
            int removeCount = 0;
            
            // 设置监听器
            cache.SetListener(new SimpleCacheListener<int, string>(
                (key, value) => { removeCount++; }
            ));
            
            // 添加元素
            cache.Put(1, "value1");
            
            // 等待过期
            Thread.Sleep(100);
            
            // 验证元素过期
            Assert.False(cache.ContainsKey(1));
            // 验证监听器被调用
            Assert.Equal(1, removeCount);
        }

        [Fact]
        public void TestReentrantCacheClearMethod()
        {
            // 计数器
            int removeCount = 0;
            
            // 创建 LRU 缓存
            var cache = CacheUtil.NewLRUCache<string, string>(4);
            
            // 设置监听器
            cache.SetListener(new SimpleCacheListener<string, string>(
                (key, value) => { removeCount++; }
            ));
            
            // 添加元素
            cache.Put("key1", "String1");
            cache.Put("key2", "String2");
            cache.Put("key3", "String3");
            cache.Put("key1", "String4"); // 覆盖已存在的键
            cache.Put("key4", "String5");
            
            // 清空缓存
            cache.Clear();
            
            // 验证所有元素都被移除
            Assert.Equal(5, removeCount); // 包括覆盖时移除的旧值
        }

        [Fact]
        public void TestNoCache()
        {
            // 创建无缓存
            var cache = new NoCache<string, string>();
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素不存在（无缓存）
            Assert.Null(cache.Get("key1"));
        }

        [Fact]
        public void TestWeakCache()
        {
            // 创建弱引用缓存，设置过期时间为 10000 毫秒
            var cache = new WeakCache<string, string>(10000);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素存在
            Assert.Equal("value1", cache.Get("key1"));
            
            // 强制垃圾回收
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            // 验证元素可能被回收（弱引用特性）
            var value = cache.Get("key1");
            // 要么返回 null（被回收），要么返回 "value1"（未被回收）
            Assert.True(value == null || value == "value1");
        }

        [Fact]
        public void TestCacheUtil()
        {
            // 测试 CacheUtil 创建不同类型的缓存
            var fifoCache = CacheUtil.NewFIFOCache<string, string>(10);
            Assert.NotNull(fifoCache);
            
            var lruCache = CacheUtil.NewLRUCache<string, string>(10);
            Assert.NotNull(lruCache);
            
            var lfuCache = CacheUtil.NewLFUCache<string, string>(10);
            Assert.NotNull(lfuCache);
            
            var timedCache = CacheUtil.NewTimedCache<string, string>(1000);
            Assert.NotNull(timedCache);
            
            var weakCache = CacheUtil.NewWeakCache<string, string>(1000);
            Assert.NotNull(weakCache);
        }

        [Fact]
        public void TestCacheListener()
        {
            // 创建一个带有监听器的缓存
            bool onAddCalled = false;
            bool onRemoveCalled = false;
            
            var cache = new FIFOCache<string, string>(2);
            cache.SetListener(new SimpleCacheListener<string, string>(
                (key, value) => { onRemoveCalled = true; }
            ));
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 移除元素，触发 OnRemove
            cache.Remove("key1");
            Assert.True(onRemoveCalled);
        }

        [Fact]
        public void TestFIFOCacheListener()
        {
            // 创建容量为3的FIFO缓存
            var cache = new FIFOCache<string, string>(3);
            
            // 验证监听器是否被正确调用
            string removedKey = null;
            string removedValue = null;
            
            cache.SetListener(new SimpleCacheListener<string, string>(
                (key, value) => {
                    removedKey = key;
                    removedValue = value;
                }
            ));
            
            // 添加元素
            cache.Put("key1", "value1");
            cache.Put("key2", "value2");
            cache.Put("key3", "value3");
            
            // 添加第四个元素，应该移除第一个元素
            cache.Put("key4", "value4");
            
            // 验证第一个元素被移除，且监听器被调用
            Assert.Null(cache.Get("key1"));
            Assert.Equal("key1", removedKey);
            Assert.Equal("value1", removedValue);
        }

        [Fact]
        public void TestLRUCacheReadWrite()
        {
            // 创建容量为 10 的 LRU 缓存
            var cache = new LRUCache<int, int>(10);
            
            // 添加元素
            for (int i = 0; i < 10; i++)
            {
                cache.Put(i, i);
            }
            
            // 创建 10 个线程，每个线程读取对应索引的元素 10000 次
            var countDownLatch = new System.Threading.CountdownEvent(10);
            
            for (int i = 0; i < 10; i++)
            {
                var finalI = i;
                new System.Threading.Thread(() => {
                    for (int j = 0; j < 10000; j++)
                    {
                        cache.Get(finalI);
                    }
                    countDownLatch.Signal();
                }).Start();
            }
            
            // 等待所有线程完成
            countDownLatch.Wait();
            
            // 按顺序读取 0-9
            var sb1 = new System.Text.StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                if (cache.ContainsKey(i))
                {
                    var value = cache.Get(i);
                    sb1.Append(value.ToString());
                }
                else
                {
                    sb1.Append("null");
                }
            }
            Assert.Equal("0123456789", sb1.ToString());
            
            // 添加第 11 个元素，应该淘汰最久未使用的 0
            cache.Put(11, 11);
            
            // 再次按顺序读取 0-9
            var sb2 = new System.Text.StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                if (cache.ContainsKey(i))
                {
                    var value = cache.Get(i);
                    sb2.Append(value.ToString());
                }
                else
                {
                    sb2.Append("null");
                }
            }
            Assert.Equal("null123456789", sb2.ToString());
        }

        [Fact]
        public void TestLRUCacheIssue2647()
        {
            // 计数器
            int removeCount = 0;
            
            // 创建容量为 3 的 LRU 缓存
            var cache = new LRUCache<string, int>(3);
            cache.SetListener(new SimpleCacheListener<string, int>(
                (key, value) => { removeCount++; }
            ));
            
            // 添加 10 个元素
            for (int i = 0; i < 10; i++)
            {
                cache.Put($"key-{i}", i);
            }
            
            // 验证移除次数和缓存大小
            Assert.Equal(7, removeCount);
            Assert.Equal(3, cache.Size());
        }

        [Fact]
        public void TestWeakCacheRemove()
        {
            // 创建弱引用缓存，设置过期时间为 -1（永不过期）
            var cache = new WeakCache<string, string>(-1);
            cache.Put("abc", "123");
            cache.Put("def", "456");
            
            // 验证初始大小
            Assert.Equal(2, cache.Size());
            
            // 移除一个元素
            cache.Remove("abc");
            
            // 验证移除后的大小
            Assert.Equal(1, cache.Size());
        }

        [Fact]
        public void TestCacheWithZeroCapacity()
        {
            // 创建容量为0的缓存
            var cache = new FIFOCache<string, string>(0);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素不能被添加
            Assert.Null(cache.Get("key1"));
        }

        [Fact]
        public void TestCacheWithNegativeCapacity()
        {
            // 创建容量为负数的缓存
            var cache = new FIFOCache<string, string>(-1);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素不能被添加
            Assert.Null(cache.Get("key1"));
        }

        [Fact]
        public void TestTimedCacheWithZeroTimeout()
        {
            // 创建过期时间为0的定时缓存
            var cache = new TimedCache<string, string>(0);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素立即过期
            Assert.Null(cache.Get("key1"));
        }

        [Fact]
        public void TestTimedCacheWithNegativeTimeout()
        {
            // 创建过期时间为负数的定时缓存（永不过期）
            var cache = new TimedCache<string, string>(-1);
            
            // 添加元素
            cache.Put("key1", "value1");
            
            // 验证元素不会过期
            Assert.Equal("value1", cache.Get("key1"));
        }

        [Fact]
        public void TestCacheClear()
        {
            // 创建缓存
            var cache = new FIFOCache<string, string>(3);
            
            // 添加元素
            cache.Put("key1", "value1");
            cache.Put("key2", "value2");
            cache.Put("key3", "value3");
            
            // 验证缓存大小
            Assert.Equal(3, cache.Size());
            
            // 清空缓存
            cache.Clear();
            
            // 验证缓存为空
            Assert.Equal(0, cache.Size());
            Assert.Null(cache.Get("key1"));
            Assert.Null(cache.Get("key2"));
            Assert.Null(cache.Get("key3"));
        }

        [Fact]
        public void TestCacheUpdate()
        {
            // 创建缓存
            var cache = new FIFOCache<string, string>(3);
            
            // 添加元素
            cache.Put("key1", "value1");
            Assert.Equal("value1", cache.Get("key1"));
            
            // 更新元素
            cache.Put("key1", "value2");
            Assert.Equal("value2", cache.Get("key1"));
        }

        // 简单的 CacheListener 实现
        private class SimpleCacheListener<K, V> : CacheListener<K, V>
        {
            private readonly Action<K, V> _onRemove;

            public SimpleCacheListener(Action<K, V> onRemove)
            {
                _onRemove = onRemove;
            }

            public void OnAdd(K key, V value)
            {
            }

            public void OnUpdate(K key, V oldValue, V newValue)
            {
            }

            public void OnRemove(K key, V value)
            {
                _onRemove?.Invoke(key, value);
            }

            public void OnExpire(K key, V value)
            {
            }
        }
    }
}