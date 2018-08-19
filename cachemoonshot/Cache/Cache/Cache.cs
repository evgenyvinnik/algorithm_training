// © Evgeny Vinnik
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class Cache<TKey, TValue>
    {
        uint N;
        uint size;
        IEvictionAlgorithm evictionAlgorithm;
        IMainStore<TKey, TValue> mainStore;

        //TODO: better default constructor
        public Cache(IMainStore<TKey, TValue> mainStore) : this(mainStore, 4, 128)
        {

        }

        public Cache(IMainStore<TKey, TValue> mainStore, uint N, uint size) : this(mainStore, N, size, new LruEvictionAlgorithm())
        {

        }

        public Cache(IMainStore<TKey, TValue> mainStore, uint N, uint size, IEvictionAlgorithm evictionAlgorithm)
        {
            //TODO: add checks
            this.mainStore = mainStore;
            this.N = N;
            this.size = size;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        public void Put(TKey key, TValue value)
        {

        }

        public TValue Get(TKey key)
        {
            return mainStore.Get(key);
        }

        public void Delete(TKey key)
        {

        }


    }
}
