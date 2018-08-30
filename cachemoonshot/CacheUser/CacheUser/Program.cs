// © Evgeny Vinnik

using System;
using Cache;

namespace CacheUser
{
    class Program
    {
        //const int entries = 10;
        const int entries = 1024 * 64;

        const int dataLength = 1024;
        const int mainStoreLatencyMs = 1;
        static int cacheEvictions;
        static int cacheMisses;

        static int entriesRetrieve = 1024 * 4;

        static void Main()
        {
            var mainStore = new MainStore<int, string>();
            var cache = new Cache<int, string>(32, 1024 * 16, new LruSortEvictionAlgorithm<int, string>());

            cache.CacheEvictionListener += CacheEvictionListener;
            cache.CacheMissListener += CacheMissListener;
            Generate1GbMainStore(ref mainStore, ref cache);

            long elapsedTicksCache = 0;
            long elapsedTicksMainStore = 0;
            long elapsedTicksMainStoreLatency = 0;
            string value;

            for (int tries = 0; tries < entriesRetrieve; tries++)
            {
                var key = Utilities.RandomGenerator.Next(entries);

                //retrieving value from main store which is entirely in memory as well
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    value = mainStore.GetValue(key);

                    watch.Stop();
                    elapsedTicksMainStore += watch.ElapsedTicks;
                }

                //retrieving value from main store which is entirely in memory as well
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    value = mainStore.GetValueLatency(key, mainStoreLatencyMs);

                    watch.Stop();
                    elapsedTicksMainStoreLatency += watch.ElapsedTicks;
                }

                //retrieving value from main store which is entirely in memory as well
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    try
                    {
                        value = cache.TryGetValue(key);
                    }
                    catch (CacheMissException e)
                    {
                        value = mainStore.GetValueLatency(key, mainStoreLatencyMs);
                        cache.PutValue(key, value);
                    }

                    watch.Stop();
                    elapsedTicksCache += watch.ElapsedTicks;
                }
            }

            Console.WriteLine($"Cache access with main storefallback {elapsedTicksCache:N0} ticks");
            Console.WriteLine($"Main store access with latency {elapsedTicksMainStoreLatency:N0} ticks");
            Console.WriteLine($"Main store access in-memory {elapsedTicksMainStore:N0} ticks");
            Console.WriteLine();
            Console.WriteLine($"Total number of entries {entries:N0}, cache evictions {cacheEvictions:N0}");
            Console.WriteLine($"Cache misses {cacheMisses:N0}");
        }

        static void CacheMissListener(object sender, EventArgs e)
        {
            cacheMisses++;
        }

        static void CacheEvictionListener(object sender, InvalidationEventArgs e)
        {
            cacheEvictions++;
        }

        static void Generate1GbMainStore(ref MainStore<int, string> mainStore, ref Cache<int, string> cache)
        {
            for (int i = 0; i < entries; i++)
            {
                var value = Utilities.GenerateRandomString(dataLength);
                mainStore.PutValue(i, value);
                cache.PutValue(i, value);
            }
        }

        internal static class Utilities
        {
            public static readonly Random RandomGenerator = new Random();

            internal static string GenerateRandomString(int length)
            {
                var randomBytes = new byte[RandomGenerator.Next(length)];
                RandomGenerator.NextBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
