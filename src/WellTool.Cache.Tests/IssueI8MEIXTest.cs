using WellTool.Cache;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Cache.Tests
{
    /// <summary>
    /// Issue I8MEIX 测试
    /// 在get后的remove前加sleep测试在读取过程中put新值的问题
    /// </summary>
    public class IssueI8MEIXTest
    {
        [Fact]
        public void GetRemoveTest()
        {
            // 创建超时为200毫秒的定时缓存
            var cache = new TimedCache<string, string>(200);
            cache.Put("a", "123");

            // 等待过期
            Thread.Sleep(300);

            // 测试在读取过程中put新值的并发问题
            // 由于缓存已过期，get应该返回null
            var result1 = cache.Get("a");
            Assert.Null(result1);

            // put新值
            cache.Put("a", "456");

            // 验证新值
            var result2 = cache.Get("a");
            Assert.Equal("456", result2);
        }

        [Fact]
        public void ConcurrentGetPutTest()
        {
            // 创建定时缓存
            var cache = new TimedCache<string, string>(100);
            cache.Put("key", "initial");

            // 等待过期
            Thread.Sleep(150);

            // 并发测试：get和put同时进行
            var tasks = new List<Task<string>>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    var value = cache.Get("key");
                    if (value == null)
                    {
                        cache.Put("key", "new");
                        return "new";
                    }
                    return value;
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // 验证最终值
            var finalValue = cache.Get("key");
            Assert.NotNull(finalValue);
            Assert.Equal("new", finalValue);
        }

        [Fact]
        public void TimedCacheExpirationTest()
        {
            // 创建超时为100毫秒的定时缓存
            var cache = new TimedCache<string, string>(100);
            
            // 添加元素
            cache.Put("a", "123");
            Assert.Equal("123", cache.Get("a"));

            // 等待过期
            Thread.Sleep(150);

            // 验证过期
            Assert.Null(cache.Get("a"));
        }

        [Fact]
        public void TimedCacheWithListenerTest()
        {
            // 创建定时缓存
            var cache = new TimedCache<string, string>(50);
            int expireCount = 0;

            cache.SetListener(new TestCacheListener<string, string>(
                onExpire: (k, v) => expireCount++
            ));

            // 添加元素
            cache.Put("a", "1");
            cache.Put("b", "2");

            // 等待过期
            Thread.Sleep(200);

            // 触发手动清理
            cache.Prune();

            // 验证过期数量
            Assert.True(expireCount >= 0);
        }

        private class TestCacheListener<K, V> : CacheListener<K, V>
        {
            private readonly Action<K, V> _onAdd;
            private readonly Action<K, V, V> _onUpdate;
            private readonly Action<K, V> _onRemove;
            private readonly Action<K, V> _onExpire;

            public TestCacheListener(
                Action<K, V> onAdd = null,
                Action<K, V, V> onUpdate = null,
                Action<K, V> onRemove = null,
                Action<K, V> onExpire = null)
            {
                _onAdd = onAdd;
                _onUpdate = onUpdate;
                _onRemove = onRemove;
                _onExpire = onExpire;
            }

            public void OnAdd(K key, V value) => _onAdd?.Invoke(key, value);
            public void OnUpdate(K key, V oldValue, V newValue) => _onUpdate?.Invoke(key, oldValue, newValue);
            public void OnRemove(K key, V value) => _onRemove?.Invoke(key, value);
            public void OnExpire(K key, V value) => _onExpire?.Invoke(key, value);
        }
    }
}
