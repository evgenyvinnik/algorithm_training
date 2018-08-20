// © Evgeny Vinnik

using System.Collections.Generic;

namespace Cache.Tests
{
    class MainStore<TKey, TValue> : IMainStore<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> values = new Dictionary<TKey, TValue>();

        void PutValue(TKey key, TValue value)
        {
            values[key] = value;
        }

        TValue IMainStore<TKey, TValue>.GetValue(TKey key)
        {
            return values[key];
        }

        void DeleteValue(TKey key)
        {

        }
    }
}
