// © Evgeny Vinnik

using System.Collections.Generic;
using Cache;

namespace Cache
{
    class MruEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
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

                if (entries[i].AccessTime > evictEntry.AccessTime)
                {
                    evictEntry = entries[i];
                }
            }

            evictEntry.Invalidate();
        }
    }
}
