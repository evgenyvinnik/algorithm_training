// © Evgeny Vinnik

using System;

namespace Cache
{
    /// <summary>
    /// Class representing single cache entry.
    /// </summary>
    /// <typeparam name="TKey">Cache entry key.</typeparam>
    /// <typeparam name="TValue">Cache entry value.</typeparam>
    public class Entry<TKey, TValue>
    {
        /// <summary>
        /// Cached entry value.
        /// </summary>
        TValue cacheValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry{TKey,Tvalue}"/> class.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <param name="value">Cache entry value.</param>
        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            AccessTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets cache entry key.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Gets last access time to the cache entry.
        /// </summary>
        public DateTime AccessTime { get; private set; }

        /// <summary>
        /// Gets <see cref="cacheValue"/> and updates <see cref="AccessTime"/>.
        /// </summary>
        internal TValue Value
        {
            get
            {
                AccessTime = DateTime.UtcNow;
                return cacheValue;
            }

            private set => cacheValue = value;
        }
    }
}
