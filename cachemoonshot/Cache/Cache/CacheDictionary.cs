// © Evgeny Vinnik

using System;
using System.Collections.Generic;
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
        /// <summary>
        /// N-Way cache parameter.
        /// </summary>
        readonly uint nWay;
        
        /// <summary>
        /// Cache set dictionary.
        /// <remarks>Dictionary was chosen as it provides reasonable
        /// balance between storage overhead vs search speed</remarks>
        /// </summary>
        readonly Dictionary<TKey, List<Entry<TKey, TValue>>> cacheDictionary =
            new Dictionary<TKey, List<Entry<TKey, TValue>>>();

        /// <summary>
        /// Specified eviction algorithm.
        /// </summary>
        readonly IEvictionAlgorithm<TKey, TValue> evictionAlgorithm;

        /// <summary>
        /// Object lock that helps with providing thread-safety for cache set.
        /// </summary>
        readonly object thisLock = new object();

        /// <summary>
        /// Cache set constructor.
        /// </summary>
        /// <param name="nWay">N-Way cache parameter</param>
        /// <param name="evictionAlgorithm">Eviction algorithm</param>
        internal CacheDictionary(uint nWay, IEvictionAlgorithm<TKey, TValue> evictionAlgorithm)
        {
            this.nWay = nWay;
            this.evictionAlgorithm = evictionAlgorithm;
        }

        /// <summary>
        /// Eviction event listener.
        /// </summary>
        internal event EventHandler<InvalidationEventArgs> EvictionListener;

        /// <summary>
        /// Number of entries in the cache set.
        /// </summary>
        internal uint EntryCount { get; private set; }

        /// <summary>
        /// Insert key/value pair into cache set.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <param name="value">Cache entry value.</param>
        internal void InsertEntry(TKey key, TValue value)
        {
            // we are locking the object to provide thread-safety.
            lock (thisLock)
            {
                // if key already exists in the cache set - invalidate it.
                if (cacheDictionary.ContainsKey(key))
                {
                    Invalidate(key, InvalidationSource.Replacement);
                }

                // if cache set has more entries than n-Way parameter
                // we call the eviction algorithm
                if (EntryCount >= nWay)
                {
                    var entries = cacheDictionary.Values.SelectMany(x => x).ToList();
                    var evictEntry = evictionAlgorithm.Evict(ref entries);

                    // this is just a simple verification that eviction algorithm has provided
                    // a valid entry to evict
                    if (evictEntry == null || !entries.Contains(evictEntry))
                    {
                        throw new ArgumentException("Eviction algorithm didn't provide entry to evict!");
                    }

                    // selected entry is invalidated and the entry deletion procedure is being called.
                    evictEntry.Invalidate(InvalidationSource.Eviction);
                    DeleteInvalidEntries();
                }

                // a new entry is then created and inserted into dictionary
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
        /// Find cache entry in the set by provided key.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <returns>Returns cache entry if a valid entry exists, null otherwise.</returns>
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
                        // if by some reason we have more than 1 valid entry for a given key
                        // we have a cache collision and we invalidate all entries.
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

        /// <summary>
        /// Listener to invalidation events coming from cache entries.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Eviction event arguments.</param>
        void OnEntryInvalidated(object sender, InvalidationEventArgs e)
        {
            //we are retranslating events to the cache object.
            EvictionListener?.Invoke(this, e);
        }
    }
}
