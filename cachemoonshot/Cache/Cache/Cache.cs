// © Evgeny Vinnik

using System;
using System.Linq;

namespace Cache
{
    public class Cache<TKey, TValue>
    {
        public const uint MaxNWays = 128;

        readonly uint size;
        readonly IMainStore<TKey, TValue> mainStore;
        readonly CacheDictionary<TKey, TValue>[] cacheDictionaries;

        //TODO: better default constructor
        public Cache(IMainStore<TKey, TValue> mainStore) : this(mainStore, 4, 128)
        {

        }

        public Cache(IMainStore<TKey, TValue> mainStore, uint N, uint size) : this(mainStore, N, size, new LruEvictionAlgorithm<TKey, TValue>())
        {

        }

        public Cache(IMainStore<TKey, TValue> mainStore, uint N, uint size, IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            if (!IsPowerOfTwo(N) && N < MaxNWays)
            {
                throw new ArgumentException();
            }

            if (!IsPowerOfTwo(size) && size < int.MaxValue)
            {
                throw new ArgumentException();
            }

            //TODO: add checks
            this.mainStore = mainStore;
            this.size = size;
            cacheDictionaries =
                Enumerable.Repeat(new CacheDictionary<TKey, TValue>(N, evictionAlgorithm), (int)size).ToArray();
        }

        public void PutValue(TKey key, TValue value)
        {
            GetDictionary(key).InsertEntry(key, value);
        }

        public TValue GetValue(TKey key)
        {
            var dictionary = GetDictionary(key);

            try
            {
                var entry = dictionary.FindEntry(key);

                return entry != null ? entry.Value : GetMainStoreAndCacheReinsert(key, dictionary);
            }
            catch (MultipleEntriesException e)
            {
                Console.WriteLine(e);

                return GetMainStoreAndCacheReinsert(key, dictionary);
            }
        }

        public void DeleteValue(TKey key)
        {
            GetDictionary(key).Invalidate(key);
        }

        TValue GetMainStoreAndCacheReinsert(TKey key, CacheDictionary<TKey, TValue> dictionary)
        {
            var value = mainStore.Get(key);
            dictionary.InsertEntry(key, value);
            return value;
        }

        CacheDictionary<TKey, TValue> GetDictionary(TKey key)
        {
            return cacheDictionaries[(int)ModTwo((uint) key.GetHashCode(), size)];
        }

        static bool IsPowerOfTwo(uint x)
        {
            return x != 0 && ModTwo(x, x-1) == 0;
        }

        static uint ModTwo(uint n, uint d)
        {
            return d & (n - 1);
        }
    }
}
