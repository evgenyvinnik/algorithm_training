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

        //TODO: better default constructor
        public Cache() : this(4, 128)
        {

        }

        public Cache(uint N, uint size) : this(N, size, new LruEvictionAlgorithm())
        {

        }

        public Cache(uint N, uint size, IEvictionAlgorithm evictionAlgorithm)
        {
            //TODO: add checks
            this.N = N;
            this.size = size;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        public void Put(TKey key, TValue value)
        {

        }

        public TValue Get(TKey key)
        {
            throw new NotImplementedException();
        }

        public void Delete(TKey key)
        {

        }
    }
}
