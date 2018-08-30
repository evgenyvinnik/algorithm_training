// © Evgeny Vinnik

using System.Collections.Generic;
using System.Diagnostics;

namespace Cache
{
    /// <summary>
    /// Most recently used eviction algorithm.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    class MruEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
    {
        /// <summary>
        /// Selects the most recently used cache entry to evict from a list of all entries in a cache set.
        /// </summary>
        /// <param name="entries">Flat list of entries among which an eviction should happen.</param>
        /// <returns>Return an entry to evict.</returns>
        public Entry<TKey, TValue> Evict(ref List<Entry<TKey, TValue>> entries)
        {
            Debug.Assert(entries.Count != 0);

            var evictEntry = entries[0];

            for (int i = 1; i < entries.Count; i++)
            {
                // since we need to provide only one entry we are being smart here
                // and automatically returning the first invalidated entry no matter when it was used
                if (entries[i].ValidityBit == ValidityBit.Invalid)
                {
                    return entries[i];
                }

                if (entries[i].AccessTime > evictEntry.AccessTime)
                {
                    evictEntry = entries[i];
                }
            }

            return evictEntry;
        }
    }
}
