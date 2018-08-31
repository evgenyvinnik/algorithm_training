// © Evgeny Vinnik

using System.Collections.Generic;
using Cache;

namespace CacheUser
{
    /// <summary>
    /// This is a "lazy" last recently used eviction algorithm.
    /// We sort the list by access time and evict first element.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public class LruSortEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
    {
        public Entry<TKey, TValue> Evict(ref List<Entry<TKey, TValue>> entries)
        {
            entries.Sort(new LruComparer());
            return entries[0];
        }

        class LruComparer : IComparer<Entry<TKey, TValue>>
        {
            public int Compare(Entry<TKey, TValue> x, Entry<TKey, TValue> y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (x == null)
                {
                    return -1;
                }

                if (y == null)
                {
                    return 1;
                }

                return x.AccessTime.CompareTo(y.AccessTime);
            }
        }
    }
}
