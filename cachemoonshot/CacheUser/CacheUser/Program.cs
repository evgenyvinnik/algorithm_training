// © Evgeny Vinnik

using System;
using Cache;

namespace CacheUser
{
    /// <summary>
    /// This is a simple test that checks effectiveness of our cache implementation
    /// against cached information being entirely in memory or being on a physical storage.
    /// </summary>
    class Program
    {
        /// <summary>
        /// number of total entries in the main store
        /// </summary>
        const int entries = 1024 * 64;

        /// <summary>
        /// Size of one cache entry
        /// </summary>
        const int dataLength = 1024;

        /// <summary>
        /// Parameter to simulate main store access latency
        /// </summary>
        const int mainStoreLatencyMs = 1;

        /// <summary>
        /// Counter for cache evictions.
        /// </summary>
        static int cacheEvictions;

        /// <summary>
        /// Counter for cache misses;
        /// </summary>
        static int cacheMisses;

        /// <summary>
        /// N-Way parameter for the cache
        /// </summary>
        static uint cacheNWays = 32;

        /// <summary>
        /// cache size;
        /// </summary>
        static uint cacheSize = 1024 * 16;

        /// <summary>
        /// Number of cache entries to retrieve
        /// </summary>
        static int entriesRetrieve = 1024 * 4;

        static void Main()
        {
            var mainStore = new MainStore<int, string>();
            var cache = new Cache<int, string>(mainStore, cacheNWays, cacheSize, new LruSortEvictionAlgorithm<int, string>());

            cache.CacheEvictionListener += CacheEvictionListener;
            cache.CacheMissListener += CacheMissListener;
            Generate1GbMainStore(ref mainStore, ref cache);

            long elapsedTicksCache = 0;
            long elapsedTicksMainStore = 0;
            long elapsedTicksMainStoreLatency = 0;
            string value;

            for (int tries = 0; tries < entriesRetrieve; tries++)
            {
                // we are using normal generator to get the key
                // note that mu and sigma parameters of Gaussian distribution affect a lot on cache performance.
                var key = (int)Math.Abs(Utilities.NextGaussian(2, 0.01) * entries) % entries;

                // in case we want our keys to be uniformly distributed
                // less honest to the cache
                //var key = Utilities.RandomGenerator.Next(entries);

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

                    value = cache.TryGetValue(key);

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


            /// <summary>
            /// Generates normally distributed numbers.
            /// Each operation makes two Gaussians for the price of one,
            /// and apparently they can be cached or something for better performance, but who cares.
            /// </summary>
            /// <param name = "mu">Mean of the distribution</param>
            /// <param name = "sigma">Standard deviation</param>
            /// <returns></returns>
            public static double NextGaussian(double mu = 0, double sigma = 1)
            {
                var u1 = RandomGenerator.NextDouble();
                var u2 = RandomGenerator.NextDouble();

                var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                      Math.Sin(2.0 * Math.PI * u2);

                var rand_normal = mu + sigma * rand_std_normal;

                return rand_normal;
            }
        }
    }
}
