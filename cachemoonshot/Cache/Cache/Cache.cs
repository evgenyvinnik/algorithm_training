// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// Implementation of N-way, Set-Associative cache.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public class Cache<TKey, TValue>
    {
        /// <summary>
        /// Maximumum value for N-way.
        /// </summary>
        public const uint MaxNWays = 128;

        /// <summary>
        /// Maximum number of cache entries.
        /// </summary>
        public const uint MaxCacheEntries = 0x7FFFFFFF;

        /// <summary>
        /// Cache sets. Each contains a maximum of <see cref="NWay"/> entries.
        /// </summary>
        readonly CacheDictionary<TKey, TValue>[] cacheDictionaries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache{TKey,Tvalue}"/> class.
        /// Creates 4-way set associative cache with 128 cache entries.
        /// <see cref="LruEvictionAlgorithm{TKey,Tvalue}"/> is set as default.
        /// </summary>
        /// <inheritdoc />
        public Cache() : this(4, 128)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache{TKey,Tvalue}"/> class.
        /// Creates n-way set associative cache with variable number of cache entries.
        /// <see cref="LruEvictionAlgorithm{TKey,Tvalue}"/> is set as default.
        /// </summary>
        /// <param name="nWay">N-way parameter for the cache. Should be a power of 2.</param>
        /// <param name="totalCacheEntries">Total number of cache entries. Should be a power of 2.</param>
        /// <inheritdoc />
        public Cache(uint nWay, uint totalCacheEntries) :
            this(nWay, totalCacheEntries, new LruEvictionAlgorithm<TKey, TValue>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache{TKey,Tvalue}"/> class.
        /// Creates n-way set associative cache with variable number of cache entries.
        /// It is possible to specify custom eviction algorithm.
        /// </summary>
        /// <param name="nWay">N-way parameter for the cache. Should be a power of 2.</param>
        /// <param name="totalCacheEntries">Total number of cache entries. Should be a power of 2.</param>
        /// <param name="evictionAlgorithm">Eviction algorithm.</param>
        public Cache(
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

            if (totalCacheEntries < nWay)
            {
                throw new ArgumentException($"Number of total cache entries {totalCacheEntries} should more or equal than {nWay} ways");
            }

            if (evictionAlgorithm == null)
            {
                throw new ArgumentException("Eviction algorithm isn't specified!");
            }

            NWay = nWay;
            TotalCacheEntries = totalCacheEntries;
            CacheSets = totalCacheEntries / nWay;

            cacheDictionaries = new CacheDictionary<TKey, TValue>[CacheSets];
            for (int i = 0; i < CacheSets; i++)
            {
                cacheDictionaries[i] = new CacheDictionary<TKey, TValue>(nWay, evictionAlgorithm);
                cacheDictionaries[i].EvictionListener += OnCacheEntryInvalidated;
            }
        }

        /// <summary>
        /// Cache miss listener event handler.
        /// Use it to get notifications about cache misses.
        /// </summary>
        public event EventHandler CacheMissListener;

        /// <summary>
        /// Cache eviction listener event handler.
        /// Use it to get noficications about cache evictions.
        /// </summary>
        public event EventHandler<InvalidationEventArgs> CacheEvictionListener;

        /// <summary>
        /// Gets N-way parameter.
        /// </summary>
        public uint NWay { get; }

        /// <summary>
        /// Gets total number of cache entries.
        /// </summary>
        public uint TotalCacheEntries { get; }

        /// <summary>
        /// Gets total number of created cache sets.
        /// </summary>
        public uint CacheSets { get; }

        /// <summary>
        /// Put value into cache.
        /// This operation might trigger the eviction algorithm.
        /// </summary>
        /// <remarks>Since we support custom eviction algorithms,
        /// if eviction algorithm fails to provide a valid entry to evict
        /// an ArgumentException would be thrown.</remarks>
        /// <param name="key">Cache entry key.</param>
        /// <param name="value">Cache entry value.</param>
        public void PutValue(TKey key, TValue value)
        {
            GetDictionary(key).InsertEntry(key, value);
        }

        /// <summary>
        /// Try getting value from the cache.
        /// If the value in cache is missing or invalidated,
        /// an <see cref="CacheMissException"/> is thrown.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <returns>Cache entry value.</returns>
        public TValue TryGetValue(TKey key)
        {
            var dictionary = GetDictionary(key);

            var entry = dictionary.FindEntry(key);

            if (entry == null)
            {
                CacheMissListener?.Invoke(this, EventArgs.Empty);
                throw new CacheMissException($"Value with key {key} isn't cached.");
            }

            return entry.Value;
        }

        /// <summary>
        /// Delete value from the cache.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <returns>Returns true if deletion is successful, false if key is missing in cache.</returns>
        public bool DeleteValue(TKey key)
        {
            return GetDictionary(key).Invalidate(key, InvalidationSource.User);
        }

        /// <summary>
        /// Get cache set that is corresponding to cache entry key.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <returns>Cache set that corresponds to the key.</returns>
        CacheDictionary<TKey, TValue> GetDictionary(TKey key)
        {
            return cacheDictionaries[(int)CacheUtils.ModTwo((uint)key.GetHashCode(), CacheSets)];
        }

        /// <summary>
        /// Cache eviction event retranslator.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Eviction event arguments.</param>
        void OnCacheEntryInvalidated(object sender, InvalidationEventArgs e)
        {
            CacheEvictionListener?.Invoke(this, e);
        }
    }
}
