using WellTool.Cache;

namespace WellTool.Cache.Tests
{
    /// <summary>
    /// Issue #3618 测试
    /// 对于替换的键值对，不做满队列检查和清除
    /// </summary>
    public class Issue3618Test
    {
        [Fact]
        public void PutTest()
        {
            // 创建容量为3的FIFO缓存
            var cache = CacheUtil.NewFIFOCache<object, object>(3);
            
            // 添加三个元素
            cache.Put(1, 1);
            cache.Put(2, 1);
            cache.Put(3, 1);

            Assert.Equal(3, cache.Size());

            // Issue #3618: 对于替换的键值对，不做满队列检查和清除
            // 替换已存在的键
            cache.Put(3, 2);

            // 缓存大小应该仍然是3
            Assert.Equal(3, cache.Size());
        }

        [Fact]
        public void PutExistingKeyTest()
        {
            // 测试替换已存在键的行为
            var cache = CacheUtil.NewFIFOCache<string, int>(2);
            
            cache.Put("a", 1);
            cache.Put("b", 2);
            Assert.Equal(2, cache.Size());

            // 替换"a"的值
            cache.Put("a", 10);
            Assert.Equal(2, cache.Size());
            
            // "a"的值应该是新的
            Assert.Equal(10, cache.Get("a"));
        }

        [Fact]
        public void PutNewKeyWhenFullTest()
        {
            // 当缓存满时，添加新键应该移除最旧的元素
            var cache = CacheUtil.NewFIFOCache<string, int>(2);
            
            cache.Put("a", 1);
            cache.Put("b", 2);
            Assert.Equal(2, cache.Size());

            // 添加新键
            cache.Put("c", 3);
            Assert.Equal(2, cache.Size());
            
            // "a"应该被移除
            Assert.Null(cache.Get("a"));
            Assert.Equal(2, cache.Get("b"));
            Assert.Equal(3, cache.Get("c"));
        }
    }
}
