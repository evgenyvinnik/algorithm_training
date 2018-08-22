// © Evgeny Vinnik

using System.Collections.Generic;

namespace Cache
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class LruEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        public Entry<TKey, TValue> Evict(ref List<Entry<TKey, TValue>> entries)
        {
            var evictEntry = entries[0];
            for (int i = 1; i < entries.Count; i++)
            {
                if (entries[i].ValidityBit == ValidityBit.Invalid)
                {
                    return entries[i];
                }

                if (entries[i].AccessTime < evictEntry.AccessTime)
                {
                    evictEntry = entries[i];
                }
            }

            return evictEntry;
        }
    }
}
