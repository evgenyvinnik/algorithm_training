// © Evgeny Vinnik

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cache
{
    /// <summary>
    /// Class the represents cache set.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    internal class CacheDictionary<TKey, TValue>
    {
        /// <summary>
        /// N-Way cache parameter.
        /// </summary>
        readonly uint nWay;
        
        /// <summary>
        /// Cache set.
        /// <remarks>Dictionary was chosen as it provides reasonable
        /// balance between storage overhead vs search speed</remarks>
        /// </summary>
        readonly Dictionary<TKey, Entry<TKey, TValue>> cacheDictionary =
            new Dictionary<TKey, Entry<TKey, TValue>>();

        /// <summary>
        /// Specified eviction algorithm.
        /// </summary>
        readonly IEvictionAlgorithm<TKey, TValue> evictionAlgorithm;

        /// <summary>
        /// Object lock that helps with providing thread-safety for cache set.
        /// </summary>
        readonly object thisLock = new object();

        /// <summary>
        /// Cache set constructor.
        /// </summary>
        /// <param name="nWay">N-Way cache parameter</param>
        /// <param name="evictionAlgorithm">Eviction algorithm</param>
        internal CacheDictionary(uint nWay, IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            this.nWay = nWay;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        /// <summary>
        /// Eviction event listener.
        /// </summary>
        internal event EventHandler<InvalidationEventArgs> EvictionListener;

        /// <summary>
        /// Number of entries in the cache set.
        /// </summary>
        internal uint EntryCount { get; private set; }

        /// <summary>
        /// Insert key/value pair into cache set.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <param name="value">Cache entry value.</param>
        internal void InsertEntry(TKey key, TValue value)
        {
            // we are locking the object to provide thread-safety.
            lock (thisLock)
            {
                // if key already exists in the cache set - invalidate it.
                if (cacheDictionary.ContainsKey(key))
                {
                    InvalidateAndDelete(key, InvalidationSource.Replacement);
                }

                // if cache set has more entries than n-Way parameter
                // we call the eviction algorithm
                if (EntryCount >= nWay)
                {
                    var entries = cacheDictionary.Values.ToList();
                    var evictEntry = evictionAlgorithm.Evict(ref entries);

                    // this is just a simple verification that eviction algorithm has provided
                    // a valid entry to evict
                    if (evictEntry == null || !entries.Contains(evictEntry))
                    {
                        throw new ArgumentException("Eviction algorithm didn't provide entry to evict!");
                    }

                    // selected entry is invalidated and the entry deletion procedure is being called.
                    InvalidateAndDelete(evictEntry.Key, InvalidationSource.Eviction);
                }

                // a new entry is then created and inserted into dictionary
                cacheDictionary.Add(key, new Entry<TKey, TValue>(key, value));

                EntryCount++;
            }
        }

        /// <summary>
        /// Find cache entry in the set by provided key.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <returns>Returns cache entry if a valid entry exists, null otherwise.</returns>
        internal Entry<TKey, TValue> FindEntry(TKey key)
        {
            lock (thisLock)
            {
                return !cacheDictionary.ContainsKey(key) ? null : cacheDictionary[key];
            }
        }

        /// <summary>
        /// Invalidates <see cref="Entry{TKey,Tvalue}"/> by provided key and deletes it from the set.
        /// </summary>
        /// <param name="key">Cache entry key to invalidate.</param>
        /// <param name="source">Value indicating the source of invalidation request.</param>
        /// <returns>Returns true if cache entry invalidation is successful,
        /// false if key is missing in cache set.</returns>
        internal bool InvalidateAndDelete(TKey key, InvalidationSource source)
        {
            lock (thisLock)
            {
                if (!cacheDictionary.ContainsKey(key))
                {
                    return false;
                }

                cacheDictionary.Remove(key);
                EntryCount--;
                EvictionListener?.Invoke(
                    this,
                    new InvalidationEventArgs
                    {
                        Source = source
                    });
                return true;
            }
        }
    }
}
