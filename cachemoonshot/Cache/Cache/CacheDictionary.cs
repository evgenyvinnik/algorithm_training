// © Evgeny Vinnik

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cache
{
    /// <summary>
    /// Class the represents cache set.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    internal class CacheDictionary<TKey, TValue>
    {
        readonly uint nWay;
        readonly Dictionary<TKey, List<Entry<TKey, TValue>>> cacheDictionary =
            new Dictionary<TKey, List<Entry<TKey, TValue>>>();

        readonly IEvictionAlgorithm<TKey, TValue> evictionAlgorithm;

        readonly object thisLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nWay"></param>
        /// <param name="evictionAlgorithm"></param>
        internal CacheDictionary(uint nWay, IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            this.nWay = nWay;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        /// <summary>
        /// 
        /// </summary>
        internal event EventHandler<InvalidationEventArgs> EvictionListener;

        /// <summary>
        /// 
        /// </summary>
        internal uint EntryCount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void InsertEntry(TKey key, TValue value)
        {
            lock (thisLock)
            {
                if (cacheDictionary.ContainsKey(key))
                {
                    Invalidate(key, InvalidationSource.Replacement);
                }

                if (EntryCount >= nWay)
                {
                    var entries = cacheDictionary.Values.SelectMany(x => x).ToList();
                    var evictEntry = evictionAlgorithm.Evict(ref entries);
                    if (evictEntry == null)
                    {
                        throw new ArgumentException("Eviction algorithm didn't provide entry to evict!");
                    }

                    evictEntry.Invalidate(InvalidationSource.Eviction);
                    DeleteInvalidEntries();
                }

                var entry = new Entry<TKey, TValue>(key, value);
                entry.InvalidationListener += OnEntryInvalidated;

                if (cacheDictionary.ContainsKey(key))
                {
                    cacheDictionary[key].Add(entry);
                }
                else
                {
                    cacheDictionary.Add(
                        key,
                        new List<Entry<TKey, TValue>>
                        {
                            entry
                        });
                }

                EntryCount++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal Entry<TKey, TValue> FindEntry(TKey key)
        {
            lock (thisLock)
            {
                if (!cacheDictionary.ContainsKey(key))
                {
                    return null;
                }

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
                        Invalidate(key, InvalidationSource.Replacement);
                        return null;
                }
            }
        }

        /// <summary>
        /// Invalidates <see cref="Entry{TKey,Tvalue}"/> by provided key.
        /// </summary>
        /// <param name="key">Cache entry key to invalidate.</param>
        /// <param name="source">Value indicating the source of invalidation request.</param>
        /// <returns>Returns true if cache entry invalidation is successful,
        /// false if key is missing in cache set.</returns>
        internal bool Invalidate(TKey key, InvalidationSource source)
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
                    entry.Invalidate(source);
                }

                return true;
            }
        }

        /// <summary>
        /// Deletes all <see cref="Entry{TKey,Tvalue}"/> that were marked as invalid.
        /// </summary>
        void DeleteInvalidEntries()
        {
            var keysToRemove = new List<TKey>();
            foreach (var entry in cacheDictionary)
            {
                var list = entry.Value;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].ValidityBit == ValidityBit.Valid)
                    {
                        continue;
                    }

                    list.RemoveAt(i);
                    EntryCount--;
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

        void OnEntryInvalidated(object sender, InvalidationEventArgs e)
        {
            EvictionListener?.Invoke(this, e);
        }
    }
}
