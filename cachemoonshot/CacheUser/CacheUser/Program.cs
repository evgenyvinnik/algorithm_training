// © Evgeny Vinnik

using System;
using Cache;

namespace CacheUser
{
    class Program
    {
        const int entries = 10;
        //const int entries = 1024 * 1024;
        const int keyLength = 100;
        const int dataLength = 1024;

        static void Main(string[] args)
        {
            MainStore<string, string> mainStore = new MainStore<string, string>();
            Cache<string, string> cache = new Cache<string,string>();

            // accessing elements from cache with fallback to mainstore

            var watch = System.Diagnostics.Stopwatch.StartNew();
            mainStore.GetValue()

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Console.WriteLine($"Main store access {elapsedMs.ElapsedMilliseconds}");

            // accessing elements from mainstore only


        }

        static void Generate1GbMainStore(ref MainStore<string, string> mainStore, ref Cache<string, string> cache)
        {
        }
    }
}
