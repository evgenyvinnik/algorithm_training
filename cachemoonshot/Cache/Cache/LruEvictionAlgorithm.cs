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
        public void Evict(ref List<Entry<TKey, TValue>> entries)
        {
            var evictEntry = entries[0];
            for (int i = 1; i < entries.Count; i++)
            {
                if (entries[i].ValidityBit == ValidityBit.Invalid)
                {
                    evictEntry = entries[i];
                    break;
                }

                if (entries[i].AccessTime < evictEntry.AccessTime)
                {
                    evictEntry = entries[i];
                }
            }

            evictEntry.Invalidate();
        }
    }
}
