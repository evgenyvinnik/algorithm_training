// © Evgeny Vinnik

using System.Collections.Generic;
using Cache;

namespace CacheUser
{
    class MruSortEvictionAlgorithm<TKey, TValue> : IEvictionAlgorithm<TKey, TValue>
    {
        public void Evict(ref List<Entry<TKey, TValue>> entries)
        {
            entries.Sort(new MruComparer());
            entries[entries.Count - 1].Invalidate();
        }

        class MruComparer : IComparer<Entry<TKey, TValue>>
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
