﻿// © Evgeny Vinnik

using System.Collections.Generic;
using System.Linq;

namespace Cache
{
    class CacheDictionary<TKey, TValue>
    {
        readonly Dictionary<TKey, List<Entry<TKey, TValue>>> cacheDictionary =
            new Dictionary<TKey, List<Entry<TKey, TValue>>>();
        readonly uint N;
        readonly IEvictionAlgorithm<TKey, TValue> evictionAlgorithm;

        public uint Count { get; private set; }

        readonly object thisLock = new object();

        public CacheDictionary(uint N, IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            this.N = N;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        public void InsertEntry(TKey key, TValue value)
        {
            lock (thisLock)
            {
                if (cacheDictionary.ContainsKey(key))
                {
                    Invalidate(key);
                }

                if (Count >= N)
                {
                    var entries = cacheDictionary.Values.SelectMany(x => x).ToList();
                    evictionAlgorithm.Evict(ref entries);
                }

                DeleteInvalidEntries();

                cacheDictionary[key]= new List<Entry<TKey, TValue>> { new Entry<TKey, TValue>(key, value) };
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

                switch (list.Count)
                {
                    case 0:
                        return null;
                    case 1:
                        return list[0];
                    default:
                        Invalidate(key);
                        throw new MultipleEntriesException();
                }
            }
        }

        public bool Invalidate(TKey key)
        {
            lock (thisLock)
            {
                if (!cacheDictionary.ContainsKey(key))
                {
                    return false;
                }

                var list = cacheDictionary[key];
                foreach (var entry in list)
                {
                    entry.Invalidate();
                }

                return true;
            }
        }

        void DeleteInvalidEntries()
        {
            var keysToRemove = new List<TKey>();
            foreach (var entry in cacheDictionary)
            {
                var list = entry.Value;
                for (int i = list.Count - 1; i >= 0; i++)
                {
                    if (list[i].ValidityBit == ValidityBit.Valid)
                    {
                        continue;
                    }

                    list.RemoveAt(i);
                    Count--;
                }

                if (list.Count == 0)
                {
                    keysToRemove.Add(entry.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                cacheDictionary.Remove(key);
            }
        }
    }
}