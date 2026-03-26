using WellTool.Cache;

namespace WellTool.Cache.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            // 测试FIFOCache
            Console.WriteLine("Testing FIFOCache...");
            var fifoCache = new FIFOCache<string, string>(2);
            fifoCache.Put("key1", "value1");
            Console.WriteLine($"After putting key1: size = {fifoCache.Size()}, contains key1: {fifoCache.ContainsKey("key1")}");
            fifoCache.Put("key2", "value2");
            Console.WriteLine($"After putting key2: size = {fifoCache.Size()}, contains key1: {fifoCache.ContainsKey("key1")}, contains key2: {fifoCache.ContainsKey("key2")}");
            var value1 = fifoCache.Get("key1");
            Console.WriteLine($"Get key1: {value1}");
            var value2 = fifoCache.Get("key2");
            Console.WriteLine($"Get key2: {value2}");
            fifoCache.Put("key3", "value3");
            Console.WriteLine($"After putting key3: size = {fifoCache.Size()}, contains key1: {fifoCache.ContainsKey("key1")}, contains key2: {fifoCache.ContainsKey("key2")}, contains key3: {fifoCache.ContainsKey("key3")}");
            value1 = fifoCache.Get("key1");
            Console.WriteLine($"Get key1: {value1}");
            value2 = fifoCache.Get("key2");
            Console.WriteLine($"Get key2: {value2}");
            var value3 = fifoCache.Get("key3");
            Console.WriteLine($"Get key3: {value3}");
            
            Console.WriteLine();
            
            // 测试LRUCache
            Console.WriteLine("Testing LRUCache...");
            var lruCache = new LRUCache<string, string>(2);
            lruCache.Put("key1", "value1");
            Console.WriteLine($"After putting key1: size = {lruCache.Size()}, contains key1: {lruCache.ContainsKey("key1")}");
            lruCache.Put("key2", "value2");
            Console.WriteLine($"After putting key2: size = {lruCache.Size()}, contains key1: {lruCache.ContainsKey("key1")}, contains key2: {lruCache.ContainsKey("key2")}");
            value1 = lruCache.Get("key1");
            Console.WriteLine($"Get key1: {value1}");
            lruCache.Put("key3", "value3");
            Console.WriteLine($"After putting key3: size = {lruCache.Size()}, contains key1: {lruCache.ContainsKey("key1")}, contains key2: {lruCache.ContainsKey("key2")}, contains key3: {lruCache.ContainsKey("key3")}");
            value1 = lruCache.Get("key1");
            Console.WriteLine($"Get key1: {value1}");
            value2 = lruCache.Get("key2");
            Console.WriteLine($"Get key2: {value2}");
            value3 = lruCache.Get("key3");
            Console.WriteLine($"Get key3: {value3}");
            
            Console.WriteLine();
            
            // 测试LFUCache
            Console.WriteLine("Testing LFUCache...");
            var lfuCache = new LFUCache<string, string>(2);
            lfuCache.Put("key1", "value1");
            Console.WriteLine($"After putting key1: size = {lfuCache.Size()}, contains key1: {lfuCache.ContainsKey("key1")}");
            lfuCache.Put("key2", "value2");
            Console.WriteLine($"After putting key2: size = {lfuCache.Size()}, contains key1: {lfuCache.ContainsKey("key1")}, contains key2: {lfuCache.ContainsKey("key2")}");
            for (int i = 0; i < 5; i++)
            {
                value1 = lfuCache.Get("key1");
                Console.WriteLine($"Get key1 {i+1}: {value1}");
            }
            lfuCache.Put("key3", "value3");
            Console.WriteLine($"After putting key3: size = {lfuCache.Size()}, contains key1: {lfuCache.ContainsKey("key1")}, contains key2: {lfuCache.ContainsKey("key2")}, contains key3: {lfuCache.ContainsKey("key3")}");
            value1 = lfuCache.Get("key1");
            Console.WriteLine($"Get key1: {value1}");
            value2 = lfuCache.Get("key2");
            Console.WriteLine($"Get key2: {value2}");
            value3 = lfuCache.Get("key3");
            Console.WriteLine($"Get key3: {value3}");
            
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}