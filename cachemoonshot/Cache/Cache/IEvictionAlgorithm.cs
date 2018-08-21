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
        void Evict(ref List<Entry<TKey, TValue>> entries);
    }
}
