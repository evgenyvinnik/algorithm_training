// © Evgeny Vinnik

using System.Collections.Generic;

namespace Cache
{
    public interface IEvictionAlgorithm<TKey, TValue>
    {
        void Evict(ref List<Entry<TKey, TValue>> entries);
    }
}
