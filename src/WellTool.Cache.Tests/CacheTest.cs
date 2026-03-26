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