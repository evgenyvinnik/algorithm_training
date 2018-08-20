// © Evgeny Vinnik
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public interface IEvictionAlgorithm<TKey, TValue>
    {
        void Evict(ref List<Entry<TKey, TValue>> entries);
    }
}
