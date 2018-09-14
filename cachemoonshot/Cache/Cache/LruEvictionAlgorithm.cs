// © Evgeny Vinnik

using System.Collections.Generic;
using System.Diagnostics;

namespace Cache
{
    /// <summary>
    /// Last recently used eviction algorithm implementation.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public class LruEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
    {
        /// <summary>
        /// Selects the last recently used cache entry to evict from a list of all entries in a cache set.
        /// </summary>
        /// <param name="entries">Flat list of entries among which an eviction should happen.</param>
        /// <returns>Returns an entry to evict.</returns>
        public Entry<TKey, TValue> Evict(List<Entry<TKey, TValue>> entries)
        {
            Debug.Assert(entries.Count != 0);

            var evictEntry = entries[0];

            for (int i = 1; i < entries.Count; i++)
            {
                if (entries[i].AccessTime < evictEntry.AccessTime)
                {
                    evictEntry = entries[i];
                }
            }

            return evictEntry;
        }
    }
}
