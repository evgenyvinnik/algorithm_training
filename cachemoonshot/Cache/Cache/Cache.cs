// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainStore"></param>
        /// <param name="nWay"></param>
        /// <param name="totalCacheEntries"></param>
        /// <param name="evictionAlgorithm"></param>
        public Cache(
            IMainStore<TKey, TValue> mainStore,
            uint nWay,
            uint totalCacheEntries,
            IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            if (nWay > MaxNWays)
            {
                throw new ArgumentException($"N-Way should be less or equal {MaxNWays}");
            }

            if (!CacheUtils.IsPowerOfTwo(nWay))
            {
                throw new ArgumentException("N-Way should be a power of two!");
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
                throw new ArgumentException($"Number of total cache entries {totalCacheEntries} should more or equal than {nWay} ways");
            }

            if (evictionAlgorithm == null)
            {
                throw new ArgumentException("Eviction algorithm isn't specified!");
            }

            this.mainStore = mainStore ?? throw new ArgumentException("Main data store should isn't specified!");

            this.NWay = nWay;
            TotalCacheEntries = totalCacheEntries;
            CacheSets = totalCacheEntries / nWay;

            cacheDictionaries = new CacheDictionary<TKey, TValue>[CacheSets];
            for (int i = 0; i < CacheSets; i++)
            {
                cacheDictionaries[i] = new CacheDictionary<TKey, TValue>(nWay, evictionAlgorithm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public uint NWay { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint TotalCacheEntries { get; }

        /// <summary>
        /// 
        /// </summary>
        public uint CacheSets { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void PutValue(TKey key, TValue value)
        {
            GetDictionary(key).InsertEntry(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetValue(TKey key)
        {
            var dictionary = GetDictionary(key);

            try
            {
                var entry = dictionary.FindEntry(key);

                if (entry != null)
                {
                    return entry.Value;
                }
            }
            catch (MultipleEntriesException e)
            {
                Console.WriteLine(e);
            }

            return GetMainStoreAndCacheReinsert(key, dictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteValue(TKey key)
        {
            return GetDictionary(key).Invalidate(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        TValue GetMainStoreAndCacheReinsert(TKey key, CacheDictionary<TKey, TValue> dictionary)
        {
            var value = mainStore.GetValue(key);
            dictionary.InsertEntry(key, value);
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        CacheDictionary<TKey, TValue> GetDictionary(TKey key)
        {
            return cacheDictionaries[(int)CacheUtils.ModTwo((uint)key.GetHashCode(), CacheSets)];
        }
    }
}
