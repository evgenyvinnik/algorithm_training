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
        /// Flag indicating whether cache entry is valid.
        /// </summary>
        volatile ValidityBit validityBit;

        /// <summary>
        /// Initializes a new instance of the <see cref="Entry{TKey,Tvalue}"/> class.
        /// </summary>
        /// <param name="key">Cache entry key.</param>
        /// <param name="value">Cache entry value.</param>
        public Entry(TKey key, TValue value)
        {
            validityBit = ValidityBit.Valid;
            Key = key;
            Value = value;
            AccessTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Event handler that is being used to signal that cache entry was invalidated.
        /// </summary>
        internal event EventHandler<InvalidationEventArgs> InvalidationListener;

        /// <summary>
        /// Gets cache entry key.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Gets <see cref="validityBit"/> value.
        /// </summary>
        public ValidityBit ValidityBit
        {
            get => validityBit;
        }

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

        /// <summary>
        /// Invalidates cache entry.
        /// </summary>
        /// <param name="source"><see cref="InvalidationSource"/> that will
        /// be used to signal <see cref="InvalidationListener"/>.</param>
        internal void Invalidate(InvalidationSource source)
        {
            // can't invalidate what is already invalid.
            if (validityBit == ValidityBit.Invalid)
            {
                return;
            }

            validityBit = ValidityBit.Invalid;
            InvalidationListener?.Invoke(
                this,
                new InvalidationEventArgs
                {
                    Source = source
                });
        }
    }
}
