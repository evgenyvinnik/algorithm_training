// © Evgeny Vinnik

using System.Collections.Generic;

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IEvictionAlgorithm<TKey, TValue>
    {
        /// <summary>
        /// Ev
        /// </summary>
        /// <param name="entries">Flat list of entries among which an eviction should happen.
        /// Some of the entries might be marked as invalidated due to previous actions.
        /// It is ok to return them as entries to evict or pick new.</param>
        /// <returns>Must return an entry to evict. Failure to provide such would result in exception.</returns>
        Entry<TKey, TValue> Evict(ref List<Entry<TKey, TValue>> entries);
    }
}
