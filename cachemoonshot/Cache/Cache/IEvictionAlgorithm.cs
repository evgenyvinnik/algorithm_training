// © Evgeny Vinnik

using System.Collections.Generic;

namespace Cache
{
    /// <summary>
    /// Interface for eviction algorithm implemenation.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public interface IEvictionAlgorithm<TKey, TValue>
    {
        /// <summary>
        /// Selects a cache entry to evict from a list of all entries in a cache set.
        /// </summary>
        /// <param name="entries">Flat list of entries among which an eviction should happen.
        /// Some of the entries might be marked as invalidated due to previous actions.
        /// It is ok to return them as entries to evict or pick new.</param>
        /// <returns>Must return an entry to evict. Failure to provide such would result in exception.</returns>
        Entry<TKey, TValue> Evict(List<Entry<TKey, TValue>> entries);
    }
}
