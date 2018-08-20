// © Evgeny Vinnik

using System;
using System.Linq;

namespace Cache
{
    public class Cache<TKey, TValue>
    {
        public const uint MaxNWays = 128;
        public const uint MaxCacheEntries = 0x7FFFFFFF;

        readonly IMainStore<TKey, TValue> mainStore;
        readonly CacheDictionary<TKey, TValue>[] cacheDictionaries;

        public Cache(IMainStore<TKey, TValue> mainStore) : this(mainStore, 4, 128)
        {

        }

        public Cache(IMainStore<TKey, TValue> mainStore, uint nWay, uint totalCacheEntries) :
            this(mainStore, nWay, totalCacheEntries, new LruEvictionAlgorithm<TKey, TValue>())
        {

        }

        public Cache(
            IMainStore<TKey, TValue> mainStore,
            uint nWay,
            uint totalCacheEntries,
            IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            if (nWay > MaxNWays)
            {
                throw new ArgumentException($"nWay ways should be less or equal {MaxNWays}");
            }

            if (!CacheUtils.IsPowerOfTwo(nWay))
            {
                throw new ArgumentException("nWay ways should be a power of two!");
            }

            if (totalCacheEntries > MaxCacheEntries)
            {
                throw new ArgumentException($"Number of total cache entries should be less or equal than {MaxCacheEntries}");
            }

            if (!CacheUtils.IsPowerOfTwo(totalCacheEntries))
            {
                throw new ArgumentException("Number of total cache entries should be a power of two!");
            }

            if (totalCacheEntries <= nWay)
            {
                throw new ArgumentException($"Number of total cache entries {totalCacheEntries} should more or equal than {nWay}");
            }

            if (evictionAlgorithm == null)
            {
                throw new ArgumentException("Eviction algorithm isn't specified!");
            }

            this.mainStore = mainStore ?? throw new ArgumentException("Main data store should isn't specified!");

            this.nWay = nWay;
            TotalCacheEntries = totalCacheEntries;
            cacheDictionaries =
                Enumerable.Repeat(
                    new CacheDictionary<TKey, TValue>(nWay, evictionAlgorithm),
                    (int)(totalCacheEntries / nWay)).ToArray();
        }

        public uint nWay { get; }

        public uint TotalCacheEntries { get; }

        public int GetCacheSets() => cacheDictionaries.Length;

        public void PutValue(TKey key, TValue value)
        {
            GetDictionary(key).InsertEntry(key, value);
        }

        public TValue GetValue(TKey key)
        {
            var dictionary = GetDictionary(key);

            try
            {
                var entry = dictionary.FindEntry(key);

                return entry != null ? entry.Value : GetMainStoreAndCacheReinsert(key, dictionary);
            }
            catch (MultipleEntriesException e)
            {
                Console.WriteLine(e);

                return GetMainStoreAndCacheReinsert(key, dictionary);
            }
        }

        public bool DeleteValue(TKey key)
        {
            return GetDictionary(key).Invalidate(key);
        }

        TValue GetMainStoreAndCacheReinsert(TKey key, CacheDictionary<TKey, TValue> dictionary)
        {
            var value = mainStore.GetValue(key);
            dictionary.InsertEntry(key, value);
            return value;
        }

        CacheDictionary<TKey, TValue> GetDictionary(TKey key)
        {
            return cacheDictionaries[(int)CacheUtils.ModTwo((uint) key.GetHashCode(), TotalCacheEntries)];
        }
    }
}
