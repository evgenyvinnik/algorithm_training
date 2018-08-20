// © Evgeny Vinnik

namespace CacheUser
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMruCache();
        }

        static void TestLruCache()
        {
            //var cache = new Cache.Cache(4, 128, new MruEvictionAlgorithm());
        }

        static void TestMruCache()
        {
           // var cache = new Cache.Cache(4, 128, new MruEvictionAlgorithm());

        }
    }
}
