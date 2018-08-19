// © Evgeny Vinnik

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Cache
{
    class CacheDictionary<TKey, TValue>
    {
        Dictionary<TKey, List<Entry<TKey, TValue>>> cacheDictionary = new Dictionary<TKey, List<Entry<TKey, TValue>>>();
        int N;

        public int Count { get; private set; }

        Object thisLock = new Object();


        public CacheDictionary(int n)
        {
            N = n;
        }

        public void InsertEntry(TKey key, TValue value)
        {
            lock (thisLock)
            {
                if (Count >= N)
                {

                }

                // cacheDictionary.
                //Bag.Add(new Entry<TKey, TValue>(key, value));

                Count++;
            }
        }

        public Entry<TKey, TValue> FindEntry(TKey key)
        {
            lock (thisLock)
            {
                var list = new List<Entry<TKey, TValue>>();

                foreach (var entry in cacheDictionary[key])
                {
                    if (entry.ValidityBit == ValidityBit.Valid)
                    {
                        list.Add(entry);
                    }
                }

                if (list.Count > 0)
                {
                    throw new MultipleEntriesException();

                }

                return list[0];
            }
        }

        public void Invalidate(TKey key)
        {
            lock (thisLock)
            {
               // List<int> 
            }

            /*foreach (var entry in cacheDictionary[key])
            {
                entry.ValidityBit = ValidityBit.Invalid;
            }*/
        }
    }
}
