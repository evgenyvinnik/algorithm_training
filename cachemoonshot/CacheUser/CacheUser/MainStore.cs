// © Evgeny Vinnik

using System.Collections.Generic;
using System.Threading;
using Cache;

namespace CacheUser
{
    class MainStore<TKey, TValue> : IMainStore<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();

        public void PutValue(TKey key, TValue value)
        {
            values[key] = value;
        }

        public TValue GetValue(TKey key)
        {
            return values[key];
        }

        public TValue GetValueLatency(TKey key, int latencyMs)
        {
            Thread.Sleep(latencyMs);
            return values[key];
        }

        public bool DeleteValue(TKey key)
        {
            return values.Remove(key);
        }

        public int Count() => values.Count;
    }
}
